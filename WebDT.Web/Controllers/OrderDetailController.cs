using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebDT.BLL;
using WebDT.DAL.Models;

namespace WebDT.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailController : ControllerBase
    {
        private readonly OrderDetailSvc _orderDetailService;

        public OrderDetailController(OrderDetailSvc orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet("search-orderDetail")]
        public async Task<IActionResult> GetOrderDetailsByOrderId(int orderId)
        {
            var orderDetails = await _orderDetailService.GetOrderDetailsByOrderIdAsync(orderId);
            return Ok(orderDetails);
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrderDetail(OrderDetail orderDetail)
        {
            var newOrderDetail = await _orderDetailService.CreateOrderDetailAsync(orderDetail);
            return CreatedAtAction(nameof(GetOrderDetailsByOrderId), new { orderId = newOrderDetail.OrderId }, newOrderDetail);
        }

        [HttpPut("update-order")]
        public async Task<IActionResult> UpdateOrderDetail(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderId)
            {
                return BadRequest();
            }

            var updatedOrderDetail = await _orderDetailService.UpdateOrderDetailAsync(orderDetail);
            return Ok(updatedOrderDetail);
        }

        [HttpDelete("delete-order")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            await _orderDetailService.DeleteOrderDetailAsync(id);
            return NoContent();
        }
    }

}
