using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDT.BLL;
using WebDT.Common.Req;
using WebDT.Common.Rsp;

namespace WebDT.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private BrandSvc brandSvc;
        public BrandController()
        {
            brandSvc = new BrandSvc();
        }

        [HttpPost]
        [Route("{id}")]
        public IActionResult GetBrandByID(int id)
        {
            var rsp = new SingleRsp();
            rsp = brandSvc.Read(id);
            return Ok(rsp);
        }

        [AllowAnonymous]
        [HttpGet("GetAllBrand")]
        public IActionResult GetBrandAll()
        {
            var rsp = new SingleRsp();
            rsp.Data = brandSvc.All;
            return Ok(rsp);
        }

        [HttpPost("CreateBrand")]
        public IActionResult CreateBrand(BrandReq brandReq)
        {
            var rsp = new SingleRsp();
            rsp = brandSvc.CreateBrand(brandReq);
            return Ok(rsp);
        }

        [HttpPut("UpdateBrand/{id}")]
        public IActionResult UpdateBrand(int id, [FromBody] BrandReq brandReq)
        {
            var res = brandSvc.UpdateBrand(id, brandReq);
            return Ok(res);
        }

        [HttpDelete("DeleteBrand")]
        public IActionResult DeleteBrand(int id)
        {
            var res = brandSvc.DeleteBrand(id);
            return Ok(res);
        }
    }
}
