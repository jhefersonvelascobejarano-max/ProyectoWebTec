using Microsoft.EntityFrameworkCore;
using ProyectoWebTec.Models;

namespace ProyectoWebTec.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<LeaderboardPeru> LeaderboardPeru { get; set; }
    }
}
