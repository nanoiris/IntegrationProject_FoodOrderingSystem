using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestaurantDao.Services;
using RestaurantDaoBase.IServices;

namespace OrderServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            // Add services to the container.
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
            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
              {
                  options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                  options.JsonSerializerOptions.PropertyNamingPolicy = null;
              });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            /*
            builder.Services.AddAuthorization().AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(30),

                    ValidateAudience = true,
                    AudienceValidator = (m, n, z) =>
                    {
                        return m != null && m.FirstOrDefault().Equals(builder.Configuration["AuthSettings:Audince"]);
                    },
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["AuthSettings:Issuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:Key"])),
                    RequireExpirationTime = true,
                };
            });
            */

            builder.Services.AddScoped<IOrderService, OrderService>(x =>
              new OrderService(x.GetRequiredService<ILogger<OrderService>>()));

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