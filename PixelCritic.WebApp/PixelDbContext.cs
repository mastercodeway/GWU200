using Microsoft.EntityFrameworkCore;
using PixelCritic.WebApp.Models;

namespace PixelCritic.WebApp
{
    public class PixelDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RatedGame> RatedGames { get; set; }

        public PixelDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
