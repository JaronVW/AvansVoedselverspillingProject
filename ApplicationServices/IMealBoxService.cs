


using Core.Domain;
using Domain;

public interface IMealBoxService
{
    public MealBox AddMealBox(MealBox mealBox, List<Product> products);
    
    public MealBoxViewModel UpdateMealBoxGet(int id);
    
    public MealBox UpdateMealBox(MealBox mealBox, List<Product> products);

}