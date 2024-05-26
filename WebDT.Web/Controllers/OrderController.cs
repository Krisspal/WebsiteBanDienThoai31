using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDT.BLL;
using WebDT.Common.Req;
using WebDT.Common.Rsp;
using WebDT.DAL;

namespace WebDT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderSvc osvc;
        [HttpPost("create-order")]
        
        public IActionResult CreateOrder([FromBody] CreateOrderReq createOrderReq) {
            {
                var rsp = new SingleRsp();
                rsp = osvc.CreateOrder(createOrderReq);
                return Ok(rsp);
            }
        }
        [HttpPost("Get-All")]
        public IActionResult GetAll([FromBody] OrderRep orderRep)
        {
            //tao bien tra ve la SingleRespone
            var rsp = new SingleRsp();
            //Goi ham Read o lop Svc
            rsp = (SingleRsp)osvc.GetAll(orderRep);
            return Ok(rsp);
        }
        public OrderController()
        {
            osvc = new OrderSvc();

        }
         
            
    }
}
