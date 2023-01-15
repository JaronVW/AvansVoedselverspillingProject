using Core.Domain;

namespace ApplicationServices;

public interface IMealBoxService
{
    public MealBox AddMealBox(MealBox mealBox, List<Product> products, int canteenId);

    public MealBox UpdateMealBox(MealBox mealBox, List<Product> products);

    public bool DeleteMealBox(int id);

    public bool ReserveMealBox(int mealBoxId, int studentId);
}