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
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;


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

            if (res.Data == null)

            {
                res.SetMessage("Không tìm thấy đơn hàng");
                res.SetError("404", "Không tìm thấy đơn hàng");
            }

            return res;
        }
        #endregion
        #region --Methods--
        public SingleRsp GetAll(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Read(id);
            return res;
        }
        public SingleRsp CreateOrder(OrderReq createOrderReq)
        {
            var res = new SingleRsp();
            try
            {
                var order = new Order()
                {
                    OrderId = createOrderReq.OrderId,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    ShipAddress = createOrderReq.ShipAddress
                };
                //    var orderDetails = new List<OrderDetail>();
                //    foreach(var orderDetail in createOrderReq.Details)
                //    {
                //        orderDetails.Add(new OrderDetail()
                //        {
                //            OrderId = order.OrderId,
                //            ProductId = orderDetail.ProductId,
                //            Quantity = orderDetail.Quantity,    
                //        });

                //    }
                orderRep.CreateOrder(order);
                res.SetMessage("Tạo order thành công");
            }
            catch (Exception ex)
            {
                res.SetMessage(ex.Message);
            }
            return res;
        }
        public SingleRsp UpdateOrder(OrderReq orderReq, string id)
        {
            int result;
            var context = new QuanLyBanDienThoaiContext();
            var res = new SingleRsp();
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    if (int.TryParse(id, out result))
                    {
                        var order = context.Orders.FirstOrDefault(u => u.OrderId == result);
                        if (order != null)
                        {
                            order.ShipAddress = orderReq.ShipAddress;
                            context.SaveChanges();
                            tran.Commit();
                            res.SetMessage("Update thanh cong");
                        }
                        else
                        {
                            res.SetMessage("Không tìm thấy đơn hàng");
                            res.SetError("404", "\"Không tìm thấy đơn hàng");
                        }
                    }
                    else
                    {
                        res.SetError("400", "Ma san pham khong hop le");
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    res.SetError(ex.StackTrace);
                    res.SetMessage(ex.Message);
                }
            }
            return res;
        }
        public SingleRsp DeleteOrder(int id)
        {
            var res = new SingleRsp();
            var context = new QuanLyBanDienThoaiContext();
            try
            {
                var order = context.Orders.Find(id);
                if (order != null)
                {
                    context.Orders.Remove(order);
                    context.SaveChanges();
                    res.SetMessage("Đã xóa đơn hàng");
                }
                else
                {
                    res.SetMessage("Không tìm thấy đơn hàng");
                    res.SetError("404", "Không tìm thấy đơn hàng");
                }
            }
            catch (Exception ex)
            {
                res.SetMessage(ex.Message);
            }
            return res;
        }
        //public SingleRsp GetAllOrders(Order order)
        //{
        //    var res = new SingleRsp();

        //    try
        //    {
        //        using (var context = new QuanLyBanDienThoaiContext())
        //        {
        //            var orders = context.Orders.Include(o => o.OrderDetails).ToList();
        //            res.Data = orders;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        res.SetMessage(ex.Message);
        //    }

        //    return res;
        //}

        //#endregion
        public OrderSvc() 
        {
            orderRep = new OrderRep();
            //var res = new SingleRsp();
            //res.Data = _rep.Read(id);
            //return res;
        }

        //#region --Methods--
        //public SingleRsp CreateOrder(OrderReq orderreq, OrderDetail orderDetail)
        //{
        //    var res = new SingleRsp();
        //    Order o = new Order();
        //    using( var context = new QuanLyBanDienThoaiContext())
        //    {
        //        using(TransactionScope scope = new TransactionScope())
        //            try
        //            {
        //                o.OrderId = order.OrderId;

        //            }    

        //    }
        //}

        #endregion
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
