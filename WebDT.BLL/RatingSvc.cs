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
    public class RatingSvc : GenericSvc<RatingRep, Rating>
    {
        #region -- Overrides --

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

        public SingleRsp CreateRating(RatingReq ratingRequest)
        {
            var res = new SingleRsp();
            var rating = new Rating
            {
                UserId = ratingRequest.UserId,
                ProductId = ratingRequest.ProductId,
                RatingValue = ratingRequest.RatingValue,
                Comment = ratingRequest.Comment
            };

            res = _rep.CreateRating(rating);
            return res;
        }

        public SingleRsp UpdateRating(RatingReq ratingRequest)
        {
            var res = new SingleRsp();
            var rating = _rep.Read(ratingRequest.RatingId);
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

        #endregion
    }
}
