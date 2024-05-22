using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDT.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDT.Common.Rsp;
using WebDT.Common.Req;
using WebDT.DAL;

namespace WebsiteBanDienThoai31.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeeSvc employeeSvc;
        public EmployeeController() 
        {
            employeeSvc = new EmployeeSvc();
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeByID([FromBody] SimpleReq simpleReq)
        {
            var rsp = new SingleRsp();
            rsp = employeeSvc.Read(simpleReq.Id);
            return Ok(rsp);
        }
        [HttpGet("")]
        public IActionResult GetEmployeeByALL()
        {
            var rsp = new SingleRsp();
            rsp.Data = employeeSvc.All;
            return Ok(rsp);
        }
        [HttpPost("")]
        public IActionResult CreateEmployee([FromBody] EmployeeReq employeeReq)
        {
            var rsp = new SingleRsp();
            rsp = employeeSvc.CreateEmployee(employeeReq);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult EditEmployee([FromBody] EmployeeReq employeeReq)
        {
            var rsp = new SingleRsp();
            rsp = employeeSvc.UpdateEmployee(employeeReq);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee()
        {
            return Ok();
        }
    }
}
