using System;
using System.Collections.Generic;
using System.Text;
using WebDT.Common.DAL;
using WebDT.DAL.Models;
using System.Linq;
using WebDT.Common.Rsp;

namespace WebDT.DAL
{
    public class EmployeeRep:GenericRep<QuanLyBanDienThoaiContext,Employee>
    {
        #region -- Overrides --


        public override Employee Read(int id)
        {
            var res = All.FirstOrDefault(e => e.EmployeeId == id);
            return res;
        }
        public int Remove(int id)
        {
            var m = base.All.First(e => e.EmployeeId == id);
            m = base.Delete(m);
            return m.EmployeeId;
        }
        #endregion

        #region -- Methods --

        public SingleRsp CreateEmployee(Employee employee)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyBanDienThoaiContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var e = context.Employees.Add(employee);
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
        public SingleRsp UpdateEmployee(Employee employee)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyBanDienThoaiContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Employees.Update(employee);
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

        #endregion
    }
}
