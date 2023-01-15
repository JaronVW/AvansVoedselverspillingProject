using Core.Domain;

namespace Core.DomainServices;

public interface IMealBoxUpdateMethods
{
    public MealBoxViewModel updateMealBoxGet(int id);

    public bool updateMealBoxPost(MealBoxViewModel mealBoxVm);

    public bool reserveMealBox(int mealBoxId, int studentId)
    {
        return true;
    }

    public MealBoxViewModel formCreateViewModel();

}