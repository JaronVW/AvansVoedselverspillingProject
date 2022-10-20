using Core.DomainServices;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VoedselVerspillingWebApp.Controllers;

public class MaaltijdboxController : Controller
{
    private readonly IMealBoxRepository _mealBoxRepository;
    private readonly ICanteenRepository _canteenRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public MaaltijdboxController(IMealBoxRepository mealBoxRepository, ICanteenRepository canteenRepository,
        IStudentRepository studentRepository, IEmployeeRepository employeeRepository)
    {
        _mealBoxRepository = mealBoxRepository;
        _canteenRepository = canteenRepository;
        _studentRepository = studentRepository;
        _employeeRepository = employeeRepository;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Index()
    {
        if (User.IsInRole("employee"))
        {
            ViewBag.cantine = _employeeRepository.GetEmployeeByEmail(User.Identity.Name).Canteen.Id;
        }

        return View(_mealBoxRepository.GetMealBoxes()
            .Where(m => m.StudentId == null).ToList());
    }

    [AllowAnonymous]
    public IActionResult BoxDetails(int id)
    {
        ViewBag.studentId = _studentRepository.GetStudentByEmail(User.Identity.Name).Id;
        return View(_mealBoxRepository.GetMealBoxes()
            .First(m => m.Id == id));
    }

    [HttpGet]
    [Authorize(Roles = "employee")]
    public IActionResult Aanpassen(int id)
    {
        ViewBag.Canteens = _canteenRepository.GetCanteens().ToList();
        return View(_mealBoxRepository.GetMealBoxes()
            .First(m => m.Id == id));
    }

    [HttpPost]
    [Authorize(Roles = "employee")]
    public IActionResult Aanpassen(MealBox mealBox)
    {
        if (mealBox.StudentId != null)
        {
            _mealBoxRepository.UpdateMealBox(mealBox);
            return RedirectToAction("Index");
        }

        return RedirectToAction("Index");
    }

    [Authorize(Roles = "student")]
    public IActionResult Gereserveerd()
    {
        try
        {
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
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "employee")]
    public IActionResult Aanmaken(MealBox mealBox)
    {
        _mealBoxRepository.AddMealBox(mealBox);
        return View("BoxDetails", mealBox);
    }

    [Authorize(Roles = "employee")]
    public IActionResult Verwijder(int id)
    {
        var m = _mealBoxRepository.GetMealBoxById(id);
        if (m.StudentId == null) return RedirectToAction("Index");
        _mealBoxRepository.DeleteMealBox(m);
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "student")]
    public IActionResult Reserveer(int mealBoxId, int studentId)
    {
        var m = _mealBoxRepository.GetMealBoxById(mealBoxId);
        m.StudentId = studentId;
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