using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Chat.Api.Managers;
using Chat.Api.Helpers;
using Chat.Api.Entities;
public class JwtManager
{
    private JwtParameters JwtParametrs { get; set; }
    public readonly IConfiguration _configuration;

    public JwtManager(IConfiguration configuration)
    {
        _configuration = configuration;
        JwtParametrs = configuration.GetSection("JwtParameters").Get<JwtParameters>()!;
    }

    public string GenerateToken(User user)
    {
        var key = System.Text.Encoding.UTF32.GetBytes(JwtParametrs.Key);
        var signkey = new SigningCredentials(new SymmetricSecurityKey(key), "HS256");

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        
        var security = new JwtSecurityToken(issuer: JwtParametrs.Issuer,audience:JwtParametrs.Audience,
            signingCredentials: signkey,claims: claims, expires: DateTime.Now.AddMinutes(60));
        
        var token = new JwtSecurityTokenHandler().WriteToken(security);
        return token;
    }
    
    
}