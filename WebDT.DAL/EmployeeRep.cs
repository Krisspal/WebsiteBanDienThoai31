using System;
using System.Linq;
using WebDT.Common.DAL;
using WebDT.Common.Rsp;
using WebDT.DAL.Models;

namespace WebDT.DAL
{
    public class EmployeeRep : GenericRep<QuanLyBanDienThoaiContext, Employee>
    {
        #region -- Overrides --


        public override Employee Read(int id)
        {
            var res = All.FirstOrDefault(e => e.EmployeeId == id);
            return res;
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
                        res.SetMessage("Tao Employee thanh cong");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                        res.SetMessage("Tao Employee that bai");
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
                        context.Employees.Update(employee);
                        context.SaveChanges();
                        tran.Commit();
                        res.SetMessage("Update Employee thanh cong");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                        res.SetMessage("Update Employee that bai");
                    }
                }
            }
            return res;
        }
        public SingleRsp DeleteEmployee(Employee employee)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyBanDienThoaiContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Employees.Remove(employee);
                        context.SaveChanges();
                        tran.Commit();
                        res.SetMessage("Da xoa nhan vien");

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                        res.SetMessage("Xoa that bai");
                    }
                }
            }
            return res;
        }
        public int GetNextUserId()
        {
            using (var context = new QuanLyBanDienThoaiContext())
            {
                int latestUserId = 0;
                var lastEmployee = context.Employees.OrderByDescending(e => e.UserId).FirstOrDefault();
                if (lastEmployee != null)
                {
                    latestUserId = (int)lastEmployee.UserId;
                }
                return latestUserId;
            }

        }

        public Employee GetEmployeeByID(int id)
        {
            var employee = All.FirstOrDefault(e => e.EmployeeId == id);
            return employee;
        }
        #endregion
    }
}
