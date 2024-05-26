using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebDT.BLL;
using WebDT.DAL.Models;
using System.Linq;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;
using System;
using WebDT.Common.Rsp;
using WebDT.Common.Req;
using Microsoft.AspNetCore.Http;
using WebDT.DAL;
using System.Net.Http;
using System.Security.Policy;

namespace WebDT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserAuthRep _userRepository;
        HttpClient httpClient = new HttpClient();

        public AuthController(IAuthService authService, IUserAuthRep userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthRsp>> Login(LoginReq request)
        {
            var user = await _userRepository.GetUserByUserNameAndPassword(request.Username, request.Password);
            var claims = await _authService.LoginAsync(request.Username, request.Password);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false,
        };
            if (user != null)
            {
                await HttpContext.SignInAsync(claims, authProperties);
                var response = new AuthRsp
                {
                    IsSuccess = true,
                    User = new UserRsp
                    {
                        UserName = claims.FindFirstValue(ClaimTypes.Name),
                        Email = claims.FindFirstValue(ClaimTypes.Email),
                        IsAdmin = claims.FindFirstValue(ClaimTypes.Role) == "Admin"
                    }
                };
                return Ok("Dang nhap thanh cong");
            }
            else
            {
                var response = new AuthRsp
                {
                    IsSuccess = false,
                    ErrorMessage = "Sai username hoac password"
                };
                return BadRequest(response);
            }
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("Dang xuat thanh cong");
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AuthRsp>>Register(RegisterReq registerReq)
        {
            var success = await _authService.RegisterAsync(registerReq);
            if (success)
            {
                var response = new AuthRsp
                {
                    IsSuccess = true,
                    User = new UserRsp
                    {
                        UserName = registerReq.UserName,
                        Email = registerReq.Email,
                        IsAdmin = false
                    }
                };
                return Ok(response);
            }
            else
            {
                var response = new AuthRsp
                {
                    IsSuccess = false,
                    ErrorMessage = "User da ton tai."
                };
                return BadRequest(response);
            }
        }
    }
}
