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
        private UserSvc userSvc;
        private EmployeeSvc employeeSvc;
        private CustomerSvc customerSvc;
        //HttpContext httpContext;

        public AuthController(IAuthService authService, IUserAuthRep userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
            userSvc = new UserSvc();
            employeeSvc = new EmployeeSvc();
            customerSvc = new CustomerSvc();
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthRsp>> Login(string username, string password)
        {
            var user = await _userRepository.GetUserByUserNameAndPassword(username, password);
            var claims = await _authService.LoginAsync(username, password);
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
                return BadRequest("Sai username hoac password");
            }
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("Dang xuat thanh cong");
        }

        [HttpGet("GetCurrentUser")]
        public ActionResult<CurrentUserRsp> GetCurrentLogInUser()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var userDataClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userDataClaim?.Value;
            var res = new CurrentUserRsp();
            if (userId != null)
            {
                User user = userSvc.GetUserByID(int.Parse(userId));
                if (user.IsAdmin == 1)
                {
                    var CurrentUser = employeeSvc.GetEmployeeByID(int.Parse(userId));
                    res.UserName = user.UserName;
                    res.Email = user.Email;
                    if (CurrentUser == null)
                        res.Name = null;
                    else
                        res.Name = CurrentUser.EmployeeName;
                }
                else
                {
                    var CurrentUser = customerSvc.GetCustomerByID(int.Parse(userId));
                    res.UserName = user.UserName;
                    res.Email = user.Email;
                    if (CurrentUser == null)
                        res.Name = null;
                    else
                        res.Name = CurrentUser.CustomerName;
                }
            }
            return Ok(res);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AuthRsp>> Register(RegisterReq registerReq)
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