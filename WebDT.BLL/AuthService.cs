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
using WebDT.Common.Req;
using WebDT.Common.Rsp;
using Microsoft.AspNetCore.Mvc;


namespace WebDT.BLL
{
    public class AuthService : IAuthService
    {
        private IUserAuthRep _userAuthRep;

        public AuthService(IUserAuthRep userRepository)
        {
            _userAuthRep = userRepository;
        }

        public async Task<ClaimsPrincipal> LoginAsync(string username, string password)
        {
            var user = await _userAuthRep.GetUserByUserNameAndPassword(username, password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.IsAdmin == 1 ? "Admin" : "Customer")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
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

        public interface IAuthService
        {
            Task<ClaimsPrincipal> LoginAsync(string email, string password);
            Task<bool> RegisterAsync(RegisterReq registerReq);
        }

        public async Task<bool> RegisterAsync(RegisterReq registerReq)
        {
            var existingUser = await _userAuthRep.CheckExistUser(registerReq.UserName, registerReq.Email, registerReq.Phone);
            var existingCustomer = await _userAuthRep.CheckExistCustomer(registerReq.Phone);
            if (existingUser != null || existingCustomer != null)
            {
                return false; // User already exists
            }

            var newUser = new User
            {
                UserName = registerReq.UserName,
                Email = registerReq.Email,
                Password = registerReq.Password,
                IsAdmin = 0
            };

            var userResult = await _userAuthRep.CreateUserAsync(newUser);

            if (userResult > 0)
            {
                var newCustomer = new Customer
                {
                    UserId = newUser.UserId,
                    CustomerName = registerReq.CustomerName,
                    Phone = registerReq.Phone,
                    Address = registerReq.Address
                };

                var customerResult = await _userAuthRep.CreateCustomerAsync(newCustomer);
                return customerResult > 0;
            }
            else
            {
                return false;
            }
        }

    }
    public interface IAuthService
    {
        Task<ClaimsPrincipal> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(RegisterReq registerReq);
    }

}

