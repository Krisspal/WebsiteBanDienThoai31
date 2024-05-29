using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebDT.Common.DAL;
using WebDT.DAL.Models;

namespace WebDT.DAL
{
    public class ThongKeModel : GenericRep<QuanLyBanDienThoaiContext, Order>
    {
        private QuanLyBanDienThoaiContext context;

        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int? TotalSale { get; set; }

        public class ProductStatistics
        {
            public int ProductId { get; set; }
            public int TotalOrders { get; set; }
            public int TotalQuantity { get; set; }
            public decimal TotalRevenue { get; set; }
        }


        public ThongKeModel() 
        { 
            context = new QuanLyBanDienThoaiContext();
        }


        //public SingleRsp GetSaleInDateRange(DateTime fromDate, DateTime toDate)
        //{
        //    var result = context.OrderDetails
        //        .Join(context.Orders,
        //            od => od.OrderId,
        //            o => o.OrderId,
        //            (od, o) => new
        //            {
        //                OrderDate = o.OrderDate,
        //                TotalPrice = od.Quantity * od.UnitPrice
        //            })
        //        .Where(x => x.OrderDate >= fromDate && x.OrderDate <= toDate)
        //        .GroupBy(x => x.OrderDate)
        //        .Select(g => new ThongKe
        //        {
        //            OrderDate = g.Key,
        //            Total = g.Sum(x => x.TotalPrice)
        //        })
        //        .ToList();

        //}
        public int? ThongKeDoanhThuTheoNgay(DateTime fromDate,  DateTime toDate)
        {
            var total = context.Orders.Where(s => s.OrderDate >= fromDate && s.OrderDate <= toDate)
                            .Join(context.OrderDetails, o => o.OrderId, od => od.OrderId, (o, od) => (od.Quantity * od.UnitPrice))
                            .Sum();
            return total;
        }

        public int? ThongKeDoanhThuTheoThang(int month, int year)
        {
            var total = context.Orders.Where(s => s.OrderDate.Value.Month == month && s.OrderDate.Value.Year == year)
                            .Join(context.OrderDetails, o => o.OrderId, od => od.OrderId, (o, od) => (od.Quantity * od.UnitPrice))
                            .Sum();
            return total;
        }

        public Product GetProduct(int productId)
        {
            var context = new QuanLyBanDienThoaiContext();
            return context.Products.FirstOrDefault(p => p.ProductId == productId);
        }

        public IEnumerable<OrderDetail> GetOrderDetailsForProduct(int productId)
        {
            var context = new QuanLyBanDienThoaiContext();
            return context.OrderDetails.Where(od => od.ProductId == productId);
        }


    }
}
