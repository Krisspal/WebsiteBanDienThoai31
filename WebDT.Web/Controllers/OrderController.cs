using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebDT.BLL;
<<<<<<< HEAD
using WebDT.DAL.Models;
=======
using WebDT.Common.Req;
using WebDT.Common.Rsp;
using WebDT.DAL;
>>>>>>> master

namespace WebDT.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
<<<<<<< HEAD
        private readonly IOrderService _orderService;
=======
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
>>>>>>> master

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("get-all-order")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("search-order")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            var newOrder = await _orderService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.OrderId }, newOrder);
        }

        [HttpPut("update-order")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            var updatedOrder = await _orderService.UpdateOrderAsync(order);
            return Ok(updatedOrder);
        }

        [HttpDelete("detele-order")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }

}
