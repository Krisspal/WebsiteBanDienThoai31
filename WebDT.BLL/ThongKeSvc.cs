using System;
using System.Collections.Generic;
using System.Text;
using WebDT.Common.BLL;
using WebDT.Common.Req;
using WebDT.Common.Rsp;
using WebDT.DAL;
using WebDT.DAL.Models;

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
    }
}
