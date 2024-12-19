using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SharedData.Data.Models;
using SharedData.Data;

namespace API_Timono_App.Service
{
    public class TokenServiceImp : ITokenService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Account> _userManager;
        private readonly IConfiguration _config;

        public TokenServiceImp(AppDbContext context, UserManager<Account> userManager, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
        }

        public async  Task<string> GenerateToken(Account account)
        {
            //generate token

            List<Claim> userClaims = new List<Claim>();
            userClaims.Add(new Claim(ClaimTypes.NameIdentifier, account.Id));
            userClaims.Add(new Claim(ClaimTypes.Name, account.UserName));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var userRoles = await _userManager.GetRolesAsync(account); // this is for authorization

            foreach (var roleName in userRoles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, roleName));
            }


            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecreteKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            //design token
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                  issuer: _config["JWT:IssuerIP"],
                  audience: _config["JWT:AudianceIP"], //angular
                  expires: DateTime.UtcNow.AddMonths(6),
                  claims: userClaims,
                  signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        }
    }
}
    

