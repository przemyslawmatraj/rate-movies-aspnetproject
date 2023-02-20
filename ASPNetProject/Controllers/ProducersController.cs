using ASPNetProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNetProject.Controllers
{
    public class ProducersController : Controller
    {
        private readonly Context _db;

        public ProducersController(Context db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var producers = await _db.Producers.ToListAsync();
            return View();
        }
    }
}
