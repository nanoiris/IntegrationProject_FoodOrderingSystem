using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantDao;
using RestaurantDao.IServices;
using RestaurantDao.Services;
using Stripe;

namespace OSS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 1;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
            });
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Home/Login";
                options.LogoutPath = $"/Home/Logout";
                options.AccessDeniedPath = $"/Home/Login";
            });
            builder.Services.AddControllersWithViews();
            
            /*
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = false;
                options.Cookie.IsEssential = true;

            });
            */
            
            builder.Services.AddScoped<IOssService, OssService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            builder.Services.AddAuthorization(options =>
            {

                options.AddPolicy("Operator",
                    authBuilder =>
                    {
                        authBuilder.RequireRole("Operator");
                    });

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