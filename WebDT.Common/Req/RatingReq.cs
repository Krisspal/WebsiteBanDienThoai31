using System;
using System.Collections.Generic;
using System.Text;

namespace WebDT.Common.Req
{
    public class RatingReq
    {
        //public int RatingId { get; set; }
        //public int UserId { get; set; }
        public int ProductId { get; set; }
        public int? RatingValue { get; set; }
        public string Comment { get; set; }
    }
}
