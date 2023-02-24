using System.ComponentModel.DataAnnotations;
using ASPNetProject.Data.Base;

namespace ASPNetProject.Models
{
    public class Cinema: IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string Logo { get; set; }
        [Required]
        public string Description { get; set; }


        public List<Movie>? Movies { get; set; }
    }
}
