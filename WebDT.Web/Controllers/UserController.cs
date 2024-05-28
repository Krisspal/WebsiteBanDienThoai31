using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebDT.BLL;
using WebDT.Common.Req;
using WebDT.Common.Rsp;

namespace WebDT.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserSvc userSvc;
        public UserController()
        {
            userSvc = new UserSvc();
        }

        [HttpPost]
        [Route("{id}")]
        public IActionResult GetUserByID(int id)
        {
            try
            {
                var res = new SingleRsp();
                res = userSvc.Read(id);
                return Ok(res);
            }
            catch (Exception)
            {
                return BadRequest("Nhap id la so nguyen");
               
            }
        }

        [HttpPost("GetUserByUsername")]
        public IActionResult GetUserByUsername(string username)
        {
            var res = new SingleRsp();
            res = userSvc.GetUserByUsername(username);
            return BadRequest(res);
        }

        [HttpGet("GetAllUser")]
        public IActionResult GetAllUser()
        {
            var res = new SingleRsp();
            res.Data = userSvc.All;
            return Ok(res);
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser(UserReq userReq)
        {
            var res = userSvc.CreateUser(userReq);
            return Ok(res);
        }

        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserReq userReq)
        {
            var res = userSvc.UpdateUser(id, userReq);
            return Ok(res);
        }

        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            var res = userSvc.DeleteUser(id);
            return Ok(res);
        }
    }
}
