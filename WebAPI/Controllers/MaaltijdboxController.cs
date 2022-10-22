using Core.DomainServices;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MaaltijdboxController : ControllerBase
{
    private readonly IMealBoxRepository _mealBoxRepository;


    public MaaltijdboxController(IMealBoxRepository mealBoxRepository)
    {
        _mealBoxRepository = mealBoxRepository;
    }


    [HttpGet(Name = "GetMaaltijdBoxen")]
    public IEnumerable<MealBox> GetMaaltijdBoxen()
        => _mealBoxRepository.GetMealBoxes();


    [HttpGet("{id}", Name = "GetMaaltijdBox")]
    public MealBox GetMaaltijdBox(int id)
        => _mealBoxRepository.GetMealBoxById(id);


    [HttpDelete("{id}", Name = "DeleteMaaltijdBox")]
    public void DeleteMaaltijdBox(int id)
        => throw new NotImplementedException();
    // _mealBoxRepository.DeleteMealBoxById(id);


    [HttpPut(Name = "UpdateMaaltijdBox"), Authorize]
    public void UpdateMaaltijdBox(MealBox mealBox)
        => throw new NotImplementedException();
    // _mealBoxRepository.UpdateMealBox(mealBox);


    [HttpPost("{id}", Name = "ReserveerMaaltijdbox")]
    public void ReserveerMaaltijdbox(int id, int studentId)
        => throw new NotImplementedException();
    // _mealBoxRepository.ReserveMealBox(id, studentId);
}