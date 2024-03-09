using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;
    public TokenService(IConfiguration config)
    {
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
    }

    public string CreateToken(AppUser user)
    {
        var claims = new List<Claim>(){
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.GivenName,user.DisplayName)
        };
        // Defines the SecurityKey, algorithm and digest for digital signatures.
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        //Contains some information which used to create a security token.
        // Payload 
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds,
            Issuer = _config["Token:Issuer"],
        };
        // creating and validating Json Web Tokens
        var tokenHandle = new JwtSecurityTokenHandler();

        var token = tokenHandle.CreateToken(tokenDescriptor);

        return tokenHandle.WriteToken(token);
    }
}
