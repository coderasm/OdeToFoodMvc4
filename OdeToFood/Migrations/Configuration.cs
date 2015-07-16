using System.Collections.Generic;
using WebMatrix.WebData;

namespace OdeToFood.Migrations
{
    using OdeToFood.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;

    internal sealed class Configuration : DbMigrationsConfiguration<OdeToFoodDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(OdeToFoodDB context)
        {
            context.Restaurants.AddOrUpdate(r => r.Name,
                 new Restaurant() { Name = "Sabatino's", City = "Baltimore", Country = "USA" },
                 new Restaurant() { Name = "Great Lake", City = "Chicago", Country = "USA" },
                 new Restaurant()
                 {
                     Name = "Smaka",
                     City = "Gothenberg",
                     Country = "Sweden",
                     Reviews =
                         new List<RestaurantReview>
                        {
                            new RestaurantReview {Rating = 9, Body = "Great food!", ReviewerName = "Scott"}
                        }
                 });

            for (int i = 0; i < 1000; i++)
            {
                context.Restaurants.AddOrUpdate(r => r.Name,
                new Restaurant()
                {
                    Name = i.ToString(),
                    City = "Nowhere",
                    Country = "USA"
                });

            }

            SeedMembership();
        }

        private void SeedMembership()
        {
            Statics.Database.Initialize();

            var roles = (SimpleRoleProvider) Roles.Provider;
            var membership = (SimpleMembershipProvider) Membership.Provider;

            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (membership.GetUser("pkrueger", false) == null)
            {
                membership.CreateUserAndAccount("pkrueger", "whatnow");
            }
            if (!roles.GetRolesForUser("pkrueger").Contains("Admin"))
            {
                roles.AddUsersToRoles(new []{"pkrueger"}, new []{"admin"});
            }
        }
    }
}
