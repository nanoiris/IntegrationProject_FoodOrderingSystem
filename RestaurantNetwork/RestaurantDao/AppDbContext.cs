using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantDao.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace RestaurantDao
{
    public partial class AppDbContext : IdentityDbContext
    {
        public AppDbContext() : base() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<MenuItem> Menus { get; set; }
        public virtual DbSet<MenuCategory> MenuCategories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<RestCategory> RestCategoris { get; set; }
        public virtual DbSet<WeeklyTrend> WeeklyTrends { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }
       
        /*
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
               new IdentityRole { Id = "1", Name = "Operator", NormalizedName = "OPERATOR" },
               new IdentityRole { Id = "2", Name = "Restaurant", NormalizedName = "RESTAURANT" },
               new IdentityRole { Id = "3", Name = "Consumer", NormalizedName = "CONSUMER" }
            );
        }
        */
        public static string GetConnectionString()
        {
            // Xiao's Azure below
            //return "server=fsd05library.database.windows.net;Initial Catalog=RestaurantNetwork;User Id=dbadmin;Password=Fsd05@@@;persist security info=True;MultipleActiveResultSets=True;App=EntityFramework";
            // Yi's Azure below
             return "server=fsd05library1.database.windows.net;Initial Catalog=RestaurantNetwork;User Id=dbadmin;Password=Fsd05@@@;persist security info=True;MultipleActiveResultSets=True;App=EntityFramework";
            //return ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString;
        }

        public static string GetBlobUrlPrefix()
        {
            //return "https://fsd05restaurant.blob.core.windows.net/restaurant/";
             return "https://fsd05restaurant1.blob.core.windows.net/restaurant/";
        }

        public static string GetBlobConnectionString()
        {   // Xiao's Azure below
            //return "DefaultEndpointsProtocol=https;AccountName=fsd05restaurant;AccountKey=6XSv86GSEh22J3ZCr08dFjq7AGS93WrC7LLqLL5bwlgeSVG2LlH1j/RcoYdNgsBdwNGdb4YThCPG+ASt58zCdA==;EndpointSuffix=core.windows.net";
            
            // Yi's Azure below
             return "DefaultEndpointsProtocol=https;AccountName=fsd05restaurant1;AccountKey=EVpGeRiiEYkV37UEZNJKCg2s9+bU3UoTHss0Au4ep2uLZsWNaDglCLTLau8RWT5c69RCMDuuwxaS+ASt7br3Ow==;EndpointSuffix=core.windows.net"; 
            //return ConfigurationManager.AppSettings["RestBlobConnectionString"].ToString();
        }

        public static string GetBlobContainerName()
        {
            return "restaurant";
            //return ConfigurationManager.AppSettings["BlobContainer"].ToString();
        }

        public static string GetBlobItemUrlPrefix()
        {
            // Xiao's Azure below
            //return "https://fsd05restaurant.blob.core.windows.net/restaurant/";
            // Yi's Azure below
             return "https://fsd05restaurant1.blob.core.windows.net/restaurant/";
            //return ConfigurationManager.AppSettings["BlobItemUrlPrefix"].ToString();
        }

        public static BlobContainerClient GetBlobContainerClient()
        {
            string blobConnString = AppDbContext.GetBlobConnectionString();
            string containerName = AppDbContext.GetBlobContainerName();
            if (blobConnString == null || containerName == null)
            {
                new SystemException("AppDbContext.GetBlobContainerClient : cannot get the connection string or the container name of the blob storage");
            }
            BlobContainerClient containerClient = null;
            try
            {
                var blobServiceClient = new BlobServiceClient(AppDbContext.GetBlobConnectionString());
                containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            }catch(Exception ex)
            {
                Console.WriteLine("AppDbContext :" + ex.Message);
            }
            return containerClient;
        }
    }
}
