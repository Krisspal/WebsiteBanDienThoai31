using System;
using System.Collections.Generic;
using System.Text;

namespace WebDT.Common.Req
{
<<<<<<< HEAD
    public class OrderReq
    {
        public int OrderId { get; }
=======
    public  class OrderReq
    {
        public int OrderId { get; set; }
>>>>>>> master
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string ShipAddress { get; set; }
    }
}
