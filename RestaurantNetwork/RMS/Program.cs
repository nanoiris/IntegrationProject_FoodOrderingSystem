using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantDao;
using RestaurantDao.IServices;
using RestaurantDao.Services;
using System.Configuration;
using Vereyon.Web;

namespace RMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //var connectionString = "server=fsd05library.database.windows.net;Initial Catalog=RestaurantNetwork;User Id=dbadmin;Password=Fsd05@@@;persist security info=True;MultipleActiveResultSets=True;App=EntityFramework";
            // var connectionString = builder.Configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.");

            /*
            builder.Services.AddDbContext<AppDbContext>(
                options => {
                    options.UseSqlServer(connectionString);
                }
            );
            */

            var connectionString = builder.Configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));


            Console.WriteLine("connection string = " + AppDbContext.GetConnectionString());

            // Add services to the container.

            builder.Services.AddControllersWithViews();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddScoped<UserManager<IdentityUser>, UserManager<IdentityUser>>();
            builder.Services.AddScoped<SignInManager<IdentityUser>, SignInManager<IdentityUser>>();

            ;
            builder.Services.AddScoped<IRmsService, RmsService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddFlashMessage();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(90);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}