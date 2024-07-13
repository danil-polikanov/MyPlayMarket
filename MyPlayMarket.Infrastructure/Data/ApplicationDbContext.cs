using Microsoft.EntityFrameworkCore;
using MyPlayMarket.Infrastructure;
using MyPlayMarket.Core;
using MyPlayMarket.Core.IRepository;

namespace MyPlayMarket.Core
{
    public class ApplicationDbContext: DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Game> Games { get; set; }
        public DbSet<User> LocalUsers { get; set; }
    }
}
