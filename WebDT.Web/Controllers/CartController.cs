using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDT.BLL;
using WebDT.DAL.Models;

namespace WebDT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private CartSvc _cartSvc;

        public CartController(CartSvc cartSvc)
        {
            _cartSvc = cartSvc;
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            var sessionId = HttpContext.Session.Id;
            var cart = _cartSvc.GetCart(sessionId);
            return Ok(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(AddToCartRequest request)
        {
            var sessionId = HttpContext.Session.Id;
            _cartSvc.AddToCart(sessionId, request.ProductId, request.Quantity);
            return Ok();
        }

        [HttpDelete("{productId}")]
        public IActionResult RemoveFromCart(int productId)
        {
            var sessionId = HttpContext.Session.Id;
            _cartSvc.RemoveFromCart(sessionId, productId);
            return Ok();
        }

        [HttpPost("checkout")]
        public IActionResult Checkout(CheckoutRequest request)
        {
            var sessionId = HttpContext.Session.Id;
            _cartSvc.Checkout(sessionId, request.CustomerId, request.EmployeeId, request.ShipAddress);
            return Ok();
        }
    }
}
