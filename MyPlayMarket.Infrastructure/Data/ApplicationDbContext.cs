using Microsoft.EntityFrameworkCore;
using MyPlayMarket.Core.Entities;
using MyPlayMarket.Core;

namespace MyPlayMarket.Core
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Game> Games { get; set; }
        public DbSet<User> LocalUsers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GameGenre> GameGenre { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<GameTag> GameTags { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }
        public DbSet<GameScreenshot> GameScreenshots { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GameGenre>()
                .HasKey(gg => new { gg.GameId, gg.GenreId });
            modelBuilder.Entity<GamePlatform>()
             .HasKey(gg => new { gg.GameId, gg.PlatformId });
            modelBuilder.Entity<GameTag>()
             .HasKey(gg => new { gg.GameId, gg.TagId });

        }
    }
}
