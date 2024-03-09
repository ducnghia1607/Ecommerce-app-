using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindUserByClaimsPrincipalWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            // Returns the value for the first claim of the specified type, otherwise null if the claim is not present.
            var email = user.FindFirstValue(ClaimTypes.Email);
            return await userManager.Users.Include(x => x.Address).Where(x => x.Email == email).SingleOrDefaultAsync();
        }

        public static async Task<AppUser> FindUserByClaimsPrincipal(this UserManager<AppUser> userManager,
        ClaimsPrincipal user)
        {
            return await userManager.FindByEmailAsync(user.FindFirstValue(ClaimTypes.Email));
        }
    }
}