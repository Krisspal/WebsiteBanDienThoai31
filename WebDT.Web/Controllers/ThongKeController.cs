using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebDT.BLL;
using WebDT.Common.Rsp;
using WebDT.DAL;
using WebDT.DAL.Models;

namespace WebDT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {

        private ThongKeSvc thongkeSvc;

        public ThongKeController()
        {
            thongkeSvc = new ThongKeSvc();
        }

        [HttpPost("GetTotalSaleInDateRange")]
        public IActionResult GetTotalSaleInDateRange(DateTime fromDate, DateTime toDate)
        {
            var res = thongkeSvc.ThongKeDoanhThuTheoNgay(fromDate, toDate);
            return Ok(res);
        }

        [HttpPost("GetTotalSaleInMonth")]
        public IActionResult GetTotalSaleInMonth(int month, int year)
        {
            var res = thongkeSvc.ThongKeDoanhThuTheoThang(month, year);
            return Ok(res);
        }


    }
}
