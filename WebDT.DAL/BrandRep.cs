using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDT.Common.DAL;
using WebDT.Common.Rsp;
using WebDT.DAL.Models;

namespace WebDT.DAL
{
    public class BrandRep : GenericRep<QuanLyBanDienThoaiContext, Brand>
    {
        public override Brand Read(int id)
        {
            var res = All.FirstOrDefault(b => b.BrandId == id);
            return res;
        }

        //public SingleRsp SearchProductByBrandName(string brandName)
        //{
        //    var res = new SingleRsp();
        //    res.Data = All.Where(b => b.BrandName.Contains(brandName).Join()
        //    return res;
        //}

        #region -- Methods --

        public SingleRsp CreateBrand(Brand brand)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyBanDienThoaiContext())
            {
                var checkbrand = context.Brands.FirstOrDefault(u => u.BrandName == brand.BrandName);
                if (checkbrand == null)
                {
                    using (var tran = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var e = context.Brands.Add(brand);
                            context.SaveChanges();
                            tran.Commit();
                            res.SetMessage("Tao brand thanh cong");
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            res.SetError(ex.StackTrace);
                            res.SetMessage("Tao brand that bai");
                        }
                    }
                }
                else
                    res.SetMessage("Brand da ton tai");
            }
            return res;
        }
        public SingleRsp UpdateBrand(Brand brand)
        {
            var res = new SingleRsp();

            using (var context = new QuanLyBanDienThoaiContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Brands.Update(brand);
                        context.SaveChanges();
                        tran.Commit();
                        res.SetMessage("Update Brand thanh cong");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                        res.SetMessage("Update Brand that bai");
                    }
                }
            }
            return res;
        }
        public SingleRsp DeleteBrand(Brand brand)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyBanDienThoaiContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Brands.Remove(brand);
                        context.SaveChanges();
                        tran.Commit();
                        res.SetMessage("Da xoa brand");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                        res.SetMessage("Xoa brand");
                    }
                }
            }
            return res;
        }

        #endregion
    }
}

