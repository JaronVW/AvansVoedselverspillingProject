using Core.Domain;

namespace VoedselVerspillingWebApp.Models;

public class FormViewModel
{
    public List<Canteen> Canteens = new List<Canteen>();

    public MealBoxViewModel MealBoxViewModel;
}