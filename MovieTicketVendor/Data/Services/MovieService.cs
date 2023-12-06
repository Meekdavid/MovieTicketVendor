using Microsoft.EntityFrameworkCore;
using MovieTicketVendor.Data.Base;
using MovieTicketVendor.Data.Enums;
using MovieTicketVendor.Data.ViewModels;
using MovieTicketVendor.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MovieTicketVendor.Data.Services
{
    public class MovieService : EntityBaseRepository<Movie>, IMovieService
    {
        private readonly AppDbContext _appDbContext;
        public MovieService(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie
            {
                Name= data.Name,
                Description = data.Description,
                Price= data.Price,
                ImageURL= data.ImageURL,
                StartDate = data.StartDate,
                EndDate= data.EndDate,
                MovieCategory = data.MovieCategory,
                CinemaId= data.CinemaId,
                ProducerId= data.ProducerId,
                
                
            };

            await _appDbContext.Movies.AddAsync(newMovie);
            await _appDbContext.SaveChangesAsync();

            //Add Actors
            foreach(var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _appDbContext.Actors_Movies.AddAsync(newActorMovie);
            }

            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var retreivedMovie = await _appDbContext.Movies
                .Include(c => c.Cinema).Include(p => p.Producer)
                .Include(am => am.Actor_Movie).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return retreivedMovie;
        }

        public async Task<NewMovieDropdownVM> GetMovieDropdownValues()
        {
            var response = new NewMovieDropdownVM
            {
                Actors = await _appDbContext.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _appDbContext.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _appDbContext.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbMovie = await _appDbContext.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);

            if(dbMovie != null)
            {
               dbMovie.Name = data.Name;
               dbMovie.Description = data.Description;
               dbMovie.Price = data.Price;
               dbMovie.ImageURL = data.ImageURL;
               dbMovie.StartDate = data.StartDate;
               dbMovie.EndDate = data.EndDate;
               dbMovie.MovieCategory = data.MovieCategory;
               dbMovie.CinemaId = data.CinemaId;
               dbMovie.ProducerId = data.ProducerId;
            }

            await _appDbContext.SaveChangesAsync();

            //Remove Existing Actors
            var existingActorsInDB = await _appDbContext.Actors_Movies.Where(n => n.MovieId == data.Id).ToListAsync();
            _appDbContext.Actors_Movies.RemoveRange(existingActorsInDB);
            await _appDbContext.SaveChangesAsync();

            //Add Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _appDbContext.Actors_Movies.AddAsync(newActorMovie);
            }

            await _appDbContext.SaveChangesAsync();
        }
    }
}
