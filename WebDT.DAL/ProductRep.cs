using System;
using System.Linq;
using WebDT.Common.DAL;
using WebDT.Common.Rsp;
using WebDT.DAL.Models;

namespace WebDT.DAL
{
    public class ProductRep : GenericRep<QuanLyBanDienThoaiContext, Product>
    {
        #region -- Overrides --


        public override Product Read(int id)
        {
            var res = All.FirstOrDefault(p => p.ProductId == id);
            return res;
        }


        public int Remove(int id)
        {
            var m = base.All.First(i => i.ProductId == id);
            m = base.Delete(m);
            return m.ProductId;
        }

        #endregion
        #region -- Methods --
        public SingleRsp CreateProduct(Product product)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyBanDienThoaiContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Products.Add(product);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetMessage(ex.Message);
                    }
                }
            }
            return res;

        }
        public SingleRsp SearchProduct(string keyWord)
        {
            var res = new SingleRsp();
            res.Data = All.Where(x => x.ProductName.Contains(keyWord));
            return res;
        }

        public SingleRsp SearchProductByBrandName(string brandName)
        {
            var res = new SingleRsp();
            QuanLyBanDienThoaiContext context = new QuanLyBanDienThoaiContext();
            try
            {
                var brand = context.Brands.FirstOrDefault(b => b.BrandName.Contains(brandName));
                if (brand == null)
                {
                    res.SetMessage("Khong tim thay brand");

                }
                else
                    res.Data = context.Products.Where(p => p.BrandId == brand.BrandId);
            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
                res.SetMessage("Khong tim thay brand");
            }
            return res;
        }

        public SingleRsp SearchProductInPriceRange(int minPrice, int maxPrice)
        {
            var res = new SingleRsp();

            using (var context = new QuanLyBanDienThoaiContext())
            {
                if (minPrice > maxPrice)
                {
                    res.SetError("400", "Nhap gia min cao hon gia max");
                }
                else
                {
                    res.Data = context.Products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
                    if (res.Data == null)
                    {
                        res.SetError("404", "Khong tim thay san pham");
                    }
                }
            }
            return res;
        }
        public SingleRsp UpdateProduct(Product product)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyBanDienThoaiContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Products.Update(product);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }
        public SingleRsp DeleteProduct(Product product)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyBanDienThoaiContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Products.Remove(product);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }
        public Product GetProductByID(int id)
        {
            var product = All.FirstOrDefault(p => p.ProductId == id);
            return product;
        }
        #endregion

    }
}
