using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLBH.Common.Req;
using QLBH.Common.Rsp;
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
        public IActionResult GetUserByID([FromBody] SimpleReq)
        {
            var res = new SingleRsp();
            return Ok(res);
        }
    }
}
