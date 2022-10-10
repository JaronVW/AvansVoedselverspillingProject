using System.Collections;
using Core.DomainServices;
using Microsoft.AspNetCore.Mvc;

namespace VoedselVerspillingWebApp.Controllers;

public class MaaltijdboxController : Controller
{
    private readonly IMealBoxRepository _mealBoxRepository;

    public MaaltijdboxController(IMealBoxRepository repository)
    {
        _mealBoxRepository = repository;
    }
    
    // GET
    public IActionResult Index()
    {
        return View(_mealBoxRepository.GetMealBoxes());
    }

    public IActionResult Gereserveerd(int studentId)
    {
        return View();
    }

   
}