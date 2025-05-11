using Microsoft.EntityFrameworkCore;

namespace PosterCMS.Models
{
    public class PosterDbContext : DbContext
    {
        public DbSet<PosterModel> Posters {get; set;}

        public PosterDbContext(DbContextOptions<PosterDbContext> options) : base (options)
        {
            
        }
    }
}