using Microsoft.EntityFrameworkCore;
using RestaurantDao.Contexts;
using RestaurantDao.Models;

namespace CosmosConsole
{
    public class Program
    {
        static async Task Main(string[] args)
        {

            await Program.Run();
        }

        public static async Task Run()
        {
            Console.WriteLine();
            Console.WriteLine("Getting started with Cosmos:");
            Console.WriteLine();

            using (var context = new RatingContext())
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                context.Add(
                    new RestRating
                    {
                        Id = "1",
                        RestaurantId = "1",
                        Value = 3,
                        CreateBy = "k1@a.com",
                        CreateTime = DateTime.Now,
                    });

                await context.SaveChangesAsync();
            }

            using (var context = new RatingContext())
            {
                var rating = await context.RestRatings.FirstAsync();
                Console.WriteLine($"First rating is: {rating.CreateBy}");
                Console.WriteLine();
            }
            using (var context = new RatingContext())
            {
                context.Add(
                   new RestRating
                   {
                       Id = "2",
                       RestaurantId = "2",
                       Value = 1,
                       CreateBy = "k1@a.com",
                       CreateTime = DateTime.Now,
                   });

                await context.SaveChangesAsync();
            }
        }
    }
}