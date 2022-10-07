using Domain;

namespace Core.DomainServices;

public interface IMealBoxRepository
{
    IEnumerable<MealBox> GetMealBoxes();

    MealBox GetMealBoxById(int id);

    void AddMealBox(MealBox mealBox);

    void UpdateMealBox(MealBox mealBox);

    void DeleteMealBox(MealBox mealBox);
}