using Microsoft.EntityFrameworkCore;
using MyPlayMarket.Infrastructure.Entities;
using MyPlayMarket.Infrastructure;
using MyPlayMarket.Infrastructure.Data;
using MyPlayMarket.Core.Services;


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

            string computerName = Environment.MachineName;

            if (computerName.ToLower() == "gregor")
            {
                builder.Services.AddDbContext<ApplicationDbContext>
                (option => option.UseSqlServer(builder.Configuration.GetConnectionString("LaptopConnection")));
                Console.WriteLine("Система определила, что вы используете ноутбук.");
            }
            else
            {
                builder.Services.AddDbContext<ApplicationDbContext>
                (option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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
