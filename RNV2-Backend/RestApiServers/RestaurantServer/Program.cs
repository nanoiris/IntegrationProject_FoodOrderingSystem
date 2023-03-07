using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RestaurantDao.Services;
using RestaurantDaoBase.IServices;
using RestaurantServer.Services;

namespace RestaurantServer
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            /*
            builder.Services.AddScoped<IFileService, LocalFileService>(
                x => new LocalFileService(builder.Configuration["LogoRootPath"]));
            */
            var blobConn = builder.Configuration["BlobStorage:ConnectionString"];
            var blobContainer = builder.Configuration["BlobStorage:ContainerName"];
            builder.Services.AddScoped<IFileService, BlobStorageService>(
                x => new BlobStorageService(blobConn, blobContainer));

            builder.Services.AddScoped<IRestaurantService, RestaurantService>();
            builder.Services.AddScoped<IMenuService, MenuService>(x =>
              new MenuService(x.GetRequiredService<ILogger<MenuService>>()));

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
           // app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}