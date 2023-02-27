using System.ComponentModel.DataAnnotations;

namespace ASPNetProject.Models;

public class NewReviewVM
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Proszę wpisać treść recenzji")]
    public string Text { get; set; }
    [Required(ErrorMessage = "Proszę wpisać ocenę")]
    public int Rating { get; set; }
}