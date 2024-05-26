using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual Employee Employee { get; set; }
<<<<<<< HEAD
=======
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
>>>>>>> master
    }
}
