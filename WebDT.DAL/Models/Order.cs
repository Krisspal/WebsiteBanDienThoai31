using System;
using System.Collections.Generic;

#nullable disable

namespace WebDT.DAL.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string ShipAddress { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }x
    }
}
