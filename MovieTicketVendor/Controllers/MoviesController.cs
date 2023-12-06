using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieTicketVendor.Data;
using MovieTicketVendor.Data.Services;
using MovieTicketVendor.Data.ViewModels;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketVendor.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _movieService.GetAllAsync(c => c.Cinema);
            return View(data);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var moviesRetreived = await _movieService.GetAllAsync(c => c.Cinema);
            if (!string.IsNullOrEmpty(searchString))
            {
                var filter = moviesRetreived.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index", filter);
            }

            return View("Index", moviesRetreived);
        }

        //public async Task<IActionResult> Filter(string searchString)
        //{
        //    var moviesRetrieved = await _movieService.GetAllAsync(c => c.Cinema);

        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        var filter = moviesRetrieved
        //            .Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString))
        //            .ToList();

        //        // Return a JSON result with the filtered results
        //        return Json(new { html = RenderPartialViewToString("Index", filter) });
        //    }

        //    // Return a JSON result with the unfiltered results
        //    return Json(new { html = RenderPartialViewToString("Index", moviesRetrieved) });
        //}

        //private string RenderPartialViewToString(string viewName, object model)
        //{
        //    ViewData.Model = model;
        //    using (var sw = new StringWriter())
        //    {
        //        var engine = HttpContext.RequestServices.GetRequiredService<ICompositeViewEngine>();
        //        var viewResult = engine.FindView(ControllerContext, viewName, false);

        //        var viewContext = new ViewContext(
        //            ControllerContext,
        //            viewResult.View,
        //            ViewData,
        //            TempData,
        //            sw,
        //            new HtmlHelperOptions()
        //        );

        //        var t = viewResult.View.RenderAsync(viewContext);
        //        t.Wait();

        //        return sw.GetStringBuilder().ToString();
        //    }
        //}



        //Movies/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var retreivedMovie = await _movieService.GetMovieByIdAsync(id);

            if (retreivedMovie != null)
            {
                return View(retreivedMovie);
            }

            return View("NotFound");
        }

        public async Task<IActionResult> Create()
        {
            var newDropdownData = await _movieService.GetMovieDropdownValues();

            ViewBag.Cinemas = new SelectList(newDropdownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(newDropdownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(newDropdownData.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM newMovie)
        {
            if (!ModelState.IsValid)
            {
                return View(newMovie);
            }

            var newDropdownData = await _movieService.GetMovieDropdownValues();

            ViewBag.Cinemas = new SelectList(newDropdownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(newDropdownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(newDropdownData.Actors, "Id", "FullName");

            await _movieService.AddNewMovieAsync(newMovie);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var retreivedMovies = await _movieService.GetMovieByIdAsync(id);

            var movieContent = new NewMovieVM
            {
                Name = retreivedMovies.Name,
                Description = retreivedMovies.Description,
                Price = retreivedMovies.Price,
                ImageURL = retreivedMovies.ImageURL,
                StartDate = retreivedMovies.StartDate,
                EndDate = retreivedMovies.EndDate,
                MovieCategory = retreivedMovies.MovieCategory,
                ActorIds = retreivedMovies.Actor_Movie.Select(n => n.ActorId).ToList(),
                CinemaId = retreivedMovies.CinemaId,
                ProducerId = retreivedMovies.ProducerId
            };

            var newDropdownData = await _movieService.GetMovieDropdownValues();

            ViewBag.Cinemas = new SelectList(newDropdownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(newDropdownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(newDropdownData.Actors, "Id", "FullName");

            return View(movieContent);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM newMovie)
        {
            if (!ModelState.IsValid)
            {
                return View(newMovie);
            }

            var newDropdownData = await _movieService.GetMovieDropdownValues();

            ViewBag.Cinemas = new SelectList(newDropdownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(newDropdownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(newDropdownData.Actors, "Id", "FullName");

            await _movieService.UpdateMovieAsync(newMovie);

            return RedirectToAction(nameof(Index));
        }
    }
}
