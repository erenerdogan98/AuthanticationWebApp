using AuthanticationWebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthanticationWebAPI.Services
{
    public class AuthService(UserManager<IdentityUser> userManager,IConfiguration configuration) : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        private readonly IConfiguration _configuration = configuration;

        public string GenerateTokenString(UserLogin user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.UserName),
                new Claim(ClaimTypes.Role,"Admin"),
            };

            // for security key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));

            // for sign in credantial
            var signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                issuer:_configuration.GetSection("Jwt:Issuer").Value,
                audience: _configuration.GetSection("Jwt:Audience").Value,
                signingCredentials:signingCredentials
                );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }

        public async Task<bool> Login(UserLogin user)
        {
            // finding user..
            var identityUser = await _userManager.FindByEmailAsync(user.UserName);
            if (identityUser == null)
            {
                return false;
            }

            // checking password
            return await _userManager.CheckPasswordAsync(identityUser, user.Password);
        }

        public async Task<bool> Register(UserLogin user)
        {
            var identityUser = new IdentityUser
            {
                UserName = user.UserName,
                Email = user.UserName
            };

            string password = user.Password;
            var result = await _userManager.CreateAsync(identityUser,password) ?? throw new InvalidDataException("add logging ..");
            return result.Succeeded; // if result is not succeeded it will return false
        }
    }
}
