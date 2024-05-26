using System;
using System.Collections.Generic;
using System.Text;

namespace WebDT.DAL.Models
{
    public class CheckoutRequest
    {
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public string ShipAddress { get; set; }
    }

}
