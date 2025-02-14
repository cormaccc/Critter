using CritterWebApi.Data.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using TwitterCloneApp.Contexts;
using TwitterCloneApp.Entities.User;

namespace CritterWebApi.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly TwitterCloneEFContext _dbContext;
        private readonly IConfiguration _configuration;
        public AuthService(TwitterCloneEFContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public async Task<HttpCookie?> Authenticate(string username, string password, bool rememberMe)
        {
            var user = await IsValidUser(username, password);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiresInMinutes = rememberMe
                ? Convert.ToDouble(60 * 24 * 30)
                : Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"]);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = token.ValidTo
            };

            return new HttpCookie
            {
                Name = "JwtToken",
                TokenString = tokenString,
                CookieOptions = cookieOptions
            };
        }


        public async Task<UserEntity> IsValidUser(string username, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == username);

            if (user == null) throw new Exception("Could not authenticate");

            return user;
        }
    }
}
