using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ASPNetProject.Data.Base;

namespace ASPNetProject.Models
{
    public class Review : IEntityBase
    {
        [Key] 
        public int Id { get; set; }
        [Required(ErrorMessage = "Proszę wpisać treść recenzji")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Proszę wpisać ocenę")]
        public int Rating { get; set; }
        
        public string UserId { get; set; }
        public string UserName { get; set; }


        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }
    }
}