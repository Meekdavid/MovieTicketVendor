using Microsoft.EntityFrameworkCore;
using MovieTicketVendor.Data.Base;
using MovieTicketVendor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTicketVendor.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(AppDbContext context) : base(context) { }        
        
    }
}
