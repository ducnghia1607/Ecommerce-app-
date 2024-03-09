using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Bob",
                    Email = "bob@example.com",
                    UserName = "bob@example.com",
                    Address = new Address
                    {
                        FirstName = "Bob",
                        LastName = "Boo",
                        Street = "10 Main Street",
                        City = "NYC",
                        State = "NY",
                        ZipCode = "100222"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}