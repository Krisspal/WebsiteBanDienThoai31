using System;
using System.Collections.Generic;
using System.Text;

namespace WebDT.Common.Req
{
    public class UserReq
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsAdmin { get; set; } = 0;
    }
}