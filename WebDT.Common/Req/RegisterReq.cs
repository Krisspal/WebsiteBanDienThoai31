using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace WebDT.Common.Req
{
    public class RegisterReq
    {
        //Gan cho bang user
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //gan cho bang Customer
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }


    }
}
