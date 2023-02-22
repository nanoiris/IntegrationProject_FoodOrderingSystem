
using Microsoft.EntityFrameworkCore;
using RestaurantDao.Contexts;
using RestaurantDao.Services;
using RestaurantDaoBase.IServices;

namespace RatingServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                       policy.AllowAnyOrigin()
                             .AllowAnyHeader()
                             .AllowAnyMethod();
                    });
            });

            // Add cosmos 
            /*
             builder.Services.AddDbContext<RatingContext>(options =>
             {
                 options.UseCosmos(
                     builder.Configuration["Cosmos.EndPoint"],
                     builder.Configuration["Cosmos.Key"],
                     builder.Configuration["Cosmos.DatabasName"]
                 );
             });
             */

            builder.Services.AddScoped<IRatingService, RatingService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseCors();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}