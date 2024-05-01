using Microsoft.EntityFrameworkCore;
using MyPlayMarket.Infrastructure.Entities;

namespace MyPlayMarket.Infrastructure.Data
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
