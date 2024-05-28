using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using WebDT.Common.BLL;
using WebDT.Common.Req;
using WebDT.Common.Rsp;
using WebDT.DAL;
using WebDT.DAL.Models;

namespace WebDT.BLL
{
    public class RatingSvc : GenericSvc<RatingRep, Rating>
    {
        #region -- Overrides --
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RatingRep ratingRep;
        public RatingSvc()
        {
            ratingRep = new RatingRep();
        }
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Read(id);
            if (res.Data == null)
            {
                res.SetMessage("Rating not found");
                res.SetError("404", "Rating not found");
            }
            return res;
        }

        #endregion

        #region -- Methods --

        public SingleRsp CreateRating(int userid,RatingReq ratingRequest)
        {
            var res = new SingleRsp();
            //var currentUserId = GetCurrentUserId();
            var r = new Rating();

            r.UserId = userid;
            r.ProductId = ratingRequest.ProductId;
            r.RatingValue = ratingRequest.RatingValue;
            r.Comment = ratingRequest.Comment;
   

            res = _rep.CreateRating(r);
            return res;
        }

        public SingleRsp UpdateRating(int id,RatingReq ratingRequest)
        {
            var res = new SingleRsp();
            var rating = _rep.Read(id);
            if (rating == null)
            {
                res.SetMessage("Rating not found");
                res.SetError("404", "Rating not found");
                return res;
            }

            rating.RatingValue = ratingRequest.RatingValue;
            rating.Comment = ratingRequest.Comment;
            res = _rep.UpdateRating(rating);
            return res;
        }

        public SingleRsp DeleteRating(int ratingId)
        {
            var res = new SingleRsp();
            var rating = _rep.Read(ratingId);
            if (rating == null)
            {
                res.SetMessage($"Rating with ID {ratingId} not found.");
                return res;
            }

            res = _rep.DeleteRating(rating);
            return res;
        }
        //private int GetCurrentUserId()
        //{
        //    var user = _httpContextAccessor.HttpContext.User;
        //    var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
        //    return userId;
        //}
    }
        #endregion
}
