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
    public class BrandSvc : GenericSvc<BrandRep, Brand>
    {
        BrandRep brandRep;

        public BrandSvc()
        {
            brandRep = new BrandRep();
        }

        //Lấy brand theo ID truyền vào
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Read(id);

            if (res.Data == null)

            {
                res.SetMessage("Khong tim thay brand");
                res.SetError("404", "Khong tim thay brand");
            }

            return res;
        }

        public SingleRsp CreateBrand(BrandReq brandReq)
        {
            var res = new SingleRsp();
            Brand brand = new Brand();
            brand.BrandName = brandReq.BrandName;
            brand.Country = brandReq.Country;
            brandRep.CreateBrand(brand);
            return res;
        }

        public SingleRsp UpdateBrand(int id, BrandReq brandReq)
        {
            var res = new SingleRsp();
            var brand = brandRep.Read(id);
            brand.BrandName = brandReq.BrandName;
            brand.Country = brandReq.Country;
            res = brandRep.UpdateBrand(brand);
            return res;
        }

        public SingleRsp DeleteBrand(int id)
        {
            var res = new SingleRsp();

            try
            {
                // Find the existing employee
                var brand = brandRep.Read(id);

                if (brand == null)
                {
                    res.SetError("Khong tim thay brand");
                }
                // Delete the employee from the database
                brandRep.DeleteBrand(brand);
                res.SetMessage("Xoa brand thanh cong");
            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
                res.SetMessage("Xoa brand that bai");
            }

            return res;
        }
    }
}
