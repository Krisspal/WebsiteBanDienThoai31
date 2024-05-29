using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDT.BLL;
using WebDT.Common.Req;
using WebDT.Common.Rsp;
using WebDT.DAL;
using WebDT.DAL.Models;

namespace WebDT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private RatingSvc ratingSvc;
        public RatingController()
        {
            ratingSvc = new RatingSvc();
        }
        [HttpGet("GetAllRating")]
        public IActionResult GetRatingAll()
        {
            var rsp = new SingleRsp();
            rsp.Data = ratingSvc.All;
            return Ok(rsp);
        }
        [HttpPost]
        [Route("GetRatingByID")]
        public IActionResult GetRatingByID(int id)
        {
            var response = ratingSvc.Read(id);
            if (response.Data == null)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Data);
        }

        //[HttpPost("CreateRating")]
        //public IActionResult CreateRating(RatingReq ratingRequest)
        //{
        //    var rsp = new SingleRsp();
        //    rsp = ratingSvc.CreateRating(ratingRequest);
        //    return Ok(rsp);
        //}

        [HttpPut("UpdateRating")]
        public IActionResult UpdateRating(int id, RatingReq ratingRequest)
        {
            var rsp = new SingleRsp();
            rsp = ratingSvc.UpdateRating(id,ratingRequest);
            return Ok(rsp);
        }

        [HttpDelete("DeleteRating")]
        public IActionResult DeleteRating(int id)
        {
            var response = ratingSvc.DeleteRating(id);
            if (response.Success)
            {
                return Ok(response.Message);
            }
            return BadRequest(response.Message);
        }
        [HttpPost("CreateByUserLogin")]
        public IActionResult CreateRating(RatingReq ratingRequest, [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var rsp = new SingleRsp();

            try
            {
                var user = httpContextAccessor.HttpContext.User;
                var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
                //var currentUserId = GetCurrentUserId(httpContextAccessor);

                rsp = ratingSvc.CreateRating(userId, ratingRequest);
            }
            catch (System.Exception ex)
            {

                return BadRequest("User chua login");
            }
            return Ok(rsp);
        }

        //private int GetCurrentUserId(IHttpContextAccessor httpContextAccessor)
        //{
        //    // Assuming you have a claims-based authentication system
        //    if (httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.User != null)
        //    {
        //        var user = httpContextAccessor.HttpContext.User;
        //        var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
        //        return userId;
        //    }
        //    else
        //    {
        //        // Handle the case where HttpContext or User is null
        //        // You can return a default value or throw an exception, depending on your requirements
        //        return 0;
        //    }
        //}
    }
}
