using WebDT.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDT.DAL.Models;

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

    }
}
