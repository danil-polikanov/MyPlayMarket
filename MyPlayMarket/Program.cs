using Microsoft.EntityFrameworkCore;
using MyPlayMarket.Web

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
            app.Run();

        }
    }
}
