using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDT.Common.DAL;
using WebDT.Common.Rsp;
using WebDT.DAL.Models;

namespace WebDT.DAL
{
    public class RatingRep : GenericRep<QuanLyBanDienThoaiContext, Rating>
    {
        #region -- Overrides --

        public override Rating Read(int id)
        {
            return All.FirstOrDefault(r => r.RatingId == id);
        }

        #endregion

        #region -- Methods --

        public SingleRsp CreateRating(Rating rating)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyBanDienThoaiContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var r = context.Ratings.Add(rating);
                        context.SaveChanges();
                        tran.Commit();
                        res.SetMessage("Create Rating successful");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                        res.SetMessage("Create Rating failed");
                    }
                }
            }
            return res;
        }

        public SingleRsp UpdateRating(Rating rating)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyBanDienThoaiContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Ratings.Update(rating);
                        context.SaveChanges();
                        tran.Commit();
                        res.SetMessage("Update Rating successful");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                        res.SetMessage("Update Rating failed");
                    }
                }
            }
            return res;
        }

        public SingleRsp DeleteRating(Rating rating)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyBanDienThoaiContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Ratings.Remove(rating);
                        context.SaveChanges();
                        tran.Commit();
                        res.SetMessage("Delete Rating successful");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                        res.SetMessage("Delete Rating failed");
                    }
                }
            }
            return res;
        }

        #endregion
    }
}
