﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantDbContext dbContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (dbContext.Database.GetPendingMigrations().Any())
                await dbContext.Database.MigrateAsync();

            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var Restaurants = GetRestaurants();
                    await dbContext.Restaurants.AddRangeAsync(Restaurants);
                    await dbContext.SaveChangesAsync();
                }
                // Seeding the roles
                if (!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    await dbContext.Roles.AddRangeAsync(roles);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles = [
                    new(RolesConstant.User){
                        NormalizedName = RolesConstant.User.ToUpper()
                    },
                    new(RolesConstant.Admin){
                        NormalizedName = RolesConstant.Admin.ToUpper()
                    },
                    new(RolesConstant.Owner)
                    { 
                        NormalizedName = RolesConstant.Owner.ToUpper()
                    }
                ];
            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            User owner = new User() { Email = "seed-test@gmail.com" };

            List<Restaurant> Restaurants =

                [
                    new()
                    {
                        Owner = owner,
                        Name = "KFC",
                        Category = "Fast Food",
                        Description =
                            "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                        ContactEmail = "contact@kfc.com",
                        HasDelivery = true,
                        Dishes =
                        [
                            new ()
                            {
                                Name = "Nashville Hot Chicken",
                                Description = "Nashville Hot Chicken (10 pcs.)",
                                Price = 10.30M,
                            },

                            new ()
                            {
                                Name = "Chicken Nuggets",
                                Description = "Chicken Nuggets (5 pcs.)",
                                Price = 5.30M,
                            },
                        ],
                        Address = new ()
                        {
                            City = "London",
                            Street = "Cork St 5",
                            PostalCode = "WC2N 5DU"
                        },

                    },
                    new ()
                    {
                        Owner = owner,
                        Name = "McDonald",
                        Category = "Fast Food",
                        Description =
                            "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                        ContactEmail = "contact@mcdonald.com",
                        HasDelivery = true,
                        Address = new Address()
                        {
                            City = "London",
                            Street = "Boots 193",
                            PostalCode = "W1F 8SR"
                        }
                    }
                ];
            return Restaurants;
        }
    }
}
