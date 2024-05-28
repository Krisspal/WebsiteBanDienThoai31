using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetEmployeeByALL()
        {
            var rsp = new SingleRsp();
            rsp.Data = ratingSvc.All;
            return Ok(rsp);
        }
        [HttpGet("{id}")]
        public IActionResult GetRating(int id)
        {
            var response = ratingSvc.Read(id);
            if (response.Data == null)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpPost]
        public IActionResult CreateRating(RatingReq ratingRequest)
        {
            var rsp = new SingleRsp();
            rsp = ratingSvc.CreateRating(ratingRequest);
            return Ok(rsp);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRating(int id, RatingReq ratingRequest)
        {
            ratingRequest.RatingId = id;
            var response = ratingSvc.UpdateRating(ratingRequest);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRating(int id)
        {
            var response = ratingSvc.DeleteRating(id);
            if (response.Success)
            {
                return Ok(response.Message);
            }
            return BadRequest(response.Message);
        }
    }
}
