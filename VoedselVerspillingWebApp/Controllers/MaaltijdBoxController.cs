using ApplicationServices;
using Core.Domain;
using Core.Domain.Exceptions;
using Core.DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoedselVerspillingWebApp.extensions;

namespace VoedselVerspillingWebApp.Controllers;

public class MaaltijdBoxController : Controller
{
    private readonly IMealBoxRepository _mealBoxRepository;
    private readonly ICanteenRepository _canteenRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMealBoxService _mealBoxService;

    public MaaltijdBoxController(IMealBoxRepository mealBoxRepository, ICanteenRepository canteenRepository,
        IStudentRepository studentRepository, IEmployeeRepository employeeRepository,
        IProductRepository productRepository,
        IMealBoxService mealBoxService)
    {
        _mealBoxRepository = mealBoxRepository;
        _canteenRepository = canteenRepository;
        _studentRepository = studentRepository;
        _employeeRepository = employeeRepository;
        _productRepository = productRepository;
        _mealBoxService = mealBoxService;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Index()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (User == null || !User.IsInRole("employee")) return View(_mealBoxService.GetMealBoxesNonReserved().ToList());

        var employee = _employeeRepository.GetEmployeeByEmail(User.Identity.Name);
        ViewBag.cantine = employee.Canteen.Id;
        return View(_mealBoxService.GetMealBoxesOwnCanteen(employee.CanteenId).ToList());
    }

    public IActionResult AndereKantines()
    {
        var employee = _employeeRepository.GetEmployeeByEmail(User.Identity.Name);
        ViewBag.cantine = employee.Canteen.Id;
        return View(_mealBoxService.GetMealBoxesOtherCanteens(employee.CanteenId).ToList());
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
        try
        {
            ViewBag.Canteens = _canteenRepository.GetCanteens().ToList();
            return View(
                MealBoxViewModeleExtension.updateMealBoxGet(_mealBoxRepository, id, _productRepository.GetProducts()));
        }
        catch
        {
            if (User != null && User.IsInRole("employee"))
            {
                ViewBag.cantine = _employeeRepository.GetEmployeeByEmail(User.Identity.Name).Canteen.Id;
            }

            return View("Index", _mealBoxService.GetMealBoxesNonReserved().ToList());
        }
    }

    [HttpPost]
    [Authorize(Roles = "employee")]
    public IActionResult Aanpassen(MealBoxViewModel mealBoxVm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Canteens = _canteenRepository.GetCanteens().ToList();
                return View(
                    MealBoxViewModeleExtension.updateMealBoxGet(_mealBoxRepository, mealBoxVm.Id,
                        _productRepository.GetProducts()));
            }

