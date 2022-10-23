using System.ComponentModel.DataAnnotations;

namespace VoedselVerspillingWebApp.Models;

public class registerViewModel
{
    [Required] [EmailAddress] public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Wachtwoord Herhalen")]
    [Compare("Password",ErrorMessage = "Wachtwoorden komen niet overeen")]
    public string PasswordConfirm { get; set; } = null!;
}