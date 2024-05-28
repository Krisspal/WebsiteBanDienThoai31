using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDT.Common.BLL;
using WebDT.Common.Req;
using WebDT.Common.Rsp;
using WebDT.DAL;
using WebDT.DAL.Models;
using static WebDT.DAL.ThongKeModel;

namespace WebDT.BLL
{
    public class ThongKeSvc : GenericSvc<ThongKeModel, Order>
    {
        private ThongKeModel thongKe;

        public ThongKeSvc()
        {
            thongKe = new ThongKeModel();
        }

        //public SingleRsp ThongKeDoanhThu(DateTime startDate, DateTime endDate)
        //{
        //    var res = thongKe.GetSaleInDateRange(startDate, endDate);
        //    return res;
        //}

        public SingleRsp ThongKeDoanhThuTheoNgay(DateTime fromDate, DateTime toDate)
        {
            var res = new SingleRsp();
            var total = thongKe.ThongKeDoanhThuTheoNgay(fromDate, toDate);
            var result = new
            {
                startDate = fromDate,
                enDate = toDate,
                TotalSale = total,

            };
            res.Data = result;
            return res;

        }

        public SingleRsp ThongKeDoanhThuTheoThang(int month, int year)
        {
            var res = new SingleRsp();
            var total = thongKe.ThongKeDoanhThuTheoThang(month, year);
            var result = new
            {
                Month = month,
                Year = year,
                TotalSale = total,

            };
            res.Data = result;
            return res;

        }
        public ProductStatistics GetProductStatistics(int productId)
        {
            var product = thongKe.GetProduct(productId);
            if (product == null)
            {
                return null;
            }

            var orderDetails = thongKe.GetOrderDetailsForProduct(productId);

            var totalOrders = orderDetails.Count();
            var totalQuantity = orderDetails.Sum(od => od.Quantity ?? 0);

            return new ProductStatistics
            {
                ProductId = productId,
                TotalOrders = totalOrders,
                TotalQuantity = totalQuantity
            };
        }
    }
}
