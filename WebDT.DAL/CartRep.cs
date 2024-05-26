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
            Add(cart);
        }

        public void UpdateCart(Cart cart)
        {
            Update(cart);
        }

    }
