using System;
using System.Data.Entity;
using OdeToFood.Models;

namespace OdeToFood.Models
{
    public class OdeToFoodDB : DbContext
    {
        public OdeToFoodDB() : base("name=DefaultConnection")
        {
            
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantReview> Reviews { get; set; }
    }
}