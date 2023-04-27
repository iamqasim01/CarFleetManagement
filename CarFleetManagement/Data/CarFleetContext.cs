using CarFleetManagement_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CarFleetManagement_API.Data
{
    public class CarFleetContext : DbContext
    {
        public CarFleetContext(DbContextOptions<CarFleetContext> options) : base(options)
        {

        }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Vehicle_TechnicalTest> Vehicle_TechnicalTest { get; set; }
    }
}
