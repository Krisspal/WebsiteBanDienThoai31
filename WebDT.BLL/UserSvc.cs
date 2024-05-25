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

        public SingleRsp UpdateUser(int id, UserReq userReq)
        {
            var res = new SingleRsp();
            var user = userRep.Read(id);
            user.UserName = userReq.UserName;
            user.Email = userReq.Email;
            user.Password = userReq.Password;
            user.IsAdmin = userReq.IsAdmin;
            //Nếu isAdmin khác 0 hoặc 1 thì gán mặc định là 0
            if (userReq.IsAdmin != 0 && userReq.IsAdmin != 1)
                user.IsAdmin = 0;
            res = userRep.UpdateUser(user);
            return res;
        }

        public SingleRsp DeleteUser(int id)
        {
            var res = new SingleRsp();

            try
            {
                // Find the existing employee
                var user = userRep.Read(id);

                if (user == null)
                {
                    res.SetError("Khong tim thay user");
                }
                // Delete the employee from the database
                userRep.DeleteUser(user);
                res.SetMessage("Xoa user thanh cong");
            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
                res.SetMessage("Failed to delete employee.");
            }

            return res;
        }

        public SingleRsp GetUserByUsername(string username)
        {
            var res = new SingleRsp();
            res.Data = userRep.GetUserByUsername(username);
            if (res.Data == null)

            {
                res.SetMessage("Khong tim thay user");
                res.SetError("404", "Khong tim thay user");
            }
            return res;
        }
    }
}