using Core.Domain;

namespace ApplicationServices;

public interface IMealBoxService
{
    public MealBox AddMealBox(MealBox mealBox, List<Product> products, int canteenId);

    public MealBox UpdateMealBox(MealBox mealBox, List<Product> products);

    public bool DeleteMealBox(int id);

    public bool ReserveMealBox(int mealBoxId, int studentId);

    public bool ReserveMealBoxCancel(int mealBoxId, int studentId);

    public IEnumerable<MealBox> GetMealBoxesReserved(int studentId);

    public IEnumerable<MealBox> GetMealBoxesNonReserved();


    public IEnumerable<MealBox> GetMealBoxesOwnCanteen(int canteenId);

    public IEnumerable<MealBox> GetMealBoxesOtherCanteens(int canteenId);
}