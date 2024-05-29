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
                        List<OrderDetail> a = context.OrderDetails.Where(o => o.OrderId == order.OrderId).ToList();
                        foreach (var detail in a)
                        {
                            context.OrderDetails.Remove(detail);
                        }

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
        public int getproducprice(int id)
        {
            var context = new QuanLyBanDienThoaiContext();
            var price = context.Products.Where(o => o.ProductId == id).Select(p => p.Price).FirstOrDefault();
            int a = (int)price;
            return a;

        }
        public int getuserid(int id)
        {
            var context = new QuanLyBanDienThoaiContext();
            var price = context.Customers.Where(o => o.UserId == id).Select(p => p.CustomerId).FirstOrDefault();
            int a = (int)price;
            return a;

        }
        public Order GetOrderWithDetails(int orderId)
        {
            return All
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.OrderId == orderId);
        }
        #endregion
    }
}
