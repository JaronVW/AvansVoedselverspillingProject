using Core.DomainServices;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoedselVerspillingWebApp.Models;

namespace VoedselVerspillingWebApp.Controllers;

public class MaaltijdboxController : Controller
{
    private readonly IMealBoxRepository _mealBoxRepository;
    private readonly ICanteenRepository _canteenRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IProductRepository _productRepository;

    public MaaltijdboxController(IMealBoxRepository mealBoxRepository, ICanteenRepository canteenRepository,
        IStudentRepository studentRepository, IEmployeeRepository employeeRepository,
        IProductRepository productRepository)
    {
        _mealBoxRepository = mealBoxRepository;
        _canteenRepository = canteenRepository;
        _studentRepository = studentRepository;
        _employeeRepository = employeeRepository;
        _productRepository = productRepository;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Index()
    {
        if (User.IsInRole("employee"))
        {
            Console.WriteLine(User.Identity.Name);
            ViewBag.cantine = _employeeRepository.GetEmployeeByEmail(User.Identity.Name).Canteen.Id;
        }

        return View(_mealBoxRepository.GetMealBoxes()
            .Where(m => m.StudentId == null).ToList());
    }

    [AllowAnonymous]
    public IActionResult BoxDetails(int id)
    {
        if (User.IsInRole("student"))
        {
            ViewBag.studentId = _studentRepository.GetStudentByEmail(User.Identity.Name).Id;
        }

        return View(_mealBoxRepository.GetMealBoxes()
            .First(m => m.Id == id));
    }

    [HttpGet]
    [Authorize(Roles = "employee")]
    public IActionResult Aanpassen(int id)
    {
        ViewBag.Canteens = _canteenRepository.GetCanteens().ToList();
        var m = _mealBoxRepository.GetMealBoxById(id);

        var vm = new MealBoxViewModel
        {
            MealBoxName = m.MealBoxName,
            City = m.City,
            PickupDateTime = m.PickupDateTime,
            ExpireTime = m.ExpireTime,
            EighteenPlus = m.EighteenPlus,
            Price = m.Price,
            CanteenId = m.CanteenId,
            StudentId = m.StudentId,
            ProductCheckBoxes = new List<CheckBoxItem>()
        };
        foreach (var p in _productRepository.GetProducts())
        {
            if (m.Products.Contains(p))
            {
                vm.ProductCheckBoxes.Add(new CheckBoxItem()
                {
                    Id = p.Id,
                    Name = p.Name,
                    IsChecked = true
                });
            }
            else
            {
                vm.ProductCheckBoxes.Add(new CheckBoxItem()
                {
                    Id = p.Id,
                    Name = p.Name,
                    IsChecked = false
                });
            }
        }

        return View(vm);
    }

    [HttpPost]
    [Authorize(Roles = "employee")]
    public IActionResult Aanpassen(MealBoxViewModel mealBoxVm)
    {
        if (mealBoxVm.StudentId != null) return RedirectToAction("Index");
        MealBox mealBox = new MealBox()
        {
            MealBoxName = mealBoxVm.MealBoxName,
            City = mealBoxVm.City,
            PickupDateTime = mealBoxVm.PickupDateTime,
            ExpireTime = mealBoxVm.ExpireTime,
            EighteenPlus = mealBoxVm.EighteenPlus,
            Price = mealBoxVm.Price,
            Type = mealBoxVm.Type,
            CanteenId = mealBoxVm.CanteenId,
            Products = new List<Product>()
        };

        foreach (var sp in mealBoxVm.selectedProducts)
        {
            mealBox.Products.Add(_productRepository.GetProductById(sp));
        }


        _mealBoxRepository.DeleteMealBox(_mealBoxRepository.GetMealBoxById(mealBoxVm.Id));
        mealBox.EighteenPlus = mealBox.Products.Any(m => m.ContainsAlcohol);
        _mealBoxRepository.UpdateMealBox(mealBox);
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "student")]
    public IActionResult Gereserveerd()
    {
        try
        {
            Console.WriteLine(User.Identity.Name);
            var studentId = _studentRepository.GetStudentByEmail(User.Identity.Name).Id;
            return View(_mealBoxRepository.GetMealBoxes()
                .Where(m => m.Student != null && m.Student.Id == studentId));
        }
        catch
        {
            return RedirectToAction("Index", "Maaltijdbox");
        }
    }

