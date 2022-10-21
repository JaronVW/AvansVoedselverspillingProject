using System.ComponentModel.DataAnnotations;
using Domain;

namespace VoedselVerspillingWebApp.Models;

public class MealBoxViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Naam verplicht")]
    public string MealBoxName { get; set; }

    [Required(ErrorMessage = "Stad verplicht")]
    public City City { get; set; }

    [Required(ErrorMessage = "Ophaaldatum verplicht")]
    public DateTime PickupDateTime { get; set; }

    [Required(ErrorMessage = "Verloopdatum verplicht")]
    public DateTime ExpireTime { get; set; }

    public bool EighteenPlus { get; set; }

    [Required(ErrorMessage = "Prijs verplicht")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Type verplicht")]
    public MealType Type { get; set; }

    public Student? Student { get; set; }

    public int? StudentId { get; set; }
    

    public Canteen? Canteen { get; set; } = null!;

    [Required(ErrorMessage = "Kantine verplicht")]
    public int CanteenId { get; set; }

    public bool WarmMeals { get; set; } = true;
    
    public List<CheckBoxItem> ProductCheckBoxes { get; set; }
    
    public List<int> selectedProducts { get; set; }
}