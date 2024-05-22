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

        [HttpPost("Lay User theo ID")]
        public IActionResult GetUserByID([FromBody] SimpleReq simpleReq)
        {
            var res = new SingleRsp();
            return Ok(res);
        }
    }
}
