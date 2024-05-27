using System;
using System.Collections.Generic;
using System.Text;

namespace WebDT.DAL.Models
{
    public class AddToCartRequest
    {
        public string sessionId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
