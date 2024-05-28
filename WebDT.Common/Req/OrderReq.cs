using System;
using System.Collections.Generic;
using System.Text;

namespace WebDT.Common.Req
{
    public class OrderReq
    {
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string ShipAddress { get; set; }
    }
}
