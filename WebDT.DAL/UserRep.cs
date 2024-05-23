using System;
using System.Linq;
using WebDT.Common.DAL;
using WebDT.Common.Rsp;
using WebDT.DAL.Models;

namespace WebDT.DAL
{
    public class UserRep : GenericRep<QuanLyBanDienThoaiContext, User>
    {
        public UserRep() { }
        //Lấy user theo ID truyền vào 
        public override User Read(int id)
        {
            var res = All.FirstOrDefault(u => u.UserId == id);
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
        public SingleRsp CreateUser(User user)
        {
            var res = new SingleRsp();
            var context = new QuanLyBanDienThoaiContext();
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                    tran.Commit();
                    res.SetMessage("Tao thanh cong");
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    res.SetError(ex.StackTrace);
                    res.SetMessage("Tao that bai");
                }
            }
            return res;
        }
        public SingleRsp UpdateUser(User user)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyBanDienThoaiContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Users.Update(user);
                        context.SaveChanges();
                        tran.Commit();
                        res.SetMessage("Update thanh cong");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                        res.SetMessage("Update that bai");
                    }
                }
            }
            return res;
        }
    }
}