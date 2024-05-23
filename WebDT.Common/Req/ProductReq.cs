using System;
using System.Collections.Generic;
using System.Text;

namespace WebDT.Common.Req
{
    public class ProductReq
    {
        public int ProductId { get; set; }
        public int BrandId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int? _5g { get; set; }
        public string Processor { get; set; }
        public int? Battery { get; set; }
        public int? FastCharge { get; set; }
        public int? Ram { get; set; }
        public int? Memory { get; set; }
        public decimal? Screen { get; set; }
        public int? RefreshRate { get; set; }
        public string Os { get; set; }
        public int? RearCamera { get; set; }
        public int? FrontCamera { get; set; }
        public int? ExtendMemory { get; set; }
        public int Quantity { get; set; }

    }
}
