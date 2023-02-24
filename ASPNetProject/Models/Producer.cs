using System.ComponentModel.DataAnnotations;
using ASPNetProject.Data.Base;

namespace ASPNetProject.Models
{
    public class Producer: IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Prosze podać imię i nazwisko")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Imię i nazwisko nie może być dłuższe niż 50 znaków i krótsze niż 3 znaki")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Prosze podać zdjęcie")]
        public string ProfileImage { get; set; }
        [Required(ErrorMessage = "Prosze podać biografię")]
        public string Bio { get; set; }


        public List<Movie>? Movies { get; set; }

    }
}
