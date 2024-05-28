using System;
using System.Collections.Generic;
using System.Text;
using WebDT.Common.DAL;
using WebDT.DAL.Models;
using System.Linq;
using WebDT.Common.Rsp;
using Microsoft.EntityFrameworkCore;

namespace WebDT.DAL
{
    public class OrderRep : GenericRep<QuanLyBanDienThoaiContext, Order>
    {
        #region -- Overrides --
        public override Order Read(int id)
        {
            var res = All.FirstOrDefault(p => p.OrderId == id);
            return res;
        }


        public int Remove(int id)
        {
            var m = base.All.First(i => i.OrderId == id);
            m = base.Delete(m);
            return m.OrderId;
        }

        #endregion
        #region -- Methods --
        public SingleRsp CreateOrder(Order order)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyBanDienThoaiContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Orders.Add(order);
                        context.SaveChanges();
                        tran.Commit();
                        res.SetMessage("Order created successfully");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetMessage(ex.Message);
                        res.SetMessage("Failed to create order");
                    }
                }
            }
            return res;
        }
        public SingleRsp UpdateOrder(Order order)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyBanDienThoaiContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Orders.Update(order);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }
        public SingleRsp DeleteOrder(Order order)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyBanDienThoaiContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Orders.Remove(order);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }
        //public SingleRsp CompleteOrder(int orderId, int customerId, string shipAddress)
        //{
        //    var res = new SingleRsp();
        //    using (var context = new QuanLyBanDienThoaiContext())
        //    {
        //        using (var tran = context.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                var order = context.Orders.Include(o => o.OrderDetails).FirstOrDefault(o => o.OrderId == orderId);
        //                if (order == null)
        //                {
        //                    res.SetMessage("Order not found");
        //                    res.SetError("404", "Order not found");
        //                    return res;
        //                }

        //                order.CustomerId = customerId;
        //                order.ShipAddress = shipAddress;
        //                order.OrderDate = DateTime.UtcNow;

        //                context.SaveChanges();
        //                tran.Commit();
        //                res.Data = order;
        //                res.SetMessage("Order completed successfully");
        //            }
        //            catch (Exception ex)
        //            {
        //                tran.Rollback();
        //                res.SetError(ex.StackTrace);
        //                res.SetMessage("Failed to complete order");
        //            }
        //        }
        //    }
        //    return res;
        //}
        #endregion
    }
}
