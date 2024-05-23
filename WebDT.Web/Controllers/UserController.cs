using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDT.Common.Req;
using WebDT.Common.Rsp;
using WebDT.BLL;

namespace WebDT.Web.Controllers
{
    [Route("api/[controller]")]
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
            var res = new SingleRsp();
            res = userSvc.Read(id);
            return Ok(res);
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
        public IActionResult UpdateUser([FromBody] UserReq userReq)
        {
            var res = userSvc.UpdateUser(userReq);
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
