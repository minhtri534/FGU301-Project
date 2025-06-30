using Microsoft.EntityFrameworkCore;
using FGUWebAPI.Models;

namespace FGUWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PlayerProgress> PlayerProgress { get; set; }
    }

}
