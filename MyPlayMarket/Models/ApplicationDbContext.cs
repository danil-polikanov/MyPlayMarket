using Microsoft.EntityFrameworkCore;

namespace MyPlayMarket.Models
{
    public class ApplicationDbContext: DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }
    }
}
