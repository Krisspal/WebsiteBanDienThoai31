using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var products = All.Where(x => x.ProductName.Contains(s.Keyword));
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
            return res;
        }

        public SingleRsp CreateProduct(ProductReq productReq)
        {
            var res = new SingleRsp();
            Product product = new Product();
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
            return res;
        }
        public SingleRsp UpdateProduct(ProductReq productReq)
        {
            var res = new SingleRsp();
            Product product = new Product();
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
            res = productRep.UpdateProduct(product);
            return res;
        }
        public SingleRsp DeleteProduct(int id)
        {
            var res = new SingleRsp();
            var context = new QuanLyBanDienThoaiContext();
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
            return res;
        }
        #endregion
        public ProductSvc() 
        { 
            productRep = new ProductRep();
        }
    }
}
