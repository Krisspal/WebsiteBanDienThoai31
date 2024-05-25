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

namespace WebDT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public AuthController(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthRsp>> Login(LoginReq request)
        {
            var user = await _userRepository.GetUserByEmailAndPassword(request.Email, request.Password);
            var claims = await _authService.LoginAsync(request.Email, request.Password);
            if (user != null)
            {
                await HttpContext.SignInAsync(claims);
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
                return Ok();
            }
            else
            {
                var response = new AuthRsp
                {
                    IsSuccess = false,
                    ErrorMessage = "Invalid email or password."
                };
                return BadRequest(response);
            }
        }
    }
}
