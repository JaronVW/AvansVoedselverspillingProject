using Core.DomainServices;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace VoedselVerspillingWebApp.Controllers;

public class MaaltijdboxController : Controller
{
    private readonly IMealBoxRepository _mealBoxRepository;
    private readonly ICanteenRepository _canteenRepository;

    public MaaltijdboxController(IMealBoxRepository mealBoxRepository, ICanteenRepository canteenRepository)
    {
        _mealBoxRepository = mealBoxRepository;
        _canteenRepository = canteenRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(_mealBoxRepository.GetMealBoxes()
            .Where(m => m.StudentId == null).ToList());
    }

    public IActionResult BoxDetails(int id)
    {
        return View(_mealBoxRepository.GetMealBoxes()
            .First(m => m.Id == id));
    }

    [HttpGet]
    public IActionResult Aanpassen(int id)
    {
        ViewBag.Canteens = _canteenRepository.GetCanteens().ToList();
        return View(_mealBoxRepository.GetMealBoxes()
            .First(m => m.Id == id));
    }

    [HttpPost]
    public IActionResult Aanpassen(MealBox mealBox)
    {
        _mealBoxRepository.UpdateMealBox(mealBox);
        return RedirectToAction("Index");
    }

    public IActionResult Gereserveerd(int studentId)
    {
        return View(_mealBoxRepository.GetMealBoxes()
            .Where(m => m.Student.Id == studentId));
    }

    [HttpGet]
    public IActionResult Aanmaken()
    {
        ViewBag.Canteens = _canteenRepository.GetCanteens().ToList();
        return View();
    }

    [HttpPost]
    public IActionResult Aanmaken(MealBox mealBox)
    {
        _mealBoxRepository.AddMealBox(mealBox);
        return View("BoxDetails", mealBox);
    }

    public IActionResult Verwijder(int id)
    {
        _mealBoxRepository.DeleteMealBox(_mealBoxRepository.GetMealBoxById(id));
        return RedirectToAction("Index");
    }
}