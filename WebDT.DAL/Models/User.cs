using System;
using System.Collections.Generic;

#nullable disable

namespace WebDT.DAL.Models
{
    public partial class User
    {
        public User()
        {
            Customers = new HashSet<Customer>();
            Employees = new HashSet<Employee>();
            Ratings = new HashSet<Rating>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsAdmin { get; set; } = 0;

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
