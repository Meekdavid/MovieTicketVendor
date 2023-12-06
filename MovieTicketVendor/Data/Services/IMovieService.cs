using MovieTicketVendor.Data.Base;
using MovieTicketVendor.Data.ViewModels;
using MovieTicketVendor.Models;
using System.Threading.Tasks;

namespace MovieTicketVendor.Data.Services
{
    public interface IMovieService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownVM> GetMovieDropdownValues();
        Task AddNewMovieAsync(NewMovieVM data);
        Task UpdateMovieAsync(NewMovieVM data);
    }
}
