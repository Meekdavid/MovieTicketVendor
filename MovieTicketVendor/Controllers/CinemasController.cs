using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketVendor.Data;
using MovieTicketVendor.Data.Services;
using MovieTicketVendor.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketVendor.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICenemaService _cenemaService;

        public CinemasController(ICenemaService cenemaService)
        {
            _cenemaService= cenemaService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _cenemaService.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo, Name, Description")]Cinema newCinema)
        {
            if (!ModelState.IsValid)
            {
                return View(newCinema);
            }

            await _cenemaService.AddAsync(newCinema);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var existingCinema = await _cenemaService.GetByIdAsync(id);

            if (existingCinema == null) return View("NotFound");
            return View(existingCinema);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var existingCinema = await _cenemaService.GetByIdAsync(id);

            if (existingCinema == null) return View("NotFound");
            return View(existingCinema);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(int id, Cinema updatedCinema)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedCinema);
            }            

            await _cenemaService.UpdateAsync(id, updatedCinema);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var existingCinema = await _cenemaService.GetByIdAsync(id);

            if (existingCinema == null) return View("NotFound");

            return View(existingCinema);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            var existingCinema = await _cenemaService.GetByIdAsync(id);

            if (existingCinema == null) return View("NotFound");

            await _cenemaService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
