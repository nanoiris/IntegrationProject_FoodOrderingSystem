using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantDao.Enums;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Services
{
    public partial class OssService : IOssService
    {
        private readonly UserManager<IdentityUser> userManager;
        public readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public OssService(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public void AddRestaurantUser(AppUser user, Stream logo)
        {
            uid = Guid.NewGuid().ToString("N");
            saveLogos(user, logo);
            using (var db = new AppDbContext())
            {
                db.Database.BeginTransaction();
                try
                {
                    Restaurant rest = db.Restaurants.FirstAsync(x => x.Id == user.Restaurant.Id)
                        .GetAwaiter().GetResult();
                    user.Restaurant = rest;
                    db.AppUsers.Add(user);
                    db.SaveChangesAsync().GetAwaiter().GetResult();

                    var result = userManager.CreateAsync(user.IdUser, user.Password).GetAwaiter().GetResult();
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine(error.Description);
                        }
                        throw new SystemException("OssService.AddRestaurantUser: cannot sign in as a identity user");
                    }
                    result = userManager.AddToRoleAsync(user.IdUser, "Restaurant").GetAwaiter().GetResult();
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine(error.Description);
                        }
                        throw new SystemException("OssService.AddRestaurantUser: cannot sign in as a identity user");
                    }
                    db.Database.CommitTransaction();
                }
                catch (Exception ex)
                {
                    db.Database.RollbackTransaction();
                    deleteLogos(user);
                    Console.WriteLine(ex.Message);
                    throw new SystemException("OssService.AddRestaurantUser: cannot sign in as a identity user");
                }
            }

        }
        private void saveLogos(AppUser user, Stream logo)
        {
            if (logo != null && user.Logo != null)
            {
                var containerClient = AppDbContext.GetBlobContainerClient();
                string imgType = user.Logo.Substring(user.Logo.LastIndexOf("."));
                user.Logo = $"user_{uid}{imgType}";
                BlobClient blobClient = containerClient.GetBlobClient(user.Logo);
                using (logo)
                {
                    blobClient.UploadAsync(logo).GetAwaiter().GetResult();
                }
            }
        }
        private void deleteLogos(AppUser user)
        {
            try
            {
                var containerClient = AppDbContext.GetBlobContainerClient();
                BlobClient blobClient = containerClient.GetBlobClient(user.Logo);
                blobClient.DeleteAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<AppUser> ListRestaurantUsers(int restaurantId)
        {
            using (var db = new AppDbContext())
            {
                return db.AppUsers.Where(x => x.Restaurant.Id == restaurantId)
                    .OrderBy(x => x.Name).ToListAsync()
                    .GetAwaiter().GetResult();
            }
        }

        public AppUser? FindRestaurantUser(int id)
        {
            using (var db = new AppDbContext())
            {
                var user = db.AppUsers.Include("Restaurant").FirstOrDefaultAsync(x => x.Id == id)
                    .GetAwaiter().GetResult();
                var idUser = userManager.FindByEmailAsync(user.Email).GetAwaiter().GetResult();
                user.IdUser = idUser;
                return user;
            }
        }
        public void DeleteRestaurantUser(int id)
        {
            using (var db = new AppDbContext())
            {
                AppUser? row = db.AppUsers.FindAsync(id)
                    .GetAwaiter().GetResult();
                if (row != null)
                {
                    IdentityUser? user = userManager.FindByEmailAsync(row.Email)
                        .GetAwaiter().GetResult();
                    var result = userManager.DeleteAsync(user)
                        .GetAwaiter().GetResult();
                    if (result.Succeeded)
                    {
                        deleteLogos(row);
                        db.AppUsers.Remove(row);
                        db.SaveChangesAsync().GetAwaiter().GetResult();
                    }
                    else
                    {
                        foreach (var msg in result.Errors)
                        {
                            Console.WriteLine(msg.Description);
                        }
                    }

                }
            }
        }

        public void UpdateRestaurantUser(AppUser user, Stream logo)
        {
            uid = Guid.NewGuid().ToString("N");
            saveLogos(user, logo);
            try
            {
                using (var db = new AppDbContext())
                {
                    AppUser originalOne = db.AppUsers.FindAsync(user.Id).GetAwaiter().GetResult();
                    if (originalOne != null)
                    {
                        db.Database.BeginTransaction();
                        Console.WriteLine("BeginTransaction");
                        if (originalOne.Logo != null)
                        {
                            deleteLogos(originalOne);
                            Console.WriteLine("deleteLogos");
                            originalOne.Logo = null;
                        }
                        originalOne.Name = user.Name;
                        originalOne.Email = user.Email;
                        originalOne.PhoneNo = user.PhoneNo;
                        originalOne.Restaurant = db.Restaurants.Find(user.Restaurant.Id);

                        //originalOne.RestaurantId = user.RestaurantId;

                        originalOne.Logo = user.Logo;
                        db.SaveChanges();
                        Console.WriteLine("save app user successfully");
                        IdentityUser idUser = userManager.FindByIdAsync(user.IdUser.Id)
                            .GetAwaiter().GetResult();
                        idUser.PhoneNumber = user.PhoneNo;
                        idUser.UserName = user.Email;
                        idUser.Email = user.Email;
                        userManager.UpdateAsync(idUser)
                            .GetAwaiter().GetResult();
                        Console.WriteLine("save identity user successfully");
                        db.Database.CommitTransaction();
                    }
                }
            }
            catch (Exception ex)
            {
                deleteLogos(user);
                //throw new SystemException("RmsService.AddRestaurant : cannot add the restaurant");
                throw new SystemException(ex.Message);
            }
        }


        public void FreeUser(string userId)
        {
            throw new NotImplementedException();
        }

        public void ListOrdersByUser(string email)
        {
            throw new NotImplementedException();
        }

        public void LockUser(string userId)
        {
            throw new NotImplementedException();
        }

        public void ResetUserPassword(string email)
        {
            throw new NotImplementedException();
        }

    }
}
