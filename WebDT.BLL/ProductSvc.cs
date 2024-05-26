using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Linq;
using System.Transactions;
using WebDT.Common.BLL;
using WebDT.Common.Req;
using WebDT.Common.Rsp;
using WebDT.DAL;
using WebDT.DAL.Models;

namespace WebDT.BLL
{
    public class ProductSvc : GenericSvc<ProductRep, Product>
    {
        private ProductRep productRep;
        #region -- Overrides --

        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();

            var m = _rep.Read(id);
            res.Data = m;

            return res;
        }

        public override SingleRsp Update(Product m)
        {
            var res = new SingleRsp();

            var m1 = m.ProductId > 0 ? _rep.Read(m.ProductId) : _rep.Read(m.ProductId);
            if (m1 == null)
            {
                res.SetError("EZ103", "No data.");
            }
            else
            {
                res = base.Update(m);
                res.Data = m;
            }

            return res;
        }


        #endregion

        #region  -- Methods --
        public object SearchProduct(SearchProductReq s)
        {
            var res = new SingleRsp();
            try
            {
                var products = All.Where(x => x.ProductName.Contains(s.Keyword));
                if (products != null)
                {
                    var offset = (s.Page - 1) * s.Size;
                    var total = products.Count();
                    int totalPage = (total % s.Size) == 0 ? (int)(total / s.Size) :
                        (int)(1 + (total / s.Size));
                    var data = products.OrderBy(x => x.ProductName).Skip(offset).Take(s.Size).ToList();
                    var obj = new
                    {
                        Data = data,
                        TotalRecord = total,
                        TotalPages = totalPage,
                        Page = s.Page,
                        Size = s.Size

                    };
                    res.Data = obj;
                }
                else
                {
                    res.SetMessage("Khong tim thay san pham");
                    res.SetError("404", "Khong tim thay san pham");
                }
            }
            catch (Exception ex)
            {
                res.SetMessage(ex.Message);
            }

            return res;
        }

        public SingleRsp SearchProductInPriceRange(int minPrice, int maxPrice)
        {
            var res = productRep.SearchProductInPriceRange(minPrice, maxPrice);
            return res;
        }

        public SingleRsp CreateProduct(ProductReq productReq)
        {


            var res = new SingleRsp();
            Product product = new Product();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    product.BrandId = productReq.BrandId;
                    product.ProductName = productReq.ProductName;
                    product.Price = productReq.Price;
                    product._5g = productReq._5g;
                    product.Processor = productReq.Processor;
                    product.Battery = productReq.Battery;
                    product.FastCharge = productReq.FastCharge;
                    product.Ram = productReq.Ram;
                    product.Memory = productReq.Memory;
                    product.Screen = productReq.Screen;
                    product.RefreshRate = productReq.RefreshRate;
                    product.Os = productReq.Os;
                    product.RearCamera = productReq.RearCamera;
                    product.FrontCamera = productReq.FrontCamera;
                    product.ExtendMemory = productReq.ExtendMemory;
                    res = productRep.CreateProduct(product);
                }
            }
            catch (Exception ex)
            {
                res.SetMessage(ex.Message);
            }
            return res;
        }

        public SingleRsp UpdateProduct(ProductReq productReq, string id)
        {
            int result;
            var context = new QuanLyBanDienThoaiContext();
            var res = new SingleRsp();
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    if (int.TryParse(id, out result))
                    {
                        var product = context.Products.FirstOrDefault(u => u.ProductId == result);
                        if (product != null)
                        {
                            product.BrandId = productReq.BrandId;
                            product.ProductName = productReq.ProductName;
                            product.Price = productReq.Price;
                            product._5g = productReq._5g;
                            product.Processor = productReq.Processor;
                            product.Battery = productReq.Battery;
                            product.FastCharge = productReq.FastCharge;
                            product.Ram = productReq.Ram;
                            product.Memory = productReq.Memory;
                            product.Screen = productReq.Screen;
                            product.RefreshRate = productReq.RefreshRate;
                            product.Os = productReq.Os;
                            product.RearCamera = productReq.RearCamera;
                            product.FrontCamera = productReq.FrontCamera;
                            product.ExtendMemory = productReq.ExtendMemory;
                            context.SaveChanges();
                            tran.Commit();
                            res.SetMessage("Update thanh cong");
                        }
                        else
                        {
                            res.SetMessage("Khong tim thay san pham");
                            res.SetError("404", "Khong tim thay san pham");
                        }
                    }
                    else
                    {
                        res.SetError("400", "Ma san pham khong hop le");
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    res.SetError(ex.StackTrace);
                    res.SetMessage(ex.Message);
                }
            }
            return res;
        }

        public SingleRsp DeleteProduct(int id)
        {
            var res = new SingleRsp();
            var context = new QuanLyBanDienThoaiContext();
            try
            {
                var product = context.Products.Find(id);
                if (product != null)
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                    res.SetMessage("Đã xóa sản phẩm");
                }
                else
                {
                    res.SetMessage("Không tìm thấy sản phẩm");
                    res.SetError("404", "Không tìm thấy sản phẩm");
                }
            }
            catch (Exception ex)
            {
                res.SetMessage(ex.Message);
            }
            return res;
        }
        #endregion
        public ProductSvc()
        {
            productRep = new ProductRep();
        }
    }
}

