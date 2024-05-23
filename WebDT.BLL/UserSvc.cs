using WebDT.Common.BLL;
using WebDT.Common.Rsp;
using System;
using System.Collections.Generic;
using System.Text;
using WebDT.DAL;
using WebDT.DAL.Models;
using System.Net.Http;
using WebDT.Common.Req;
using System.Linq;

namespace WebDT.BLL
{
    public class UserSvc : GenericSvc<UserRep, User>
    {
        UserRep userRep;

        public UserSvc()
        {
            userRep = new UserRep();
        }

        //Lấy user theo ID truyền vào
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Read(id);

            if (res.Data == null)

            {
                res.SetMessage("Khong tim thay user");
                res.SetError("404", "Khong tim thay user");
            }

            return res; 
        }

        public SingleRsp CreateUser(UserReq userReq)
        {
            var res = new SingleRsp();
            User user = new User();
            user.UserName = userReq.UserName; ;
            user.Email = userReq.Email;
            user.Password = userReq.Password;
            user.IsAdmin = userReq.IsAdmin;
            //Nếu isAdmin khác 0 hoặc 1 thì gán mặc định là 0
            if (userReq.IsAdmin != 0 && userReq.IsAdmin != 1)
                user.IsAdmin = 0;
            userRep.CreateUser(user);
            return res;
        }

        public SingleRsp UpdateUser(UserReq userReq)
        {
            var res = new SingleRsp();
            User user = new User();
            user.UserName = userReq.UserName;
            user.Email = userReq.Email;
            user.Password = userReq.Password;
            user.IsAdmin = userReq.IsAdmin;
            //Nếu isAdmin khác 0 hoặc 1 thì gán mặc định là 0
            if (userReq.IsAdmin != 0 && userReq.IsAdmin != 1)
                user.IsAdmin = 0;
            userRep.UpdateUser(user);
            return res;
        }

        public SingleRsp DeleteUser(int id)
        {
            var res = new SingleRsp();
            var context = new QuanLyBanDienThoaiContext();
            var user = context.Users.Find(id);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
                res.SetMessage("Da xoa user");
            }
            else
            {
                res.SetMessage("Khong tim thay user");
                res.SetError("404", "Khong tim thay user");
            }
            return res;
        }

    }
}