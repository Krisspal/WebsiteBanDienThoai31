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
        public SingleRsp CreateOrder(OrderReq createOrderReq)
        {
            var res = new SingleRsp();
            try
            {
                var order = new Order()
                {
                    EmployeeId = createOrderReq.EmployeeId,
                    CustomerId = createOrderReq.CustomerId,
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
                res = orderRep.CreateOrder(order);
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
        #endregion
    }
}
