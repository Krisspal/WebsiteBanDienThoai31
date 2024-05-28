using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDT.BLL;
using WebDT.Common.Req;
using WebDT.Common.Rsp;

namespace WebDT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderSvc orderSvc;
        public OrderController()
        {
            orderSvc = new OrderSvc();
        }
        [HttpGet("SearchOrder")]
        public IActionResult SearchOrder(int id)
        {
            var res = new SingleRsp();
            res = orderSvc.Read(id);
            return Ok(res);
        }
        [HttpPost("CreateOrder")]
        public IActionResult CreateOrder([FromBody] OrderReq createOrderReq /*[FromServices] HttpContextAccessor httpContextAccessor*/)
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var userDataClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userDataClaim?.Value;

            int result = int.Parse(userId);
            //tao bien tra ve la SingleRespone
            var rsp = new SingleRsp();
            rsp = orderSvc.CreateOrder(createOrderReq, result);
            return Ok(rsp);
        }
        //private int GetCurrentUser(HttpContextAccessor httpContextAccessor)
        //{
        //    //if(httpContextAccessor !=null && httpContextAccessor.HttpContext.User != null)
        //    //{
        //    //    var user = httpContextAccessor.HttpContext.User;
        //    //    var userId  =   int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
        //    //    return userId
        //    //}
        //    //else
        //    //    return 0;


        //}

        [HttpPut("UpdateOrder")]
        public IActionResult UpdateOrder([FromBody] CreateOrderReq createOrderReq, int id)
        {
            var res = orderSvc.UpdateOrder(createOrderReq, id);
            return Ok(res);
        }

        [HttpDelete("DeleteOrder")]
        public IActionResult DeleteOrder(int id)
        {
            var res = orderSvc.DeleteOrder(id);
            return Ok(res);
        }
        //[HttpPost("Checkout")]
        //public IActionResult Checkout([FromBody] CheckoutReq checkoutReq)
        //{
        //    var rsp = new SingleRsp();
        //    rsp = orderSvc.CompleteOrder(checkoutReq.OrderId, checkoutReq.CustomerId, checkoutReq.ShipAddress);
        //    return Ok(rsp);
        //}
    }
}
