using Microsoft.EntityFrameworkCore;
using VehicleCatalog.Service.Models;

namespace VehicleCatalog.Service
{
    public class ApplicationDbContex : DbContext
    {
        public ApplicationDbContex(DbContextOptions<ApplicationDbContex> options) : base(options) { }

        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }

    }
}
