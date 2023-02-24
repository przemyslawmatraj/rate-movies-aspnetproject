using ASPNetProject.Data;
using ASPNetProject.Data.Services;
using ASPNetProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNetProject.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorService _service;

        public ActorsController(IActorService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var actors = await _service.GetAllAsync();

            return View(actors);
        }
        
        public IActionResult Create()
        {
            return View("Create");
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ProfileImage, Bio")]Actor actor)
        {
            
            if (!ModelState.IsValid)
            {
                return View(actor);
            } 
            
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }
        
        
        public async Task<IActionResult> Details(int id)
        {
         
         var actor = await _service.GetByIdAsync(id);
         
         if (actor == null)
         {
             return View("NotFound");
         }
         
         return View(actor);
        }
        
        
        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _service.GetByIdAsync(id);
         
            if (actor == null)
            {
                return View("NotFound");
            }

            return View(actor);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id, FullName, ProfileImage, Bio")]Actor actor)
        {
            
            if (!ModelState.IsValid)
            {
                return View(actor);
            } 
            
            await _service.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }
        
        
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _service.GetByIdAsync(id);
         
            if (actor == null)
            {
                return View("NotFound");
            }

            return View(actor);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var actor = await _service.GetByIdAsync(id);
            if (actor == null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