    [HttpGet]
    [Authorize(Roles = "employee")]
    public IActionResult Aanmaken()
    {
        ViewBag.Canteens = _canteenRepository.GetCanteens().ToList();
        var vm = new MealBoxViewModel()
        {
            PickupDateTime = DateTime.Now,
            ExpireTime = DateTime.Now.AddHours(2)
        };
        vm.ProductCheckBoxes = new List<CheckBoxItem>();
        foreach (var p in _productRepository.GetProducts())
        {
            vm.ProductCheckBoxes.Add(new CheckBoxItem()
            {
                Id = p.Id,
                Name = p.Name,
                IsChecked = false
            });
        }

        return View(vm);
    }

    [HttpPost]
    [Authorize(Roles = "employee")]
    public IActionResult Aanmaken(MealBoxViewModel mealBoxVm)
    {
        MealBox mealBox = new MealBox()
        {
            MealBoxName = mealBoxVm.MealBoxName,
            City = mealBoxVm.City,
            PickupDateTime = mealBoxVm.PickupDateTime,
            ExpireTime = mealBoxVm.ExpireTime,
            EighteenPlus = mealBoxVm.EighteenPlus,
            Price = mealBoxVm.Price,
            Type = mealBoxVm.Type,
            CanteenId = mealBoxVm.CanteenId,
            Products = new List<Product>()
        };

        foreach (var sp in mealBoxVm.selectedProducts)
        {
            var mb = _productRepository.GetProductById(sp);
            mealBox.Products.Add(mb);
            if (mb.ContainsAlcohol)
            {
                mealBox.EighteenPlus = true;
            }
        }

        _mealBoxRepository.AddMealBox(mealBox);
        return View("BoxDetails", mealBox);
    }

    [Authorize(Roles = "employee")]
    public IActionResult Verwijder(int id)
    {
        var m = _mealBoxRepository.GetMealBoxById(id);
        if (m.StudentId != null) return RedirectToAction("Index");
        _mealBoxRepository.DeleteMealBox(m);
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "student")]
    public IActionResult Reserveer(int mealBoxId, int studentId)
    {
        TempData["ErrorMessage"] = null;

        var m = _mealBoxRepository.GetMealBoxById(mealBoxId);
        var s = _studentRepository.GetStudentById(studentId);
        var age = m.PickupDateTime.Year - s.BirthDate.Year;
        if (s.BirthDate.Date > m.PickupDateTime.AddYears(-18) && m.EighteenPlus)
        {
            TempData["ErrorMessage"] = "U moet achttien zijn om deze maaltijdbox te reserveren";
            return RedirectToAction("BoxDetails", "Maaltijdbox", new { id = mealBoxId });
        }

        if (_mealBoxRepository.GetReservedMealBoxToday(studentId, m.PickupDateTime) != null)
        {
            TempData["ErrorMessage"] = "U heeft al een maaltijdbox voor deze dag gereserveerd";
            return RedirectToAction("BoxDetails", "Maaltijdbox", new { id = mealBoxId });
        }

        m.StudentId = s.Id;
        _mealBoxRepository.UpdateMealBox(m);
        return RedirectToAction("BoxDetails", "Maaltijdbox", new { id = mealBoxId });
    }

    [Authorize(Roles = "student")]
    public IActionResult ReserveerAnnuleer(int mealBoxId, int studentId)
    {
        var m = _mealBoxRepository.GetMealBoxById(mealBoxId);
        m.StudentId = null;
        _mealBoxRepository.UpdateMealBox(m);
        return RedirectToAction("BoxDetails", "Maaltijdbox", new { id = mealBoxId });
    }
}