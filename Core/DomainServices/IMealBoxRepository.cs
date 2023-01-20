using Core.Domain;

namespace Core.DomainServices;

public interface IMealBoxRepository
{
    
    IEnumerable<MealBox> GetMealBoxes();


    MealBox GetMealBoxById(int id);
    
    
    MealBox AddMealBox(MealBox mealBox);

    
    void UpdateMealBox(MealBox mealBox);

    bool DeleteMealBox(MealBox mealBox);

    MealBox? GetReservedMealBoxToday(int studentId, DateTime date);

    void DeleteMealBoxProducts(MealBox mealBox);

    public MealBox? ReserveMealBox(int mealBoxId, int studentId);


}