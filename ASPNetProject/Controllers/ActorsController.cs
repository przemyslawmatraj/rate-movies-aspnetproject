using ASPNetProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNetProject.Controllers
{
    public class ActorsController : Controller
    {
        private readonly Context _db;

        public ActorsController(Context db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var actors = await _db.Actors.ToListAsync();

            return View(actors);
        }
    }
}
