 using ASPNetProject.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using ASPNetProject.Data.Base;

 namespace ASPNetProject.Models
{
    public class NewMovieVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Movie Category is required")]
        public MovieCategory MovieCategory { get; set; }

        [Required(ErrorMessage = "Cinema is required")]
        public List<int>? ActorsIds { get; set; }
        public List<int>? ReviewsIds { get; set; }
        
        [Required(ErrorMessage = "Cinema is required")]
        public int CinemaId { get; set; }

        [Required(ErrorMessage = "Producer is required")]
        public int ProducerId { get; set; }
    }
}
