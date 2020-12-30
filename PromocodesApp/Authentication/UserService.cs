using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PromocodesApp.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PromocodesApp.Authentication
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<string> GetId(string authorization)
        {
            var token = AuthenticationHelper.GetToken(authorization);
            var userName = AuthenticationHelper.GetUser(token, _configuration);
            var user = await _userManager.FindByNameAsync(userName);

            return user.Id;
        }
        public async Task<LoginDTO> Login(LoginRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null) return null;

            var checkPasswords = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!checkPasswords) return null;

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new LoginDTO(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
        }

        public async Task<string> Register(RegisterRequest model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            
            if (userExists != null) return "exists";

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return string.Join(",",
                result.Errors.Select(x => "Code " + x.Code + " Description" + x.Description));

            return "User created successfully.";
        }
    }
}
