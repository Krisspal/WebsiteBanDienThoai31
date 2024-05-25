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

        [HttpPost("CreateEmployee")]
        public IActionResult CreateEmployee(EmployeeReq employeeReq)
        {
            var rsp = new SingleRsp();
            rsp = employeeSvc.CreateEmployee(employeeReq);
            return Ok(rsp);
        }

        [HttpPost("CreateEmployeeByUserId")]
        public IActionResult CreateEmployeeWithUserId(EmployeeReq employeeReq)
        {
            var rsp = new SingleRsp();
            rsp = employeeSvc.CreateEmployeeByUserId(employeeReq);
            return Ok(rsp);
        }

        [HttpPut("UpdateEmployee/{id}")]
        public IActionResult UpdateEmployee(int id,[FromBody] EmployeeReq employeeReq)
        {
            if (id != employeeReq.EmployeeId)
            {
                return BadRequest(); 
            }
            var rsp = new SingleRsp();
            rsp = employeeSvc.UpdateEmployee(employeeReq);
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
