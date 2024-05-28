using System;
using System.Collections.Generic;
using System.Text;
using WebDT.DAL;
using WebDT.Common.BLL;
using WebDT.DAL.Models;
using System.Linq;
using WebDT.Common.Rsp;
using WebDT.Common.Req;

namespace WebDT.BLL
{
    public class OrderSvc : GenericSvc<OrderRep,Order>
    {
        private OrderRep orderRep;
        public OrderSvc()
        {
            orderRep = new OrderRep();
        }

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
        public SingleRsp CreateOrder(OrderReq orderReq)
        {
            var res = new SingleRsp();
            try
            {
                var order = new Order
                {
                    CustomerId = orderReq.CustomerId,
                    EmployeeId = orderReq.EmployeeId,
                    ShipAddress = orderReq.ShipAddress,
                    OrderDate = DateTime.UtcNow,
                    OrderDetails = orderReq.OrderDetails.Select(detail => new OrderDetail
                    {
                        ProductId = detail.ProductId,
                        UnitPrice = detail.UnitPrice,
                        Quantity = detail.Quantity
                    }).ToList()
                };
                res = orderRep.CreateOrder(order);
                res.SetMessage("Tạo order thành công");
            }
            catch (Exception ex)
            {
                res.SetMessage(ex.Message);
            }
            return res;
        }
        //public SingleRsp CompleteOrder(int orderId, int customerId, string shipAddress)
        //{
        //    var res = orderRep.CompleteOrder(orderId, customerId, shipAddress);
        //    return res;
        //}
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
        #endregion
    }
}
