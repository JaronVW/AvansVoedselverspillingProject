using System.Net;
using Core.Domain;
using Core.Domain.Exceptions;
using Core.DomainServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json", "application/xml")]
public class MaaltijdboxController : ControllerBase
{
    private readonly IMealBoxRepository _mealBoxRepository;


    public MaaltijdboxController(IMealBoxRepository mealBoxRepository)
    {
        _mealBoxRepository = mealBoxRepository;
    }


    [HttpGet(Name = "GetMaaltijdBoxen")]
    public IEnumerable<MealBox> GetMaaltijdBoxen()
        => _mealBoxRepository.GetMealBoxes().Where(box => box.StudentId == null);


    [HttpGet("{Id}", Name = "GetMaaltijdBox")]
    public MealBox GetMaaltijdBox([FromRoute] int Id)
        => _mealBoxRepository.GetMealBoxById(Id);


    [HttpPatch("{mealBoxId}", Name = "ReserveerMaaltijdBox")]
    public IActionResult ReserveerMaaltijdBox(int mealBoxId, [FromBody] studentIdDto studentIdDto)
    {
        try
        {
            return Ok(_mealBoxRepository.ReserveMealBox(mealBoxId, studentIdDto.studentId));
        }
        catch (NullReferenceException e)
        {
            return NotFound();
        }
        catch (InvalidReservationException e)
        {
            return BadRequest();
        }catch (Exception e)
        {
            return StatusCode(500);
        }
    }


    [HttpDelete("{Id}", Name = "DeleteMaaltijdBox")]
    public void DeleteMaaltijdBox(int id)
        => throw new NotImplementedException();
    // _mealBoxRepository.DeleteMealBoxById(Id);


    [HttpPut(Name = "UpdateMaaltijdBox"), Authorize]
    public void UpdateMaaltijdBox([FromBody] MealBox mealBox)
        => throw new NotImplementedException();
    // _mealBoxRepository.UpdateMealBox(mealBox);
}