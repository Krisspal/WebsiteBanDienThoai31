using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebDT.Common.BLL;
using WebDT.Common.DAL;
using WebDT.DAL.Models;

Data Access Layer (DAL):
csharp
Copy
public class CartRep : GenericRep<QuanLyBanDienThoaiContext, Cart>
{
    public Cart GetCartBySessionId(string sessionId)
    {
        return All.FirstOrDefault(c => c.SessionId == sessionId);
    }

    public void CreateCart(Cart cart)
    {
        Add(cart);
    }

    public void UpdateCart(Cart cart)
    {
        Update(cart);
    }

    public void DeleteCartItem(Cart cart, int productId)
    {
        var item = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            cart.CartItems.Remove(item);
            Update(cart);
        }
    }
}

public class OrderDetailRep : GenericRep<QuanLyBanDienThoaiContext, OrderDetail>
{
    public void CreateOrderDetail(OrderDetail orderDetail)
    {
        Add(orderDetail);
    }
}

public class OrderRep : GenericRep<QuanLyBanDienThoaiContext, Order>
{
    public int CreateOrder(Order order)
    {
        Add(order);
        return order.OrderId;
    }
}
Business Logic Layer (BLL):
csharp
Copy
public class CartSvc : GenericSvc<CartRep, Cart>
{
    private CartRep _cartRep;
    private OrderDetailRep _orderDetailRep;
    private OrderRep _orderRep;

    public Cart GetCart(string sessionId)
    {
        return _cartRep.GetCartBySessionId(sessionId);
    }

    public void AddToCart(string sessionId, int productId, int quantity)
    {
        var cart = _cartRep.GetCartBySessionId(sessionId);
        if (cart == null)
        {
            cart = new Cart { SessionId = sessionId };
            _cartRep.CreateCart(cart);
        }

        var item = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
        if (item == null)
        {
            cart.CartItems.Add(new CartItem { ProductId = productId, Quantity = quantity });
        }
        else
        {
            item.Quantity += quantity;
        }

        _cartRep.UpdateCart(cart);
    }

    public void RemoveFromCart(string sessionId, int productId)
    {
        var cart = _cartRep.GetCartBySessionId(sessionId);
        if (cart != null)
        {
            _cartRep.DeleteCartItem(cart, productId);
        }
    }

    public void Checkout(string sessionId, int customerId, int employeeId, string shipAddress)
    {
        var cart = _cartRep.GetCartBySessionId(sessionId);
        if (cart == null || cart.CartItems.Count == 0)
            return;

        var order = new Order
        {
            CustomerId = customerId,
            EmployeeId = employeeId,
            OrderDate = DateTime.Now,
            ShipAddress = shipAddress
        };

        var orderId = _orderRep.CreateOrder(order);

        foreach (var item in cart.CartItems)
        {
            var orderDetail = new OrderDetail
            {
                OrderId = orderId,
                ProductId = item.ProductId,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity
            };

            _orderDetailRep.CreateOrderDetail(orderDetail);
        }

        _cartRep.DeleteCartBySessionId(sessionId);
    }

    public CartSvc(CartRep cartRep, OrderDetailRep orderDetailRep, OrderRep orderRep)
    {
        _cartRep = cartRep;
        _orderDetailRep = orderDetailRep;
        _orderRep = orderRep;
    }
}
Controller Layer:
csharp
Copy
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