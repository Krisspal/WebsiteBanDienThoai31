
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace WebDT.DAL.Models
{
    public partial class OrderDetail
    {   
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int? UnitPrice { get; set; }
        public int? Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}