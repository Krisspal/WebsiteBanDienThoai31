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

namespace WebDT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthRsp>> Login(LoginReq request)
        {
            var user = await _authService.LoginAsync(request.Email, request.Password);
            if (user != null)
            {
                var response = new AuthRsp
                {
                    IsSuccess = true,
                    User = new UserRsp
                    {
                        UserName = user.FindFirstValue(ClaimTypes.Name),
                        Email = user.FindFirstValue(ClaimTypes.Email),
                        IsAdmin = user.FindFirstValue(ClaimTypes.Role) == "Admin"
                    }
                };
                return Ok(response);
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
