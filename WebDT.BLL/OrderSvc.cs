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
        public SingleRsp CreateOrder(OrderReq orderReq, int userID)
        {

            var res = new SingleRsp();
            try
            {


                var order = new Order
                {
                    CustomerId = orderRep.getuserid(userID),
                    EmployeeId = orderReq.EmployeeId,
                    ShipAddress = orderReq.ShipAddress,
                    OrderDate = DateTime.UtcNow,
                    OrderDetails = orderReq.OrderDetails.Select(detail => new OrderDetail
                    {
                        ProductId = detail.ProductId,
                        UnitPrice = orderRep.getproducprice(detail.ProductId),
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
        public SingleRsp UpdateOrder(CreateOrderReq createOrderReq, int id)
        {
            var res = new SingleRsp();


            try
            {
                var order = orderRep.Read(id);
                if (order != null)
                {
                    DateTime now = DateTime.UtcNow;
                    TimeSpan diference = (TimeSpan)(order.OrderDate - now);
                    if ((int)(diference.TotalMinutes) < 30)
                        res.SetMessage("Đơn hàng đã được giao");
                    else
                    {
                        order.ShipAddress = createOrderReq.ShipAddress;
                        orderRep.UpdateOrder(order);
                        res.SetMessage("Update thanh cong");
                    }
                }

                else
                {
                    res.SetMessage("Không tìm thấy đơn hàng");
                    res.SetError("404", "\"Không tìm thấy đơn hàng");
                }
            }



            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
                res.SetMessage(ex.Message);
            }

            return res;
        }
        public SingleRsp DeleteOrder(int id)
        {
            var res = new SingleRsp();
            try
            {
                var order = orderRep.Read(id);
                if (order != null)
                {
                    DateTime now = DateTime.UtcNow;
                    TimeSpan diference = (TimeSpan)(order.OrderDate - now);
                    if ((int)(diference.TotalMinutes) < 30)
                        res.SetMessage("Đơn hàng đã được giao");
                    else
                    {
                        orderRep.DeleteOrder(order);
                        res.SetMessage("Đã xóa đơn hàng");
                    }
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
