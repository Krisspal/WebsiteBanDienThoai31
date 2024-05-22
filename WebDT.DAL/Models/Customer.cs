using System;
using System.Collections.Generic;

#nullable disable

namespace WebDT.DAL.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
