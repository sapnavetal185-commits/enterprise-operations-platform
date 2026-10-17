using EnterpriseOperations.Models;
using Microsoft.EntityFrameworkCore;
namespace EnterpriseOperations.Data
{
    public class TravelHubDbContext : DbContext
    {
        public TravelHubDbContext(
            DbContextOptions<TravelHubDbContext> options)
            : base(options)
        {
        }
        public DbSet<Airport> Airports => Set<Airport>();
    }
}
