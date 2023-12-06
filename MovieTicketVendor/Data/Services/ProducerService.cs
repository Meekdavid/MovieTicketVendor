using MovieTicketVendor.Data.Base;
using MovieTicketVendor.Models;

namespace MovieTicketVendor.Data.Services
{
    public class ProducerService : EntityBaseRepository<Producer>, IProducerService
    {
        public ProducerService(AppDbContext context):base(context)
        {

        }
    }
}
