using QLBH.Common.BLL;
using QLBH.Common.Rsp;
using System;
using System.Collections.Generic;
using System.Text;
using WebDT.DAL;
using WebDT.DAL.Models;

namespace WebDT.BLL
{
    public class UserSvc : GenericSvc<UserRep,User>
    {
        private UserRep userRep;
        public UserSvc() 
        { 
            userRep = new UserRep();
        }

        //Lấy user theo ID truyền vào
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Read(id);
            return res; 
        }
    }
}
