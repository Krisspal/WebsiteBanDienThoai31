using System;
using System.Collections.Generic;
using System.Text;

namespace WebDT.DAL.Models
{
    public class Cart
    {
        public string SessionId { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
