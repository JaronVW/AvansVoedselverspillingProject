using Domain;

namespace Core.DomainServices;

public interface IMealBoxRepository
{
    IEnumerable<MealBox> GetMealBoxes();

    MealBox GetMealBoxById(int id);
}