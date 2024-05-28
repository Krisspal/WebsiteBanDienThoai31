using System;
using System.Collections.Generic;
using System.Text;

namespace WebDT.Common.Req
{
    public class CheckoutReq
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string ShipAddress { get; set; }
    }
}
