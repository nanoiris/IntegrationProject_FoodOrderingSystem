using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantDao;
using RestaurantDao.Models;

namespace OSS
{
    public class OssContext : AppDbContext
    {
        public OssContext() : base() { }
        public OssContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //seed admin role
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                  Name = "Operator",
                  NormalizedName = "OPERATOR",
                  Id = "1",
                },
                new IdentityRole
                {
                  Name = "Restaurant",
                  NormalizedName = "RESTAURANT",
                  Id = "2",
                 },
                new IdentityRole
                {
                    Name = "Consumer",
                    NormalizedName = "CONSUMER",
                    Id = "3",
                }
            );

            //create user
            var appUser = new IdentityUser
            {
                Id = "1",
                Email = "admin@rn.com",
                UserName = "admin@rn.com",
                NormalizedUserName = "ADMIN@RN.COM"
            };

            //set user password
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "111");

            //seed user
            builder.Entity<AppUser>().HasData(appUser);

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = "1"
            });
        }
    }
}
