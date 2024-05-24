using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDT.BLL;
using WebDT.Common.Req;
using WebDT.Common.Rsp;

namespace WebDT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductSvc productSvc;
        public ProductController()
        {
            productSvc = new ProductSvc();
        }
        [HttpPost("search-product")]
        public IActionResult SearchProduct([FromBody] SearchProductReq searchProductReq)
        {
            //tao bien tra ve la SingleRespone
            var rsp = new SingleRsp();
            //Goi ham Read o lop Svc
            rsp = (SingleRsp)productSvc.SearchProduct(searchProductReq);
            return Ok(rsp);
        }
        [HttpPost("create-product")]
        public IActionResult CreateProduct([FromBody] ProductReq productReq)
        {
            //tao bien tra ve la SingleRespone
            var rsp = new SingleRsp();
            rsp = productSvc.CreateProduct(productReq);
            return Ok(rsp);
        }
        [HttpPut("update-product")]
        public IActionResult UpdateProduct([FromBody] ProductReq reqProduct, string id)
        {
            var res = productSvc.UpdateProduct(reqProduct,id);
            return Ok(res);
        }
        [HttpDelete("delete-product")]
        public IActionResult DeleteProduct(int id)
        {
            var res = productSvc.DeleteProduct(id);
            return Ok(res);
        }
    }

}
