using Core.Domain;
using Core.Domain.Exceptions;
using Core.DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VoedselVerspillingWebApp.Controllers;

public class MaaltijdBoxController : Controller
{
    private readonly IMealBoxRepository _mealBoxRepository;
    private readonly ICanteenRepository _canteenRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMealBoxUpdateMethods _mealBoxUpdateMethods;

    public MaaltijdBoxController(IMealBoxRepository mealBoxRepository, ICanteenRepository canteenRepository,
        IStudentRepository studentRepository, IEmployeeRepository employeeRepository,
        IMealBoxUpdateMethods mealBoxUpdateMethods)
    {
        _mealBoxRepository = mealBoxRepository;
        _canteenRepository = canteenRepository;
        _studentRepository = studentRepository;
        _employeeRepository = employeeRepository;
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
    public IActionResult Aanpassen(MealBoxViewModel mealBoxViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Canteens = _canteenRepository.GetCanteens().ToList();
                return View(_mealBoxUpdateMethods.updateMealBoxGet(mealBoxViewModel.Id));
            }

            TempData["ErrorMessage"] = null;
            if (_mealBoxUpdateMethods.updateMealBoxPost(mealBoxViewModel))
            {
                return RedirectToAction("Index");
            }

            ViewBag.Canteens = _canteenRepository.GetCanteens().ToList();
            return View(_mealBoxUpdateMethods.updateMealBoxGet(mealBoxViewModel.Id));
        }
        catch (InvalidFormdataException e)
        {
            ViewBag.Canteens = _canteenRepository.GetCanteens().ToList();
            ModelState.AddModelError("CustomError", e.Message);
            return View(_mealBoxUpdateMethods.updateMealBoxGet(mealBoxViewModel.Id));
        }
    }

    [Authorize(Roles = "student")]
    public IActionResult Gereserveerd()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (User == null) return RedirectToAction("Index", "MaaltijdBox");

        var studentId = _studentRepository.GetStudentByEmail(User.Identity.Name).Id;
        return View(_mealBoxRepository.GetMealBoxes()
            .Where(m => m.Student != null && m.Student.Id == studentId));
    }

    [HttpGet]
    [Authorize(Roles = "employee")]
    public IActionResult Aanmaken()
    {
        ViewBag.Canteens = _canteenRepository.GetCanteens().ToList();
        return View(_mealBoxUpdateMethods.formCreateViewModel());
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
                return View(_mealBoxUpdateMethods.formCreateViewModel());
            }

            var m = _mealBoxRepository.AddMealBox(mealBoxVm);
            return RedirectToAction("index");
        }
        catch (InvalidFormdataException e)
        {
            ModelState.AddModelError("CustomError", e.Message);
            ViewBag.Canteens = _canteenRepository.GetCanteens().ToList();
            return View(_mealBoxUpdateMethods.formCreateViewModel());
        }
        catch
        {
            ModelState.AddModelError("CustomError", "Er is iets mis gegaan bij het creëren van de Maaltijdbox");
            ViewBag.Canteens = _canteenRepository.GetCanteens().ToList();
            return View(_mealBoxUpdateMethods.formCreateViewModel());
        }
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

        Console.WriteLine(s.BirthDate.Date > m.PickupDateTime.AddYears(-18) && m.EighteenPlus);
        if (s.BirthDate.Date > m.PickupDateTime.AddYears(-18) && m.EighteenPlus)
        {
            TempData["ErrorMessage"] = "U moet achttien zijn om deze maaltijdbox te reserveren";
            return RedirectToAction("BoxDetails", "MaaltijdBox", new { id = mealBoxId });
        }

        if (_mealBoxRepository.GetReservedMealBoxToday(studentId, m.PickupDateTime) != null)
        {
            TempData["ErrorMessage"] = "U heeft al een maaltijdbox voor deze dag gereserveerd";
            return RedirectToAction("BoxDetails", "MaaltijdBox", new { id = mealBoxId });
        }

        m.StudentId = s.Id;
        _mealBoxRepository.UpdateMealBox(m);
        return RedirectToAction("BoxDetails", "MaaltijdBox", new { id = mealBoxId });
    }

    [Authorize(Roles = "student")]
    public IActionResult ReserveerAnnuleer(int mealBoxId, int studentId)
    {
        var m = _mealBoxRepository.GetMealBoxById(mealBoxId);
        m.StudentId = null;
        _mealBoxRepository.UpdateMealBox(m);
        return RedirectToAction("BoxDetails", "MaaltijdBox", new { id = mealBoxId });
    }
}