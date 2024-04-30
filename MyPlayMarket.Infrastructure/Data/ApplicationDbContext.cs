using Microsoft.EntityFrameworkCore;
using MyPlayMarket.Core.Entities.Game
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
