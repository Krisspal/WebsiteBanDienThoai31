using System;
using System.Collections.Generic;
using System.Text;

namespace WebDT.Common.Req
{
    public class OrderDetailReq
    {
        public int ProductId { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
 }
