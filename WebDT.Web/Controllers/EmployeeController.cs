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

        [HttpPost("{id}")]
        public IActionResult GetEmployeeByID([FromBody] SimpleReq simpleReq)
        {
            //tao bien tra ve la SingleRespone
            var rsp = new SingleRsp();
            //Goi ham Read o lop Svc
            rsp = employeeSvc.Read(simpleReq.Id);
            return Ok(rsp);
        }
        [HttpGet("")]
        public IActionResult GetEmployeeByALL()
        {
            //tao bien tra ve la SingleRespone
            var rsp = new SingleRsp();
            //Goi ham o lop Svc
            rsp.Data = employeeSvc.All;
            return Ok(rsp);
        }
        [HttpPost("")]
        public IActionResult CreateEmployee([FromBody] EmployeeReq employeeReq)
        {
            //tao bien tra ve la SingleRespone
            var rsp = new SingleRsp();
            //Goi ham o lop Svc
            rsp = employeeSvc.CreateEmployee(employeeReq);
            return Ok(rsp);
        }
        [HttpPut("{id}")]
        public IActionResult EditEmployee([FromBody] EmployeeReq employeeReq)
        {
            var rsp = new SingleRsp();
            rsp = employeeSvc.UpdateEmployee(employeeReq);
            return Ok();
        }
        
        
    }
}
