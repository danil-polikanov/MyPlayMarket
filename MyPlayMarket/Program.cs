using Microsoft.EntityFrameworkCore;
using MyPlayMarket.Infrastructure.Entities;
using MyPlayMarket.Infrastructure;
using MyPlayMarket.Infrastructure.Data;
using MyPlayMarket.Core.Services;
using MyPlayMarket.Core.IServices;


namespace MyPlayMarket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IGameRepository, GameRepository>();
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<ISortingService,SortingService>();
            builder.Services.AddScoped<IFilteringService, FilteringService>();
            builder.Services.AddScoped<IPaginationService, PaginationService>();
            builder.Services.AddScoped<IDataService, DataService>();

            string computerName = Environment.MachineName;

            if (computerName.ToLower() == "gregor")
            {
                builder.Services.AddDbContext<ApplicationDbContext>
                (option => option.UseSqlServer(builder.Configuration.GetConnectionString("LaptopConnection")).LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
        .EnableSensitiveDataLogging());
                Console.WriteLine("Система определила, что вы используете ноутбук.");
            }
            else
            {
                builder.Services.AddDbContext<ApplicationDbContext>
                (option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
        .EnableSensitiveDataLogging());
                Console.WriteLine("Система определила, что вы используете компьютер.");
            }

            var app = builder.Build();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.MapControllers();
            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();

        }
    }
}
