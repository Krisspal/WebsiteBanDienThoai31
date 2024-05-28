using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDT.BLL;
using WebDT.Common.Req;
using WebDT.Common.Rsp;

namespace WebDT.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductSvc productSvc;
        public ProductController()
        {
            productSvc = new ProductSvc();
        }

        [AllowAnonymous]
        [HttpPost("SearchProduct")]
        public IActionResult SearchProduct([FromBody] SearchProductReq searchProductReq)
        {
            //tao bien tra ve la SingleRespone
            var rsp = new SingleRsp();
            //Goi ham Read o lop Svc
            rsp = (SingleRsp)productSvc.SearchProduct(searchProductReq);
            return Ok(rsp);
        }

        [AllowAnonymous]
        [HttpPost("SearchProductByPriceRange")]
        public IActionResult SearchProductByPriceRange(int minPrice, int maxPrice)
        {
            //tao bien tra ve la SingleRespone
            var rsp = new SingleRsp();
            //Goi ham Read o lop Svc
            rsp = productSvc.SearchProductInPriceRange(minPrice, maxPrice);
            return Ok(rsp);
        }

        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct([FromBody] ProductReq productReq)
        {
            //tao bien tra ve la SingleRespone
            var rsp = new SingleRsp();
            rsp = productSvc.CreateProduct(productReq);
            return Ok(rsp);
        }

        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct([FromBody] ProductReq reqProduct, string id)
        {
            var res = productSvc.UpdateProduct(reqProduct,id);
            return Ok(res);
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int id)
        {
            var res = productSvc.DeleteProduct(id);
            return Ok(res);
        }
    }

}
