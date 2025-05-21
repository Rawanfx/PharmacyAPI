using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using projectAPI.Core.Domain.IdentityEntities;
using projectAPI.Core.DTO;
using projectAPI.Core.ServiceCotract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace projectAPI
{ 
    public class JwtServices : IJwtServices
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public JwtServices(IConfiguration configuration,UserManager<ApplicationUser>userManager
            ,RoleManager<IdentityRole> roleManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }



        public async Task<AuthenticationRespose> CreateTokenAsync(ApplicationUser user)
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var userRole = await userManager.GetRolesAsync(user);
            SymmetricSecurityKey SymmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:key"])
             );
            SigningCredentials signingCredentials = new SigningCredentials(
                SymmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            DateTime expire = DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["Jwt:expiration_minutes"]));
            List<Claim> claim = new List<Claim>()
            {
                new Claim (JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim (JwtRegisteredClaimNames.Jti,Guid.NewGuid(). ToString()),
                new Claim (ClaimTypes.Role,"User"),
                 new Claim(JwtRegisteredClaimNames.Iat,
        new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
        ClaimValueTypes.Integer64)
            };
            claim.AddRange(userRole.Select(role => new Claim(ClaimTypes.Role, role)));
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: configuration["Jwt:issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claim,
                signingCredentials: signingCredentials,
                expires: expire
                );
            JwtSecurityTokenHandler token = new JwtSecurityTokenHandler();
            string tokeWrite = token.WriteToken(jwtSecurityToken);

            return new AuthenticationRespose()
            {
                token = tokeWrite,
                email = user.Email,
                name = user.UserName
            };


        }
      



    }
}
