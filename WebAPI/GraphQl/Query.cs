using System.Reflection.Metadata.Ecma335;
using Core.DomainServices;
using Domain;
using Infrastructure;

namespace WebAPI.GraphQl;

public class Query
{
    private readonly IMealBoxRepository _mealBoxRepository;

    public Query(IMealBoxRepository mealBoxRepository)
    {
        _mealBoxRepository = mealBoxRepository;
    }

    public IEnumerable<MealBox> GetMaaltijdBoxen()
        => _mealBoxRepository.GetMealBoxes();

    public MealBox GetMaaltijdBoxById(int id)
        => _mealBoxRepository.GetMealBoxById(id);
}