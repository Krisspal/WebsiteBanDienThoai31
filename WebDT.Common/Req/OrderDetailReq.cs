using System;
using System.Collections.Generic;
using System.Text;

namespace WebDT.Common.Req
{
    public class OrderDetailReq
    {
        public int OrderId { get;  }
        public int ProductId { get; set; }
        public int? UnitPrice { get; }
        public int? Quantity { get; set; }
    }
}
