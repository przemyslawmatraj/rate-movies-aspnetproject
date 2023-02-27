using System.ComponentModel.DataAnnotations;

namespace ASPNetProject.Models;

public class RegisterVM
{
    [Required(ErrorMessage = "Wprowadź email")]
    [Display(Name = "Email")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Wprowadź hasło")]
    [MinLength(8, ErrorMessage = "Hasło musi mieć minimum 8 znaków")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,30}$", ErrorMessage = "Hasło musi zawierać minimum 1 dużą literę, 1 małą literę, 1 cyfrę i 1 znak specjalny i mieć od 8 do 15 znaków")]
    [Display(Name = "Hasło")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Display(Name = "Powtórz Hasło")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Hasła nie są takie same")]
    public string RepeatPassword { get; set; }
    
    [Display(Name = "Imię i nazwisko")]
    [Required(ErrorMessage = "Wprowadź imię i nazwisko")]
    public string FullName { get; set; }
    
}