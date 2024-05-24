using System;
using System.Collections.Generic;
using System.Text;
using WebDT.DAL;
using WebDT.Common.BLL;
using WebDT.DAL.Models;

namespace WebDT.BLL
{
    public class OrderSvc : GenericSvc<OrderRep,Order>
    {
        private OrderRep orderRep;
        public OrderSvc() 
        {
            orderRep = new OrderRep();  
        }
    }
}
