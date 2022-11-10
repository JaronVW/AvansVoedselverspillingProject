using VoedselVerspillingWebApp.Models;

namespace Core.DomainServices;

public interface IMealBoxUpdateMethods
{
    public MealBoxViewModel updateMealBoxGet(int id);

    public bool updateMealBoxPost(MealBoxViewModel mealBoxVm);
}