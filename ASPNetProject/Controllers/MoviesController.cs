using ASPNetProject.Data;
using ASPNetProject.Data.Services;
using ASPNetProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASPNetProject.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _db;

        public MoviesController(IMoviesService db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _db.GetAllAsync();
            return View(movies);
        }
        
        public async Task<IActionResult> Filter(string prompt)
        {
            var movies = await _db.GetAllAsync();
            if (!string.IsNullOrEmpty(prompt))
            {
                var filteredMovies = movies.Where(m => m.Title.ToLower().Contains(prompt.ToLower()));
                return View("Index", filteredMovies);
            }
            return View("Index", movies);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _db.GetMovieByIdAsync(id);
            return View(movie);
        }

        public async Task<IActionResult> Create()
        {
            var dropdownData = await _db.GetMovieDropdownAsync();
            
            ViewBag.CinemasId = new SelectList(dropdownData.Cinemas, "Id", "Name");
            ViewBag.ProducersId = new SelectList(dropdownData.Producers, "Id", "FullName");
            ViewBag.ActorsId = new MultiSelectList(dropdownData.Actors, "Id", "FullName");
            
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var dropdownData = await _db.GetMovieDropdownAsync();
                ViewBag.CinemasId = new SelectList(dropdownData.Cinemas, "Id", "Name");
                ViewBag.ProducersId = new SelectList(dropdownData.Producers, "Id", "FullName");
                ViewBag.ActorsId = new MultiSelectList(dropdownData.Actors, "Id", "FullName");
                return View(movie);
            }
            await _db.AddMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _db.GetMovieByIdAsync(id);
            
            if (movie == null)
            {
                return View("NotFound");
            }
            
            var response = new NewMovieVM()
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Price = movie.Price,
                Image = movie.Image,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                MovieCategory = movie.MovieCategory,
                CinemaId = movie.CinemaId,
                ProducerId = movie.ProducerId,
                ActorsIds = movie.ActorMovies.Select(n => n.ActorId).ToList()
            };
            
            var dropdownData = await _db.GetMovieDropdownAsync();
            
            ViewBag.CinemasId = new SelectList(dropdownData.Cinemas, "Id", "Name", movie.CinemaId);
            ViewBag.ProducersId = new SelectList(dropdownData.Producers, "Id", "FullName", movie.ProducerId);
            ViewBag.ActorsId = new MultiSelectList(dropdownData.Actors, "Id", "FullName", movie.ActorMovies.Select(n => n.ActorId));
            
            return View(response);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if (id != movie.Id)
            {
                return View("NotFound");
            }
            if (!ModelState.IsValid)
            {
                var dropdownData = await _db.GetMovieDropdownAsync();
                ViewBag.CinemasId = new SelectList(dropdownData.Cinemas, "Id", "Name", movie.CinemaId);
                ViewBag.ProducersId = new SelectList(dropdownData.Producers, "Id", "FullName", movie.ProducerId);
                ViewBag.ActorsId = new MultiSelectList(dropdownData.Actors, "Id", "FullName", movie.ActorsIds);
                return View(movie);
            }
            await _db.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
    }
}
