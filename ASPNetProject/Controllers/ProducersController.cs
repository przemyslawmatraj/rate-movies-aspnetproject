using ASPNetProject.Data;
using ASPNetProject.Data.Services;
using ASPNetProject.Data.Static;
using ASPNetProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNetProject.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProducersController : Controller
    {
        private readonly IProducersService _db;

        public ProducersController(IProducersService db)
        {
            _db = db;
        }
        
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var producers = await _db.GetAllAsync();
            return View(producers);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var producer = await _db.GetByIdAsync(id);
            if (producer == null)
            {
                return View("NotFound");
            }

            return View(producer);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ProfileImage, Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }

            await _db.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var producer = await _db.GetByIdAsync(id);
            if (producer == null)
            {
                return View("NotFound");
            }
            return View(producer);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, ProfileImage, Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }

            if (id == producer.Id)
            {
                await _db.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }
            
            return View(producer);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var producer = await _db.GetByIdAsync(id);
            if (producer == null)
            {
                return View("NotFound");
            }
            return View(producer);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteProducent(int id)
        {
            var producer = await _db.GetByIdAsync(id);
            if (producer == null)
            {
                return View("NotFound");
            }
            await _db.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    } 
}
