using Core.Domain;
using Domain;
using VoedselVerspillingWebApp.Models;

namespace Core.DomainServices;

public interface IMealBoxRepository
{
    IEnumerable<MealBox> GetMealBoxes();
    Task<List<MealBox>> GetMealBoxesAsync();

    MealBox GetMealBoxById(int id);

    MealBox AddMealBox(MealBoxViewModel mealBoxVm);

    void UpdateMealBox(MealBox mealBox);

    void DeleteMealBox(MealBox mealBox);

    MealBox? GetReservedMealBoxToday(int studentId, DateTime date);

    void DeleteMealBoxProducts(MealBox mealBox);


    void DeleteMealBoxById(int id);
    void ReserveMealBox(int mealBoxId, int studentId);

    public bool ReserveMealBoxCancel(int mealBoxId);
}