using System.ComponentModel.DataAnnotations;

namespace ASPNetProject.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public string Bio { get; set; }


        public List<Movie> Movies { get; set; }

    }
}
