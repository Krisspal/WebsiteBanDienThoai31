using System;
using System.Collections.Generic;
using System.Text;
using WebDT.Common.DAL;
using WebDT.DAL.Models;
using System.Linq;
using WebDT.Common.Rsp;
using System.Net.WebSockets;
using WebDT.Common.Req;

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
        //#region -- Methods --
        //public SingleRsp CreateOrder(Order order)
        //{
        //    var res = new SingleRsp();
        //    using (var context = new QuanLyBanDienThoaiContext())
        //    {
        //        using (var tran = context.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                var p = context.Orders.Add(order);
        //                context.SaveChanges();
        //                tran.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                tran.Rollback();
        //                res.SetMessage(ex.Message);
        //            }
        //        }
        //    }
        //    return res;
        //}

        //#endregion
        public int CreateOrder(Order order)
        {
            add(order);
            return order.OrderId;
        }
    }

}
