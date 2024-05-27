using System;
using System.Collections.Generic;
using System.Text;
using WebDT.DAL;
using WebDT.Common.BLL;
using WebDT.DAL.Models;
using WebDT.Common.Rsp;

namespace WebDT.BLL
{
    //public class OrderSvc : GenericSvc<OrderRep,Order>
    //{
    //    private OrderRep orderRep;
    //    public OrderSvc() 
    //    {
    //        orderRep = new OrderRep();  
    //    }

    //    public SingleRsp CreateOrder(CreateOrderReq createOrderReq)
    //    {
    //        var res = new SingleRsp();


    //        var order = new Order()
    //        {
    //            OrderDate = DateTime.Now,
    //            ShipAddress = createOrderReq.ShipAddress
    //            Details = createOrderReq.Details;
    //        };
    //        var orderDetails = new List<OrderDetail>();
    //        foreach (var orderDetail in Or)
    //        {
    //            orderDetails.Add(new OrderDetail()
    //            {
    //                OrderId = order.OrderId,
    //                ProductId = orderDetail.ProductId,
    //                Quantity = orderDetail.Quantity,
    //            });

    //        }
    //        res = orderRep.CreateOrder(order);
    //        return res;
    //    }
    //}
}
