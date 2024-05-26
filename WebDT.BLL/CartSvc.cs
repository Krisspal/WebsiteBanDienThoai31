using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDT.Common.BLL;
using WebDT.DAL;
using WebDT.DAL.Models;

namespace WebDT.BLL
{
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

            cart = null;
        }

        public CartSvc(CartRep cartRep, OrderDetailRep orderDetailRep, OrderRep orderRep)
        {
            _cartRep = cartRep;
            _orderDetailRep = orderDetailRep;
            _orderRep = orderRep;
        }
    }
}
