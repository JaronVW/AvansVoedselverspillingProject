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
    private readonly IMealBoxUpdateMethods _mealBoxUpdateMethods;

    public MaaltijdboxController(IMealBoxRepository mealBoxRepository, ICanteenRepository canteenRepository,
        IStudentRepository studentRepository, IEmployeeRepository employeeRepository,
        IProductRepository productRepository, IMealBoxUpdateMethods mealBoxUpdateMethods)
    {
        _mealBoxRepository = mealBoxRepository;
        _canteenRepository = canteenRepository;
        _studentRepository = studentRepository;
        _employeeRepository = employeeRepository;
        _productRepository = productRepository;
        _mealBoxUpdateMethods = mealBoxUpdateMethods;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Index()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (User != null && User.IsInRole("employee"))
        {
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
        return View(_mealBoxUpdateMethods.updateMealBoxGet(id));
    }

    [HttpPost]
    [Authorize(Roles = "employee")]
    public IActionResult Aanpassen(MealBoxViewModel mealBoxVm)
    {
        TempData["ErrorMessage"] = null;
        if (_mealBoxUpdateMethods.updateMealBoxPost(mealBoxVm)) return RedirectToAction("Index");
        TempData["ErrorMessage"] = "Deze maaltijd box is al gereserveerd";
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "student")]
    public IActionResult Gereserveerd()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (User == null) return RedirectToAction("Index", "Maaltijdbox");

        var studentId = _studentRepository.GetStudentByEmail(User.Identity.Name).Id;
        return View(_mealBoxRepository.GetMealBoxes()
            .Where(m => m.Student != null && m.Student.Id == studentId));
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
        TempData["ErrorMessage"] = null;
        if (mealBoxVm.WarmMeals && _canteenRepository.GetCanteenById(mealBoxVm.CanteenId).WarmMealsprovided != true)
        {
            TempData["ErrorMessage"] = "Warme maaltijden zijn niet beschikbaar in deze kantine";
            return RedirectToAction("Aanmaken");
        }
        return View("BoxDetails", _mealBoxRepository.AddMealBox(mealBoxVm));
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