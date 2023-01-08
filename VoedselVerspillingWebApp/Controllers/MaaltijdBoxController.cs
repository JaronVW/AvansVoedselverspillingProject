﻿using Core.Domain;
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

            if (_mealBoxUpdateMethods.updateMealBoxPost(mealBoxViewModel))
                return RedirectToAction("Index");

            ModelState.AddModelError("CustomError", "Deze maaltijdbox is al gereserveerd");
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
        try
        {
            _mealBoxRepository.ReserveMealBox(mealBoxId, studentId);
            return RedirectToAction("BoxDetails", "MaaltijdBox", new { id = mealBoxId });
        }
        catch (InvalidReservationException e)
        {
            if (User.IsInRole("student"))
            {
                ViewBag.studentId = _studentRepository.GetStudentByEmail(User.Identity.Name).Id;
            }

            ModelState.AddModelError("CustomError", e.Message);
            return View("BoxDetails", _mealBoxRepository.GetMealBoxes()
                .First(m => m.Id == mealBoxId));
        }
    }

    [Authorize(Roles = "student")]
    public IActionResult ReserveerAnnuleer(int mealBoxId)
    {
        if (_mealBoxRepository.ReserveMealBoxCancel(mealBoxId))
            return RedirectToAction("BoxDetails", "MaaltijdBox", new { id = mealBoxId });

        ModelState.AddModelError("CustomError", "Er is iets mis gegaan met de reservering annuleren");
        if (User.IsInRole("student"))
        {
            ViewBag.studentId = _studentRepository.GetStudentByEmail(User.Identity.Name).Id;
        }
        return View("BoxDetails", _mealBoxRepository.GetMealBoxes()
            .First(m => m.Id == mealBoxId));
    }
}