using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebDT.BLL;

using WebDT.DAL.Models;

using WebDT.Common.Req;
using WebDT.Common.Rsp;
using WebDT.DAL;


namespace WebDT.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private OrderSvc orderSvc;
        public OrderController()
        {
            orderSvc = new OrderSvc();
        }
        [HttpGet("search-order")]
        public IActionResult GetALl(int id)
        {
            var res = new SingleRsp();
            res = orderSvc.Read(id);
            return Ok(res);
        }
            [HttpPost("create-order")]
        public IActionResult CreateOrder([FromBody] OrderReq createOrderReq)
        {
            //tao bien tra ve la SingleRespone
            var rsp = new SingleRsp();
            rsp = orderSvc.CreateOrder(createOrderReq);
            return Ok(rsp);
        }
        [HttpPut("update-order")]
        public IActionResult UpdateOrder([FromBody] OrderReq orderReq, string id)
        {
            var res = orderSvc.UpdateOrder(orderReq, id);
            return Ok(res);
        }
        [HttpDelete("delete-Order")]
        public IActionResult DeleteOrder(int id)
        {
            var res = orderSvc.DeleteOrder(id);
            return Ok(res);
        }

    }

}
