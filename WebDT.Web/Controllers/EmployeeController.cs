using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDT.BLL;
using WebDT.Common.Req;
using WebDT.Common.Rsp;

namespace WebsiteBanDienThoai31.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : ControllerBase
    {
        //Phuong thuc khoi tao de lien ket voi lop BLL
        private EmployeeSvc employeeSvc;
        public EmployeeController()
        {
            employeeSvc = new EmployeeSvc();
        }

        [HttpPost]
        [Route("{id}")]
        public IActionResult GetEmployeeByID(int id)
        {
            var rsp = new SingleRsp();
            rsp = employeeSvc.Read(id);
            return Ok(rsp);
        }

        [HttpGet("GetAllEmployee")]
        public IActionResult GetEmployeeByALL()
        {
            var rsp = new SingleRsp();
            rsp.Data = employeeSvc.All;
            return Ok(rsp);
        }

        //[HttpPost("CreateEmployee")]
        //public IActionResult CreateEmployee(EmployeeReq employeeReq)
        //{
        //    var rsp = new SingleRsp();
        //    rsp = employeeSvc.CreateEmployee(employeeReq);
        //    return Ok(rsp);
        //}

        [HttpPost("CreateEmployee")]
        public IActionResult CreateEmployeeWithUserId(EmployeeReq employeeReq)
        {
            var rsp = new SingleRsp();
            rsp = employeeSvc.CreateEmployee(employeeReq);
            return Ok(rsp);
        }

        [HttpPut("UpdateEmployee/{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] EmployeeReq employeeReq)
        {

            var rsp = new SingleRsp();
            rsp = employeeSvc.UpdateEmployee(id,employeeReq);
            return Ok(rsp);
        }

        [HttpDelete("DeleteEmployee")]
        public IActionResult DeleteEmployee(int id)
        {
            var rsp = new SingleRsp();
            rsp = employeeSvc.DeleteEmployee(id);
            return Ok(rsp);
        }
    }
}
