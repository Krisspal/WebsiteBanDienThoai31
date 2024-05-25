using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDT.Common.DAL;
using WebDT.Common.Rsp;
using WebDT.DAL.Models;


namespace WebDT.DAL
{
    public class OrderDetailRep : GenericRep<QuanLyBanDienThoaiContext, OrderDetail>
    {
        #region -- Overrides --


        public override OrderDetail Read(int id)
        {
            var res = All.FirstOrDefault(p => p.OrderId == id);
            return res;
        }


        public int Remove(int id)
        {
            var m = base.All.First(i => i.OrderId == id);
            m = base.Delete(m);
            return m.ProductId;
        }
        #endregion
        #region -- Methods --
        public SingleRsp CreateProduct(OrderDetail orderDetail)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyBanDienThoaiContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.OrderDetails.Add(orderDetail);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetMessage(ex.Message);
                    }
                }
            }
            return res;

        }
        #endregion
    }
}
