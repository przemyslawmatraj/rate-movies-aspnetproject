using ASPNetProject.Data.Base;
using ASPNetProject.Models;

namespace ASPNetProject.Data.Services;

public interface IMoviesService: IEntityBaseRepository<Movie>
{
    Task<Movie> GetMovieByIdAsync(int id);
    Task<MovieDropdown> GetMovieDropdownAsync();
    
    Task AddMovieAsync(NewMovieVM movie);
    
    Task UpdateMovieAsync(NewMovieVM movie);
}