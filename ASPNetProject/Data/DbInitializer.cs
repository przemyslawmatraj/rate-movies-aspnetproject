using ASPNetProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNetProject.Data
{
    public class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
             using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
             {
                var context = serviceScope.ServiceProvider.GetService<Context>();

                context.Database.EnsureCreated();

                if (!context.Cinemas.Any())
                {
                    context.AddRange(new List<Cinema>() {

                        new Cinema() { Name = "Cinema 1", Logo = "https://picsum.photos/250", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit" },
                        new Cinema() { Name = "Cinema 2", Logo = "https://picsum.photos/250", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit2" },
                        new Cinema() { Name = "Cinema 3", Logo = "https://picsum.photos/250", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit3" },
                        new Cinema() { Name = "Cinema 4", Logo = "https://picsum.photos/250", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit4" },
                        new Cinema() { Name = "Cinema 5", Logo = "https://picsum.photos/250", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit5" }
                    });
                    context.SaveChanges();
                }

                if (!context.Actors.Any())
                {
                    context.AddRange(new List<Actor>()
                    {

                        new Actor() { FullName = "Actor 1", ProfileImage = "https://picsum.photos/250", Bio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit" },
                        new Actor() { FullName = "Actor 2", ProfileImage = "https://picsum.photos/250", Bio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit2" },
                        new Actor() { FullName = "Actor 3", ProfileImage = "https://picsum.photos/250", Bio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit3" },
                        new Actor() { FullName = "Actor 4", ProfileImage = "https://picsum.photos/250", Bio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit4" },
                        new Actor() { FullName = "Actor 5", ProfileImage = "https://picsum.photos/250", Bio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit5" }
                    });
                    context.SaveChanges();
                }

                if(!context.Producers.Any())
                {
                    context.AddRange(new List<Producer>() 
                    { 
                        new Producer()
                        {
                            FullName = "Andrzej Wajda",
                            Bio = "Lorem ipsum1",
                            ProfileImage = "https://picsum.photos/250"
                        },
                        new Producer()
                        {
                            FullName = "Steven Spielberg",
                            Bio = "Lorem ipsum2",
                            ProfileImage = "https://picsum.photos/250"
                        },
                        new Producer()
                        {
                            FullName = "Peter Jackson",
                            Bio = "Lorem ipsum3",
                            ProfileImage = "https://picsum.photos/250"
                        },
                        new Producer()
                        {
                            FullName = "James Cameron",
                            Bio = "Lorem ipsum4",
                            ProfileImage = "https://picsum.photos/250"
                        },
                        new Producer()
                        {
                            FullName = "Stanley Kubrick",
                            Bio = "Lorem ipsum5",
                            ProfileImage = "https://picsum.photos/250"
                        }
                    });
                    context.SaveChanges();
                }


                if (!context.Movies.Any())
                {

                    context.AddRange(new List<Movie>()
                    {
                       new Movie()
                        {
                            Title = "Movie 1",
                            Description = "Lorem ipsum dolor sit amet",
                            Price = 23.23,
                            Image = "https://picsum.photos/250",
                            StartDate = DateTime.Now.AddDays(1),
                            EndDate= DateTime.Now.AddDays(10),
                            CinemaId = 1,
                            ProducerId = 1,
                            MovieCategory = Enums.MovieCategory.SciFi
                        },
                        
                        new Movie()
                        {
                            Title = "Movie 2",
                            Description = "Lorem ipsum dolor sit amet2",
                            Price = 23.23,
                            Image = "https://picsum.photos/250",
                            StartDate = DateTime.Now.AddDays(-6),
                            EndDate= DateTime.Now.AddDays(5),
                            CinemaId = 2,
                            ProducerId = 3,
                            MovieCategory = Enums.MovieCategory.Comedy
                        },
                        
                        new Movie()
                        {
                            Title = "Movie 3",
                            Description = "Lorem ipsum dolor sit amet3",
                            Price = 23.23,
                            Image = "https://picsum.photos/250",
                            StartDate = DateTime.Now.AddDays(-3),
                            EndDate= DateTime.Now.AddDays(8),
                            CinemaId = 3,
                            ProducerId = 2,
                            MovieCategory = Enums.MovieCategory.Action
                        },
                        
                        new Movie()
                        {
                            Title = "Movie 4",
                            Description = "Lorem ipsum dolor sit amet4",
                            Price = 23.23,
                            Image = "https://picsum.photos/250",
                            StartDate = DateTime.Now.AddDays(-8),
                            EndDate= DateTime.Now.AddDays(3),
                            CinemaId = 4,
                            ProducerId = 4,
                            MovieCategory = Enums.MovieCategory.Drama
                        },
                        
                        new Movie()
                        {
                            Title = "Movie 5",
                            Description = "Lorem ipsum dolor sit amet5",
                            Price = 23.23,
                            Image = "https://picsum.photos/250",
                            StartDate = DateTime.Now.AddDays(-5),
                            EndDate= DateTime.Now.AddDays(6),
                            CinemaId = 5,
                            ProducerId = 5,
                            MovieCategory = Enums.MovieCategory.SciFi

                        },

                        new Movie()
                        {
                            Title = "Movie 6",
                            Description = "Lorem ipsum dolor sit amet6",
                            Price = 23.23,
                            Image = "https://picsum.photos/250",
                            StartDate = DateTime.Now.AddDays(-2),
                            EndDate= DateTime.Now.AddDays(15),
                            CinemaId = 3,
                            ProducerId = 3,
                            MovieCategory = Enums.MovieCategory.Crime
                        },

                        new Movie()
                        {
                            Title = "Movie 7",
                            Description = "Lorem ipsum dolor sit amet7",
                            Price = 23.23,
                            Image = "https://picsum.photos/250",
                            StartDate = DateTime.Now.AddDays(-9),
                            EndDate= DateTime.Now.AddDays(2),
                            CinemaId = 1,
                            ProducerId = 1,
                            MovieCategory = Enums.MovieCategory.SciFi
                        },
                        
                        new Movie()
                        {
                            Title = "Movie 8",
                            Description = "Lorem ipsum dolor sit amet8",
                            Price = 23.23,
                            Image = "https://picsum.photos/250",
                            StartDate = DateTime.Now.AddDays(-1),
                            EndDate= DateTime.Now.AddDays(4),
                            CinemaId = 2,
                            ProducerId = 2,
                            MovieCategory = Enums.MovieCategory.Action
                        },
                        
                        new Movie()
                        {
                            Title = "Movie 9",
                            Description = "Lorem ipsum dolor sit amet9",
                            Price = 23.23,
                            Image = "https://picsum.photos/250",
                            StartDate = DateTime.Now.AddDays(-4),
                            EndDate= DateTime.Now.AddDays(1),
                            CinemaId = 4,
                            ProducerId = 4,
                            MovieCategory = Enums.MovieCategory.Drama
                        }
                        
                    });
                    context.SaveChanges();
                }

                if (!context.ActorsMovies.Any())
                {
                    context.AddRange(new List<ActorMovie>() {

                         new ActorMovie()
                         {
                             ActorId = 1,
                             MovieId= 2,
                         },
                         new ActorMovie()
                         {
                             ActorId = 2,
                             MovieId= 2,
                         },
                         new ActorMovie()
                         {
                             ActorId = 3,
                             MovieId= 3,
                         },
                         new ActorMovie()
                         {
                             ActorId = 4,
                             MovieId= 4,
                         },
                         new ActorMovie()
                         {
                             ActorId = 5,
                             MovieId= 5,
                         }

                    });
                    context.SaveChanges();
                }
             }
        }
    }
}