            var products = new List<Product>();
            if (mealBoxVm.SelectedProducts != null)
                products.AddRange(mealBoxVm.SelectedProducts.Select(sp => _productRepository.GetProductById(sp)));
            var employee = _employeeRepository.GetEmployeeByEmail(User.Identity.Name);
            _mealBoxService.UpdateMealBox(new MealBox()
            {
                Id = mealBoxVm.Id,
                MealBoxName = mealBoxVm.MealBoxName,
                City = employee.Canteen.City,
                PickupDateTime = mealBoxVm.PickupDateTime,
                ExpireTime = mealBoxVm.ExpireTime,
                EighteenPlus = mealBoxVm.EighteenPlus,
                Price = mealBoxVm.Price,
                Type = mealBoxVm.Type,
                CanteenId = employee.CanteenId,
                Products = new List<Product>()
            }, products);
            return RedirectToAction("Index");
        }
        catch (InvalidFormdataException e)
        {
            ViewBag.Canteens = _canteenRepository.GetCanteens().ToList();
            ModelState.AddModelError("CustomError", e.Message);
            return View(
                MealBoxViewModeleExtension.updateMealBoxGet(_mealBoxRepository, mealBoxVm.Id,
                    _productRepository.GetProducts()));
        }
    }

    [Authorize(Roles = "student")]
    public IActionResult Gereserveerd()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (User == null) return RedirectToAction("Index", "MaaltijdBox");

        var studentId = _studentRepository.GetStudentByEmail(User.Identity.Name).Id;
        return View(_mealBoxService.GetMealBoxesReserved(studentId));
    }

    [HttpGet]
    [Authorize(Roles = "employee")]
    public IActionResult Aanmaken()
    {
        ViewBag.EmployeeCanteen = _employeeRepository.GetEmployeeByEmail(User.Identity.Name).Canteen;
        ViewBag.Canteens = _canteenRepository.GetCanteens().ToList();
        return View(MealBoxViewModeleExtension.formCreateViewModel(_productRepository.GetProducts()));
    }

    [HttpPost]
    [Authorize(Roles = "employee")]
    public IActionResult Aanmaken(MealBoxViewModel mealBoxVm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Canteens = _canteenRepository.GetCanteens().ToList();
                return View(MealBoxViewModeleExtension.formCreateViewModel(_productRepository.GetProducts()));
            }

            var products = new List<Product>();
            if (mealBoxVm.SelectedProducts != null)
                products.AddRange(mealBoxVm.SelectedProducts.Select(sp => _productRepository.GetProductById(sp)));
            var employee = _employeeRepository.GetEmployeeByEmail(User.Identity.Name);
            var m = _mealBoxService.AddMealBox(new MealBox()
            {
                MealBoxName = mealBoxVm.MealBoxName,
                City = employee.Canteen.City,
                PickupDateTime = mealBoxVm.PickupDateTime,
                ExpireTime = mealBoxVm.ExpireTime,
                EighteenPlus = false,
                Price = mealBoxVm.Price,
                Type = mealBoxVm.Type,
                CanteenId = employee.CanteenId,
                Products = new List<Product>(),
                WarmMeals = mealBoxVm.WarmMeals,
            }, products, employee.CanteenId);
            return RedirectToAction("index");
        }
        catch (InvalidFormdataException e)
        {
            ModelState.AddModelError("CustomError", e.Message);
            ViewBag.Canteens = _canteenRepository.GetCanteens().ToList();
            return View(MealBoxViewModeleExtension.formCreateViewModel(_productRepository.GetProducts()));
        }
        catch
        {
            ModelState.AddModelError("CustomError", "Er is iets mis gegaan bij het creëren van de Maaltijdbox");
            ViewBag.Canteens = _canteenRepository.GetCanteens().ToList();
            return View(MealBoxViewModeleExtension.formCreateViewModel(_productRepository.GetProducts()));
        }
    }

    [Authorize(Roles = "employee")]
    public IActionResult Verwijder(int id)
    {
        if (!_mealBoxService.DeleteMealBox(id))
            ModelState.AddModelError("CustomError", "kan de maaltijdbox niet verwijderen");

        if (User == null || !User.IsInRole("employee"))
            return View("Index", _mealBoxService.GetMealBoxesNonReserved().ToList());

        var employee = _employeeRepository.GetEmployeeByEmail(User.Identity.Name);
        ViewBag.cantine = employee.Canteen.Id;
        return View("Index", _mealBoxService.GetMealBoxesOwnCanteen(employee.CanteenId).ToList());
    }

    [Authorize(Roles = "student")]
    public IActionResult Reserveer(int mealBoxId, int studentId)
    {
        try
        {
            if (!_mealBoxService.ReserveMealBox(mealBoxId, studentId))
                ModelState.AddModelError("CustomError", "Deze maaltijdbox is niet meer beschikbaar");

            if (User.IsInRole("student"))
            {
                ViewBag.studentId = _studentRepository.GetStudentByEmail(User.Identity.Name).Id;
            }

            return RedirectToAction("BoxDetails", new { id = mealBoxId });
        }
        catch (InvalidReservationException e)
        {
            if (User.IsInRole("student"))
            {
                ViewBag.studentId = _studentRepository.GetStudentByEmail(User.Identity.Name).Id;
            }

            ModelState.AddModelError("CustomError", e.Message);
            return View("BoxDetails", _mealBoxService.GetMealBoxesNonReserved()
                .First(m => m.Id == mealBoxId));
        }
    }

    [Authorize(Roles = "student")]
    public IActionResult ReserveerAnnuleer(int mealBoxId)
    {
        var student = _studentRepository.GetStudentByEmail(User.Identity.Name);
        if (!_mealBoxService.ReserveMealBoxCancel(mealBoxId, student.Id))
            ModelState.AddModelError("CustomError",
                "Er is iets mis gegaan met de reservering annuleren. Maaltijdbox is door iemand anders gereserveerd, of is niet meer beschikbaar.");

        ViewBag.studentId = student.Id;
        return View("BoxDetails", _mealBoxRepository.GetMealBoxById(mealBoxId));
    }
}