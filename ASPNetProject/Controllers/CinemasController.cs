using ASPNetProject.Data;
using ASPNetProject.Data.Services;
using ASPNetProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNetProject.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _db;
        public CinemasController(ICinemasService db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var cinemas = await _db.GetAllAsync();
            return View(cinemas);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var cinema = await _db.GetByIdAsync(id);
            if (cinema == null)
            {
                return View("NotFound");
            }
            return View(cinema);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Logo,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _db.AddAsync(cinema); 
            return RedirectToAction("Index");
            
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var cinema = await _db.GetByIdAsync(id);
            if (cinema == null)
            {
                return View("NotFound");
            }
            return View(cinema);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name,Logo,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            if (id != cinema.Id)
            {
                return View(cinema);
            }
            await _db.UpdateAsync(id, cinema);
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await _db.GetByIdAsync(id);
            if (cinema == null)
            {
                return View("NotFound");
            }
            return View(cinema);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteCinema(int id)
        {
            var cinema = await _db.GetByIdAsync(id);
            if (cinema == null)
            {
                return View("NotFound");
            }
            await _db.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
