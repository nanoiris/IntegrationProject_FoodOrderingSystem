using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantDao.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Services
{
    public partial class OssService : IOssService
    {
        public List<IdentityRole> ListRole()
        {
            return roleManager.Roles.ToList();
        }
        public void AddRole(string name)
        {
            IdentityRole role = new IdentityRole
            {
                Name = name
            };

            var result = roleManager.CreateAsync(role)
                .GetAwaiter().GetResult();
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
                throw new ArgumentException("OssService.AddRole : cannot add the new role");
            }
        }
        public void DeleteRole(string id)
        {
            roleManager.Roles.Where(x => x.Id == id)
                .ExecuteDelete();
        }
    }
}
