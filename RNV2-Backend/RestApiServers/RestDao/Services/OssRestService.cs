using Microsoft.EntityFrameworkCore;
using RestaurantDao.Contexts;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Services
{
    public partial class RestaurantService : IRestaurantService
    {
        public async Task<bool> AddRestaurant(RestaurantForm form)
        {
            using (var ctx = new RestaurantContext())
            {
                RestCategory? category = await ctx.RestCategories.FindAsync(form.CategoryId);
                if (category == null)
                {
                    Console.WriteLine("The category is not right");
                    return false;
                }


                Restaurant newOne = new Restaurant();
                newOne.Id = Guid.NewGuid().ToString("N");
                newOne.Name = form.Name;
                newOne.Email = form.Email;
                newOne.Description = form.Description;
                newOne.Logo = form.Logo;
                newOne.PhoneNo = form.PhoneNo;
                newOne.IsFeatured = form.IsFeatured;

                Address address = new Address();
                address.Street = form.Street;
                address.City = form.City;
                address.State = form.State;
                address.Country = form.Country;
                address.PostalCode = form.PostalCode;

                if (address.Street != null && address.City != null && address.State != null)
                    newOne.Address = address;

                newOne.CategoryId = category.Id;
                ctx.Restaurants.Add(newOne);
                var result = await ctx.SaveChangesAsync();
                if (result == 1)
                    return true;
                return false;
            }
        }

        public async Task<bool> DeleteRestaurant(string restaurantId)
        {
            using (var ctx = new RestaurantContext())
            {
                Restaurant? row = await ctx.Restaurants.FirstAsync(x => x.Id == restaurantId);
                if (row != null)
                {
                    ctx.Remove(row);
                    var result = await ctx.SaveChangesAsync();
                    if (result == 1)
                        return true;
                }
                return false;
            }
        }
        public Task<Restaurant?> FindRestaurant(string restaurantId)
        {
            using (var ctx = new RestaurantContext())
            {
                return ctx.Restaurants.FirstOrDefaultAsync(x => x.Id == restaurantId);
            }
        }
        public Task<List<Restaurant>> ListRestaurant()
        {
            using (var ctx = new RestaurantContext())
            {
                return ctx.Restaurants.Take(25).ToListAsync();
            }
        }
        public async Task<bool> UpdateRestaurant(RestaurantForm form)
        {
            using (var ctx = new RestaurantContext())
            {
                var row = await ctx.Restaurants.FindAsync(form.Id);
                if (row == null)
                    return false;

                RestCategory? category = null;
                if (form.CategoryId != null)
                {
                    category = await ctx.RestCategories.FindAsync(form.CategoryId);
                    if (category == null)
                        return false;
                }

                if (form.Name != null)
                    row.Name = form.Name;
                if (form.Email != null)
                    row.Email = form.Email;
                if (form.Description != null)
                    row.Description = form.Description;

                if (form.IsFeatured != null)
                    row.IsFeatured = form.IsFeatured;

                if (form.Logo != null)
                    row.Logo = form.Logo;

                if (form.PhoneNo != null)
                    row.PhoneNo = form.PhoneNo;

                Address address = new Address();
                if (form.Street != null)
                    address.Street = form.Street;
                if (form.City != null)
                    address.City = form.City;
                if (form.State != null)
                    address.State = form.State;
                if (form.Country != null)
                    address.Country = form.Country;
                if (form.PostalCode != null)
                    address.PostalCode = form.PostalCode;

                if (address.Street != null && address.City != null && address.State != null)
                    row.Address = address;

                row.CategoryId = category.Id;

                var result = await ctx.SaveChangesAsync();
                if (result == 1)
                    return true;
                return false;
            }
        }
        public async Task<bool> UpdateRestaurantLogo(Restaurant model)
        {
            using (var ctx = new RestaurantContext())
            {
                var row = await ctx.Restaurants.FindAsync(model.Id);
                if (row == null)
                    return false;
                row.Logo= model.Logo;
                var result = await ctx.SaveChangesAsync();
                return result == 1 ? true : false;
            }
        }
    }

}
