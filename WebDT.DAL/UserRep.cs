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

        public override User Read(int id)
        {
            var res = All.FirstOrDefault(e => e.UserId == id);
            return res;
        }
    //    public SingleRsp CreateUser(User user)
    //    {
    //        var res = new SingleRsp();
    //        var context = new QuanLyBanDienThoaiContext();
    //        using (var tran = context.Database.BeginTransaction())
    //        {
    //            try
    //            {
    //                context.Users.Add(user);
    //                context.SaveChanges();
    //                tran.Commit();
    //                res.SetMessage("Tao user thanh cong");
    //            }
    //            catch (Exception ex)
    //            {
    //                tran.Rollback();
    //                res.SetError(ex.StackTrace);
    //                res.SetMessage("Tao user that bai");
    //            }
    //        }
    //        return res;
    //    }
    //    public SingleRsp UpdateUser(User user)
    //    {
    //        var res = new SingleRsp();
    //        using (var context = new QuanLyBanDienThoaiContext())
    //        {
    //            using (var tran = context.Database.BeginTransaction())
    //            {
    //                try
    //                {
    //                    context.Users.Update(user);
    //                    context.SaveChanges();
    //                    tran.Commit();
    //                    res.SetMessage("Update user thanh cong");
    //                }
    //                catch (Exception ex)
    //                {
    //                    tran.Rollback();
    //                    res.SetError(ex.StackTrace);
    //                    res.SetMessage("Update user that bai");
    //                }
    //            }
    //        }
    //        return res;
    //    }

    //    public SingleRsp DeleteUser(User user)
    //    {
    //        var res = new SingleRsp();
    //        using (var context = new QuanLyBanDienThoaiContext())
    //        {
    //            using (var tran = context.Database.BeginTransaction())
    //            {
    //                try
    //                {
    //                    context.Users.Remove(user);
    //                    context.SaveChanges();
    //                    tran.Commit();
    //                    res.SetMessage("Da xoa user");
    //                }
    //                catch (Exception ex)
    //                {
    //                    tran.Rollback();
    //                    res.SetError(ex.StackTrace);
    //                    res.SetMessage("Xoa that bai");
    //                }
    //            }
    //        }
    //        return res;
    //    }

    //    public User GetUserByUsername(string username)
    //    {
    //        var res = All.FirstOrDefault(e => e.UserName == username); 
    //        return res;
    //    }
    }
}