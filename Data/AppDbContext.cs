using Microsoft.EntityFrameworkCore;

namespace htmlscraperapi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<ScrapedContent> ScrapedContents { get; set; }


    }
}
