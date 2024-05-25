using System;
using System.Collections.Generic;
using System.Text;
using WebDT.DAL;
using WebDT.Common.BLL;
using WebDT.DAL.Models;
using static WebDT.BLL.OrderSvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using WebDT.Common.Rsp;
using System.Transactions;
using WebDT.Common.Req;

namespace WebDT.BLL
{
    public class OrderSvc : GenericSvc<OrderRep, Order>
    {
        private OrderRep orderRep;
        #region --Override--
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Read(id);
            return res;
        }
        #endregion

        #region --Methods--
        public SingleRsp CreateOrder(OrderReq orderreq, OrderDetail orderDetail)
        {
            var res = new SingleRsp();
            Order o = new Order();
            using( var context = new QuanLyBanDienThoaiContext())
            {
                using(TransactionScope scope = new TransactionScope())
                    try
                    {
                        o.OrderId = order.OrderId;

                    }    

            }
        }
        #endregion
        public OrderSvc()
        {
            orderRep = new OrderRep();
        }
        //public class OrderSvc : IOrderService
        //{
        //    private readonly QuanLyBanDienThoaiContext _context;

        //    public OrderSvc(QuanLyBanDienThoaiContext context)
        //    {
        //        _context = context;
        //    }

        //    public async Task<List<Order>> GetAllOrdersAsync()
        //    {
        //        return await _context.Orders.ToListAsync();
        //    }

        //    public async Task<Order> GetOrderByIdAsync(int id)
        //    {
        //        return await _context.Orders.FindAsync(id);
        //    }

        //    public async Task<Order> CreateOrderAsync(Order order)
        //    {
        //        _context.Orders.Add(order);
        //        await _context.SaveChangesAsync();
        //        return order;
        //    }

        //    public async Task<Order> UpdateOrderAsync(Order order)
        //    {
        //        _context.Orders.Update(order);
        //        await _context.SaveChangesAsync();
        //        return order;
        //    }

        //    public async Task DeleteOrderAsync(int id)
        //    {
        //        var order = await _context.Orders.FindAsync(id);
        //        _context.Orders.Remove(order);
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
    }
