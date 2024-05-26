using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDT.Common.DAL;
using WebDT.DAL.Models;

namespace WebDT.DAL
{
    public class CartRep : GenericRep<QuanLyBanDienThoaiContext, Cart>
    {
        public Cart GetCartBySessionId(string sessionId)
        {
            return All.FirstOrDefault(c => c.SessionId == sessionId);
        }

        public void CreateCart(Cart cart)
        {
            add(cart);
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


}
