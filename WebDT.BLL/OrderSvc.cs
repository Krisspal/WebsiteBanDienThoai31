using System;
using System.Collections.Generic;
using System.Text;
using WebDT.DAL;
using WebDT.Common.BLL;
using WebDT.DAL.Models;
using WebDT.Common.Rsp;
using Microsoft.AspNetCore.Mvc;
using WebDT.Common.Req;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace WebDT.BLL
{
    public class OrderSvc : GenericSvc<OrderRep,Order>
    {
        private OrderRep orderRep;
        private readonly QuanLyBanDienThoaiContext _dbcontext;
        public OrderSvc(QuanLyBanDienThoaiContext dbContext)
        {
            var da = dbContext;
        }
        #region --Override--
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();

            var m = _rep.Read(id);
            res.Data = m;

            return res;
        }
        #endregion
        #region --Methods--
        public SingleRsp GetAll (OrderRep orderRep)
        {
            var res = All;
            return (SingleRsp)res;
        }
        public SingleRsp CreateOrder(CreateOrderReq createOrderReq)
        {
            var res = new SingleRsp();

            var order = new Order()
            {
                CustomerId = 1,
                OrderDate = DateTime.Now,
                ShipAddress = createOrderReq.ShipAddress
            };
            var orderDetails = new List<OrderDetail>();
            foreach(var orderDetail in createOrderReq.Details)
            {
                orderDetails.Add(new OrderDetail()
                {
                    OrderId = order.OrderId,
                    ProductId = orderDetail.ProductId,
                    Quantity = orderDetail.Quantity,    
                });

            }
            res = orderRep.CreateOrder(order);
            return res;
        }
        public SingleRsp GetAllOrders(Order order)
        {
            var res = new SingleRsp();

            try
            {
                using (var context = new QuanLyBanDienThoaiContext())
                {
                    var orders = context.Orders.Include(o => o.OrderDetails).ToList();
                    res.Data = orders;
                }
            }
            catch (Exception ex)
            {
                res.SetMessage(ex.Message);
            }

            return res;
        }

        #endregion
        public OrderSvc() 
        {
            orderRep = new OrderRep();  
        }
    }
}
