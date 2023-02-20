using ASPNetProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNetProject.Controllers
{
    public class CinemasController : Controller
    {
        private readonly Context _db;

        public CinemasController(Context db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var cinemas = await _db.Cinemas.ToListAsync();
            return View();
        }
    }
}
