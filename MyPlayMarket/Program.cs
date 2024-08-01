using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyPlayMarket.Infrastructure;
using MyPlayMarket.Infrastructure.Data;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Core.Services;
using NLog.Web;
using NLog;
using MyPlayMarket.Core.IRepository;
using MyPlayMarket.Infrastructure.Data.Repository;
using MyPlayMarket.Core;
using MyPlayMarket.Core.Entities;
using System.Configuration;
using MyPlayMarket.Web.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.CookiePolicy;
using Stripe;

namespace MyPlayMarket
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // Настройка NLog
            var logger = LogManager.Setup().LoadConfigurationFromXml("nlog.config").GetCurrentClassLogger();
            logger.Debug("Start Program");
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Настройка логирования
                builder.Logging.ClearProviders();
                builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                builder.Host.UseNLog();

                //JWT
                builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
                builder.Services.AddApiAuthentication(
                 builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());
                // Регистрация служб
                builder.Services.AddRazorPages();
                builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
                builder.Services.AddControllersWithViews();

                // Dependency Injection
                builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
                builder.Services.AddScoped<IJwtProvider, JwtProvider>();
                builder.Services.AddScoped<IGameService, GameService>();
                builder.Services.AddScoped<IUserService, UserService>();
                builder.Services.AddScoped<ISortingService, SortingService>();
                builder.Services.AddScoped<IFilteringService, FilteringService>();
                builder.Services.AddScoped<IPaginationService, PaginationService>();
                builder.Services.AddScoped<IDataService, DataService>();
                builder.Services.AddScoped<ICartService,CartService>();
                builder.Services.AddScoped<IApiService, ApiService>();                      
                builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
                builder.Services.AddScoped<IUserRepository, UserRepository>();
                builder.Services.AddScoped<IGameRepository, GameRepository>();                                   
                builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                builder.Services.AddHttpClient();
                builder.Services.AddHttpContextAccessor();
                builder.Services.AddSession();
                //Stripe
                builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

                // Configure Stripe settings
                var stripeSettings = builder.Configuration.GetSection("Stripe").Get<StripeSettings>();
                Stripe.StripeConfiguration.ApiKey = stripeSettings.SecretKey;
                // Строка подключения SQL

                string computerName = Environment.MachineName;
                if (computerName.ToLower() == "gregor")
                {
                    builder.Services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("LaptopConnection"))
                               .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
                               .EnableSensitiveDataLogging());
                    Console.WriteLine("Система определила, что вы используете ноутбук.");
                }
                else
                {
                    builder.Services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                               .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
                               .EnableSensitiveDataLogging());
                    Console.WriteLine("Система определила, что вы используете компьютер.");
                }
                var app = builder.Build();

                // Конфигурация промежуточного ПО
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }
                app.UseHttpsRedirection();
                app.UseCookiePolicy(new CookiePolicyOptions
                {
                    MinimumSameSitePolicy=SameSiteMode.Strict,
                    HttpOnly=HttpOnlyPolicy.Always,
                    Secure=CookieSecurePolicy.Always
                });
                app.UseStaticFiles();
                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();
                app.MapControllers();
                app.MapRazorPages();
                app.UseSession();
                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "End programm with exception.");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}