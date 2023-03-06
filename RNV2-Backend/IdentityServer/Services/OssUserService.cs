using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IdentityServer.Services
{
    public partial class UserService : IUserService
    {
        
        public async Task<AppResult> AddUser(AddUserViewModel model)
        {
            var user = new AppUser
            {
                Name = model.Name,
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.PhoneNo,
                Status = model.Status,
                RestaurantId = model.RestaurantId,
                RestaurantName = model.RestaurantName,
            };

            var result = await userManager.CreateAsync(user, "111");
            AppResult appResult;
            if (!result.Succeeded)
            {
                appResult = new AppResult("User is not created", false);
                appResult.Errors = result.Errors.Select(e => e.Description);
                return appResult;
            }
            appResult = await addRole(user, "Restaurant");
            if(appResult.IsSuccess)
            {
                user = await userManager.FindByEmailAsync(user.Email);
                appResult.Message = user.Id;
                return appResult;
            }else
                return new AppResult("Cannot find the user",false);
        }

        public async Task<AppResult> UpdateUserMain(AddUserViewModel model)
        {
            AppUser? user = await userManager.FindByEmailAsync(model.Email);
            if(user == null) 
            {
                return new AppResult($"No such a user (email={model.Email})", false);
            }
            if(model.Name != null)  
              user.Name = model.Name;
            if (model.PhoneNo != null)
                user.PhoneNumber = model.PhoneNo;
            user.Status = UserStatusEnum.active;
            if (model.RestaurantId != null)
                user.RestaurantId = model.RestaurantId;
            if (model.RestaurantName != null)
                user.RestaurantName = model.RestaurantName;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded) 
            {
                return new AppResult("",true);
            }
            AppResult appResult = new AppResult($"Cannot update the user({model.Email})", false);
            appResult.Errors = result.Errors.Select(x => x.Description);
            return appResult;
        }

        public async Task<AppResult> UpdateUserAddress(UserAddressViewModel model)
        {
            AppUser? user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new AppResult($"No such a user (email={model.Email})", false);
            }
            if(model.Street != null)user.Street = model.Street;
            if(model.City != null) user.City = model.City;
            if(model.State != null)user.State = model.State;
            if(model.Country != null)user.Country = model.Country;
            if(model.PostCode != null)user.PostCode = model.PostCode;
            if(model.District != null)user.District = model.District;

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new AppResult("", true);
            }
            AppResult appResult = new AppResult($"Cannot update the user({model.Email})", false);
            appResult.Errors = result.Errors.Select(x => x.Description);
            return appResult;
        }
        public async Task<AppResult> UpdateUserLogo(string email, string logoPath)
        {
            AppUser user = await userManager.FindByEmailAsync(email);
            if(user == null) 
            {
                return new AppResult("no such a user", false);
            }
            user.Logo = logoPath;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new AppResult("", true);
            }
            AppResult appResult = new AppResult($"Cannot update the user({email})", false);
            appResult.Errors = result.Errors.Select(x => x.Description);
            return appResult;
        }

        public async Task<List<object>>? AvaliableDeliveryManList()
        {
            //userManager.Users.Where(x => x.Status == UserStatusEnum.active
            //&& x.Role)
            using (var ctx = new AppCtx())
            {
                var query = from users in ctx.Users
                            join userRoles in ctx.UserRoles
                            on users.Id equals userRoles.UserId
                            join roles in ctx.Roles
                            on userRoles.RoleId equals roles.Id
                            where users.Status == UserStatusEnum.active
                            && roles.Name == "Delivery"
                            select new
                            {
                                Id = users.Id,
                                Name = users.Name,
                            };
                return query.ToListAsync<List<object>>(); ;

            }
        }

    }
}
