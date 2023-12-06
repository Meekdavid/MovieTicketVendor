using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketVendor.Data;
using MovieTicketVendor.Data.Services;
using MovieTicketVendor.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketVendor.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _actorService;

        public ActorsController(IActorsService actorService)
        {
            _actorService = actorService;
        }

        // Get: Actors/Index
        public async Task<IActionResult> Index()
        {
            var data = await _actorService.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ProfilePictureURL, Bio")]Actor actor)
        {
            if(!ModelState.IsValid)
            {
                return View(actor);
            }
            _actorService.AddAsync(actor);

            return RedirectToAction(nameof(Index));
        }

        // Get: Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _actorService.GetByIdAsync(id);

            if (actorDetails == null)
            {
                return View("NotFound");
            }

            return View(actorDetails);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _actorService.GetByIdAsync(id);

            if (actorDetails == null)
            {
                return View("NotFound");
            }

            return View(actorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }

            await _actorService.UpdateAsync(id, actor);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _actorService.GetByIdAsync(id);

            if (actorDetails == null)
            {
                return View("NotFound");
            }

            return View(actorDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            
            await _actorService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
