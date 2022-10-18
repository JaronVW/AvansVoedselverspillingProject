using Domain;

namespace Core.DomainServices;

public interface IMealBoxRepository
{
    IEnumerable<MealBox> GetMealBoxes();
    Task<List<MealBox>> GetMealBoxesAsync();

    MealBox GetMealBoxById(int id);

    void  AddMealBox(MealBox mealBox);

    void UpdateMealBox(MealBox mealBox);

    void DeleteMealBox(MealBox mealBox);


}