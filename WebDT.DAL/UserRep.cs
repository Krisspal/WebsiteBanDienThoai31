using WebDT.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDT.DAL.Models;
using WebDT.Common.Rsp;

namespace WebDT.DAL
{
    public class UserRep : GenericRep<QuanLyBanDienThoaiContext, User>
    {
        public UserRep() { }
        //Lấy user theo ID truyền vào 
        public override User Read(int id)
        {
            var res = All.FirstOrDefault(u => u.UserId==id);
            return res;
        }
        //public SingleRsp CreateUser(User user)
        //{
        //    var res = new SingleRsp();
        //    using (var context = new QuanLyBanDienThoaiContext())
        //    {
        //        using (var tran = context.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                var p = context.Users.Add(user);
        //                context.SaveChanges();
        //                tran.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                tran.Rollback();
        //                res.SetError(ex.StackTrace);
        //            }
        //        }
        //    }
        //    return res;
        //}
    }
}
