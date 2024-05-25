using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebDT.DAL;
using WebDT.DAL.Models;
using Microsoft.AspNetCore.SignalR;
using static System.Net.WebRequestMethods;
using System.Net.Http;


namespace WebDT.BLL
{
    public class AuthService : IAuthService
    {
        private IUserRepository _userRepository;
        private readonly HttpContext httpContext;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        

        public async Task<ClaimsPrincipal> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAndPassword(email, password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.IsAdmin == 1 ? "Admin" : "User")
                };

                var claimsIdentity = new ClaimsIdentity(claims, /*Explicit*/CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                };

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                return claimsPrincipal;
            }
            else
            {
                return null;
            }
        }
    }

    public interface IAuthService
    {
        Task<ClaimsPrincipal> LoginAsync(string email, string password);
    }
}
