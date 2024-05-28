using System;
using System.Collections.Generic;
using System.Text;

namespace WebDT.Common.Req
{
    public class CreateOrderReq
    {
        public int EmployeeId { get; set; }
        public DateTime? OrderDate { get; }
        public string ShipAddress { get; set; }
    }
}