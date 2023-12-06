using MovieTicketVendor.Data.Base;
using MovieTicketVendor.Models;

namespace MovieTicketVendor.Data.Services
{
    public class CenemaService : EntityBaseRepository<Cinema>, ICenemaService
    {
        public CenemaService(AppDbContext appDbContext):base(appDbContext)
        {

        }
    }
}
