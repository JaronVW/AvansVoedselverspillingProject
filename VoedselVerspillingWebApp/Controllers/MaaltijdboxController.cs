using Core.DomainServices;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace VoedselVerspillingWebApp.Controllers;

public class MaaltijdboxController : Controller
{
    private readonly IMealBoxRepository _mealBoxRepository;

    public MaaltijdboxController(IMealBoxRepository repository)
    {
        _mealBoxRepository = repository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(_mealBoxRepository.GetMealBoxes()
            .Where(m => m.Student == null));
    }

    public IActionResult BoxDetails(int id)
    {
        return View(_mealBoxRepository.GetMealBoxes()
            .Where(m => m.Id == id));
    }

    [HttpGet]
    public IActionResult Aanpassen()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Aanpassen(MealBox mealBox)
    {
        return View("Index");
    }

    public IActionResult Gereserveerd(int studentId)
    {
        return View(_mealBoxRepository.GetMealBoxes()
            .Where(m => m.Student.Id == studentId));
    }
}