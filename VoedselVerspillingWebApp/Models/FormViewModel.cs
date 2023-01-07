using Core.Domain;
using Domain;

namespace VoedselVerspillingWebApp.Models;

public class AanpassenViewModel
{
    public List<Canteen> Canteens = new List<Canteen>();

    public MealBoxViewModel MealBox;
}