using ASPNetProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNetProject.Controllers
{
    public class MoviesController : Controller
    {
        private readonly Context _db;

        public MoviesController(Context db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var cinemas = await _db.Movies.Include(n => n.Cinema).ToListAsync();
            return View(cinemas);
        }
    }
}
