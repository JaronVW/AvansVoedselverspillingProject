using System.ComponentModel.DataAnnotations;

namespace VoedselVerspillingWebApp.Models;

public class loginViewModel
{
    [Required] [EmailAddress] public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Onthoud Mij")] public bool RememberUser { get; set; }
}