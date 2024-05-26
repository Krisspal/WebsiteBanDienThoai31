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

namespace WebDT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerSvc customerSvc;
        public CustomerController()
        {
            customerSvc = new CustomerSvc();
        }

        [HttpPost]
        [Route("{id}")]
        public IActionResult GetCustomerByID(int id)
        {
            var rsp = new SingleRsp();
            rsp = customerSvc.Read(id);
            return Ok(rsp);
        }

        [HttpGet("GetAllCustomer")]
        public IActionResult GetCustomerByALL()
        {
            var rsp = new SingleRsp();
            rsp.Data = customerSvc.All;
            return Ok(rsp);
        }

        [HttpPost("CreateEmployee")]
        public IActionResult CreateCustomer(CustomerReq customerReq)
        {
            var rsp = new SingleRsp();
            rsp = customerSvc.CreateCustomer(customerReq);
            return Ok(rsp);
        }

        [HttpPut("UpdateCustomer/{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerReq customerReq)
        {
            if (id != customerReq.CustomerId)
            {
                return BadRequest();
            }
            var rsp = new SingleRsp();
            rsp = customerSvc.UpdateCustomer(customerReq);
            return Ok(rsp);
        }

        [HttpDelete("DeleteCustomer")]
        public IActionResult DeleteCustomer(int id)
        {
            var rsp = new SingleRsp();
            rsp = customerSvc.DeleteCustomer(id);
            return Ok(rsp);
        }
    }
}
