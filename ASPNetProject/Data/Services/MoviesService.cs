using ASPNetProject.Data.Base;
using ASPNetProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNetProject.Data.Services;

public class MoviesService: EntityBaseRepository<Movie>, IMoviesService
{
    private readonly Context _context;
    public MoviesService(Context context) : base(context)
    {
        _context = context;
    }

    public async Task<Movie> GetMovieByIdAsync(int id)
    {
        var result = await _context.Movies
            .Include(c => c.Cinema)
            .Include(p => p.Producer)
            .Include(am => am.ActorMovies)
            .ThenInclude(a => a.Actor)
            .FirstOrDefaultAsync(n => n.Id == id);
        return result;
    }

    public async Task<MovieDropdown> GetMovieDropdownAsync()
    {
        var result = new MovieDropdown()
        {
            Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync(),
            Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
            Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync()
        };

        return result;
    }

    public async  Task AddMovieAsync(NewMovieVM movie)
    {
        var newMovie = new Movie()
        {
            Title = movie.Title,
            Description = movie.Description,
            Price = movie.Price,
            Image = movie.Image,
            StartDate = movie.StartDate,
            EndDate = movie.EndDate,
            MovieCategory = movie.MovieCategory,
            CinemaId = movie.CinemaId,
            ProducerId = movie.ProducerId
        };
        await _context.Movies.AddAsync(newMovie);
        await _context.SaveChangesAsync();
        
        foreach (var actorId in movie.ActorsIds)
        {
            var actorMovie = new ActorMovie()
            {
                ActorId = actorId,
                MovieId = newMovie.Id
            };
            await _context.ActorsMovies.AddAsync(actorMovie);
        }
        await _context.SaveChangesAsync();
    }
    

    public async Task UpdateMovieAsync(NewMovieVM movie)
    {
        var movieFromDb = await _context.Movies
            .Include(am => am.ActorMovies)
            .FirstOrDefaultAsync(n => n.Id == movie.Id);
        
        if (movieFromDb == null)
        {
            return;
        }
        
        movieFromDb.Title = movie.Title;
        movieFromDb.Description = movie.Description;
        movieFromDb.Price = movie.Price;
        movieFromDb.Image = movie.Image;
        movieFromDb.StartDate = movie.StartDate;
        movieFromDb.EndDate = movie.EndDate;
        movieFromDb.MovieCategory = movie.MovieCategory;
        movieFromDb.CinemaId = movie.CinemaId;
        movieFromDb.ProducerId = movie.ProducerId;
        await _context.SaveChangesAsync();
        
        var actorMovies = await _context.ActorsMovies.Where(n => n.MovieId == movie.Id).ToListAsync();
        _context.ActorsMovies.RemoveRange(actorMovies);
        await _context.SaveChangesAsync();
        
        foreach (var actorId in movie.ActorsIds)
        {
            var actorMovie = new ActorMovie()
            {
                ActorId = actorId,
                MovieId = movie.Id
            };
            await _context.ActorsMovies.AddAsync(actorMovie);
        }
        await _context.SaveChangesAsync();
    }
}