using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketVendor.Data;
using MovieTicketVendor.Data.Services;
using MovieTicketVendor.Models;
using System.Threading.Tasks;

namespace MovieTicketVendor.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducerService _producerService;

        public ProducersController(IProducerService producerService)
        {
            _producerService= producerService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _producerService.GetAllAsync();
            return View(data);
        }

        //Producer/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var existingProducer = await _producerService.GetByIdAsync(id);
            if (existingProducer == null) return View("NotFound");
            return View(existingProducer);
        }

        // Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL, FullName, Bio")] Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);
            await _producerService.AddAsync(producer);

            return RedirectToAction(nameof(Index));
        }


        // Producers/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var existingProducer = await _producerService.GetByIdAsync(id);
            if (existingProducer == null) return View("NotFound");
            return View(existingProducer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("ProfilePictureURL, FullName, Bio")] Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);
            await _producerService.AddAsync(producer);

            return RedirectToAction(nameof(Index));
        }


        // Producers/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var existingProducer = await _producerService.GetByIdAsync(id);
            if (existingProducer == null) return View("NotFound");
            return View(existingProducer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            var existingProducer = await _producerService.GetByIdAsync(id);
            if (existingProducer == null) return View("NotFound");

            await _producerService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
