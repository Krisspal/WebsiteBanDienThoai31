using System;
using System.Collections.Generic;

#nullable disable

namespace WebDT.DAL.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
        }

        public int EmployeeId { get; set; }
        public int? UserId { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Idcard { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public int? Salary { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
