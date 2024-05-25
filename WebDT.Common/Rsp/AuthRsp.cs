using System;
using System.Collections.Generic;
using System.Text;

namespace WebDT.Common.Rsp
{
    public class AuthRsp
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public UserRsp User { get; set; }
    }
}
