using System;
using System.Collections.Generic;
using System.Text;

namespace WebDT.DAL.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
    }
}
