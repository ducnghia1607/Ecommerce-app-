using System.Text;
using Core;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API;

public static class IdentityServicesExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {

        services.AddDbContext<AppIdentityDbContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("IdentityConnection"));
        });
        services.AddIdentityCore<AppUser>(opt =>
        {
            // opt.Password.RequireNonAlphanumeric("&*");
        })
        .AddEntityFrameworkStores<AppIdentityDbContext>()
        .AddSignInManager<SignInManager<AppUser>>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opt =>
        {

            // Tạo ra các option để validate khi nhận 1 token từ client 
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                // Khai báo key đúng 
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                // Khai báo issuer hợp lệ 
                ValidIssuer = config["Token:Issuer"],
                ValidateIssuer = true,
                ValidateAudience = false
            };
        });
        services.AddAuthorization();

        return services;
    }
}
