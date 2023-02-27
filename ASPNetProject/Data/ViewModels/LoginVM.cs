using System.ComponentModel.DataAnnotations;

namespace ASPNetProject.Models;

public class LoginVM
{
    [Required(ErrorMessage = "Wprowadź email")]
    [Display(Name = "Email")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Wprowadź hasło")]
    [Display(Name = "Hasło")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}