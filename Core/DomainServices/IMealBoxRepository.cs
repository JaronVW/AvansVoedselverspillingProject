using Core.Domain;

namespace Core.DomainServices;

public interface IMealBoxRepository
{
    IEnumerable<MealBox> GetMealBoxes();

    IEnumerable<MealBox> GetMealBoxesReserved(int studentId);
    Task<List<MealBox>> GetMealBoxesAsync();

    MealBox GetMealBoxById(int id);
    
    public MealBox GetMealBoxByIdDetached(int id);


    MealBox AddMealBox(MealBox mealBox, List<Product> products);

    MealBox AddMealBox(MealBox mealBox);

    void UpdateMealBox(MealBox mealBox, List<Product> products);
    
    void UpdateMealBox(MealBox mealBox);

    bool DeleteMealBox(MealBox mealBox);

    MealBox? GetReservedMealBoxToday(int studentId, DateTime date);

    void DeleteMealBoxProducts(MealBox mealBox);


    void DeleteMealBoxById(int id);
    void ReserveMealBox(int mealBoxId, int studentId);

    public bool ReserveMealBoxCancel(int mealBoxId);

    public IEnumerable<MealBox> GetMealBoxesOwnCanteen(int canteenId);

    public IEnumerable<MealBox> GetMealBoxesOtherCanteens(int canteenId);
}