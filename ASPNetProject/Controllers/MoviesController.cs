using System.Security.Claims;
using ASPNetProject.Data;
using ASPNetProject.Data.Services;
using ASPNetProject.Data.Static;
using ASPNetProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.EntityFrameworkCore;

namespace ASPNetProject.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _db;

        public MoviesController(IMoviesService db)
        {
            _db = db;
        }
        
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var movies = await _db.GetAllMovieAsync();
            return View(movies);
        }
        
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string prompt)
        {
            var movies = await _db.GetAllMovieAsync();
            if (!string.IsNullOrEmpty(prompt))
            {
                var filteredMovies = movies.Where(m => m.Title.ToLower().Contains(prompt.ToLower()));
                return View("Index", filteredMovies);
            }
            return View("Index", movies);
        }
        
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _db.GetMovieByIdAsync(id);
            return View(movie);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Create()
        {
            var dropdownData = await _db.GetMovieDropdownAsync();
            
            ViewBag.CinemasId = new SelectList(dropdownData.Cinemas, "Id", "Name");
            ViewBag.ProducersId = new SelectList(dropdownData.Producers, "Id", "FullName");
            ViewBag.ActorsId = new MultiSelectList(dropdownData.Actors, "Id", "FullName");
            
            return View();
        }
        
        [Authorize(Roles = UserRoles.Admin)]
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
        
        [Authorize(Roles = UserRoles.Admin)]
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
                ActorsIds = movie.ActorMovies.Select(n => n.ActorId).ToList(),
                ReviewsIds = movie.Reviews.Select(r => r.Id).ToList()
            };
            
            var dropdownData = await _db.GetMovieDropdownAsync();
            
            ViewBag.CinemasId = new SelectList(dropdownData.Cinemas, "Id", "Name", movie.CinemaId);
            ViewBag.ProducersId = new SelectList(dropdownData.Producers, "Id", "FullName", movie.ProducerId);
            ViewBag.ActorsId = new MultiSelectList(dropdownData.Actors, "Id", "FullName", movie.ActorMovies.Select(n => n.ActorId));
            
            return View(response);
        }
        
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
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
        
        
        public async Task<IActionResult> NewReview(int id)
        {
            var movie = await _db.GetMovieByIdAsync(id);
            ViewBag.Movie = movie;
            int averageRating = 0;
            if (movie.Reviews.Count > 0 && movie.Reviews != null) averageRating =  (int) movie.Reviews.Average(r => r.Rating);
            ViewBag.AverageRating = averageRating;
            return View();
        }
        public async Task<IActionResult> CreateReview(int id, NewReviewVM review)
        {
            var movie = await _db.GetMovieByIdAsync(id);
            
            if(movie == null)
            {
                return View("NotFound");
            }
            ViewBag.Movie = movie;
            int averageRating = 0;
            if (movie.Reviews.Count > 0 && movie.Reviews != null) averageRating = (int) movie.Reviews.Average(r => r.Rating);
            ViewBag.AverageRating = averageRating;
            if (!ModelState.IsValid)
            {
                return View("NewReview", review);
            }
            
            
            var newReview = new Review()
            {
                Rating = review.Rating,
                Text = review.Text,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                UserName =  User.FindFirstValue(ClaimTypes.Name),
                MovieId = movie.Id
            };
            await _db.AddReviewAsync(newReview);
            return RedirectToAction(nameof(Details), new {id = id});
        }
    }
}
