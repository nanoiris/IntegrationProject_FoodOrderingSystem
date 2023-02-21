﻿using Microsoft.EntityFrameworkCore;
using RestaurantDaoBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Contexts
{
    public class RestaurantContext : DbContext
    {
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestCategory> RestCategories { get; set; }
        public DbSet<WeeklyTrend> weeklyTrends { get; set; }

        public RestaurantContext() : base() { }
        public RestaurantContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseCosmos(
            "https://localhost:8081",
            "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
            databaseName: "RatingsDB");
    }
}
