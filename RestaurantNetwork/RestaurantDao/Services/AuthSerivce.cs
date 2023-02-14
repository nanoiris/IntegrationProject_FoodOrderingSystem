using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using RestaurantDao.ViewModel;


namespace RestaurantDao.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        string uid;

        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager )
        {
            this.userManager = userManager;
            this.signInManager = signInManager; 
        }

        public void ChangePassword(string email, string password, string retypePassword)
        {
            throw new NotImplementedException();
        }

        public void ChangeProfile(UserViewModel userViewModel)
        {
            throw new NotImplementedException();
        }

        public AppUser? Login(string username, string password)
        {
            var result = signInManager.PasswordSignInAsync(username, password,true,false).GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                using (var db = new AppDbContext())
                {
                    AppUser appUser = db.AppUsers.Include("Restaurant").Where(x => x.Email == username).FirstAsync().GetAwaiter().GetResult();
                    IdentityUser? idUser = userManager.FindByEmailAsync(username)
                    .GetAwaiter().GetResult();

                    IList<string> roles = userManager.GetRolesAsync(idUser)
                        .GetAwaiter().GetResult();
                    if (roles != null && roles.Count > 0)
                    {
                        appUser.Role = roles[0];
                    }
                    return appUser;
                }
            }
            return null;
        }

        public void Logout()
        {
            signInManager.SignOutAsync().GetAwaiter().GetResult();
        }

        public void Signup(UserViewModel userViewModel)
        {
            throw new NotImplementedException();
        }

        public void AddUser(AppUser user, Stream logo)
        {
            uid = Guid.NewGuid().ToString("N");
            saveLogos(user, logo);
            using (var db = new AppDbContext())
            {
                db.Database.BeginTransaction();
                try
                {
                    db.AppUsers.Add(user);
                    db.SaveChangesAsync().GetAwaiter().GetResult();

                    var result = userManager.CreateAsync(user.IdUser, user.Password).GetAwaiter().GetResult();
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine(error.Description);
                        }
                        throw new SystemException("AuthService.AddUser: cannot sign in as a identity user");
                    }
                    result = userManager.AddToRoleAsync(user.IdUser, user.Role).GetAwaiter().GetResult();
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine(error.Description);
                        }
                        throw new SystemException("AuthService.AddUser: cannot sign in as a identity user");
                    }
                    db.Database.CommitTransaction();
                }
                catch (Exception ex)
                {
                    db.Database.RollbackTransaction();
                    deleteLogos(user);
                    Console.WriteLine(ex.Message);
                    throw new SystemException("AuthService.AddUser: cannot sign in as a identity user");
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
        public void UpdateUser(AppUser user, Stream logo)
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
                        if (user.Logo!= null && originalOne.Logo != null)
                        {
                            deleteLogos(originalOne);
                            Console.WriteLine("deleteLogos");
                            originalOne.Logo = user.Logo;
                        }
                        originalOne.Name = user.Name;
                        originalOne.Email = user.Email;
                        originalOne.PhoneNo = user.PhoneNo;
                        db.SaveChanges();
                        Console.WriteLine("save app user successfully");
                        IdentityUser idUser = userManager.FindByIdAsync(user.IdUser.Id).GetAwaiter().GetResult();
                        idUser.PhoneNumber = user.PhoneNo;
                        idUser.UserName = user.Email;
                        idUser.Email = user.Email;
                        userManager.UpdateAsync(idUser).GetAwaiter().GetResult();
                        Console.WriteLine("save identity user successfully");
                        db.Database.CommitTransaction();
                    }
                }
            }
            catch (Exception ex)
            {
                deleteLogos(user);
                throw new SystemException(ex.Message);
            }
        }
        public AppUser FindAppUserById(int id)
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
    }
}
