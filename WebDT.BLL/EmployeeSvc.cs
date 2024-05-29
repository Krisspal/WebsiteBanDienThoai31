using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using WebDT.Common.BLL;
using WebDT.Common.Req;
using WebDT.Common.Rsp;
using WebDT.DAL;
using WebDT.DAL.Models;

namespace WebDT.BLL
{
    public class EmployeeSvc:GenericSvc<EmployeeRep,Employee>
    {
        private EmployeeRep employeeRep;
        private CustomerRep customerRep;
        #region -- Overrides --

        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Read(id);
            if (res.Data == null)
            {
                res.SetMessage("Khong tim thay employee");
                res.SetError("404", "Khong tim thay employee");
            }
            return res;
        }

        #endregion

        #region -- Methods --

        public EmployeeSvc() 
        {
            employeeRep = new EmployeeRep();
        }
        //public SingleRsp CreateEmployee(EmployeeReq employeeReq)
        //{
        //    var res = new SingleRsp();
        //    Employee e = new Employee(); 

        //    e.EmployeeName = employeeReq.EmployeeName;
        //    e.Gender = employeeReq.Gender;
        //    e.BirthDate = employeeReq.BirthDate;
        //    e.Idcard = employeeReq.Idcard;
        //    e.Title = employeeReq.Title;
        //    e.Phone = employeeReq.Phone;
        //    e.Salary = employeeReq.Salary;

        //    res = employeeRep.CreateEmployee(e);

        //    return res;
        //}
        public SingleRsp CreateEmployee(EmployeeReq employeeReq)
        {
            var res = new SingleRsp();
            try
            {
                Employee e = new Employee();
                int nextUserId = employeeRep.GetNextUserId();

                var existingCus = customerRep.All.FirstOrDefault(e => e.UserId == nextUserId);
                if (existingCus != null)
                {
                    res.SetError($"The UserId {nextUserId} is already associated with a customer.");
                    return res;
                }
                var existingEmp = employeeRep.All.FirstOrDefault(x => x.UserId == nextUserId);
                if (existingEmp != null)
                {
                    res.SetError($"Employee with UserId {nextUserId} already exists.Please create new User");
                    return res;
                }

                e.UserId = nextUserId;
                e.EmployeeName = employeeReq.EmployeeName;
                e.Gender = employeeReq.Gender;
                e.BirthDate = employeeReq.BirthDate;
                e.Idcard = employeeReq.Idcard;
                e.Title = employeeReq.Title;
                e.Phone = employeeReq.Phone;
                e.Salary = employeeReq.Salary;

                res = employeeRep.CreateEmployee(e);
                return res;
            }
            catch (Exception ex)
            {

                res.SetError(ex.StackTrace);
                res.SetMessage("Failed to create employee.");
            }

            return res;
        }

        public SingleRsp UpdateEmployee(int id,EmployeeReq employeeReq)
        {
            var res = new SingleRsp();
            try
            {
                //Employee ex = new Employee();
                var ex = employeeRep.Read(id);
                //Cap nhat
                //ex.UserId = employeeReq.UserId;
                ex.EmployeeName = employeeReq.EmployeeName;
                ex.Gender = employeeReq.Gender;
                ex.BirthDate = employeeReq.BirthDate;
                ex.Idcard = employeeReq.Idcard;
                ex.Title = employeeReq.Title;
                ex.Phone = employeeReq.Phone;
                ex.Salary = employeeReq.Salary;
                res = employeeRep.UpdateEmployee(ex);;
            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
                res.SetMessage("Failed to update employee.");
            }
            return res;
        }

        public SingleRsp DeleteEmployee(int employeeId)
        {
            var rsp = new SingleRsp();

            try
            {
                // Find the existing employee
                var employee = employeeRep.Read(employeeId);

                if (employee == null)
                {
                    rsp.SetError($"Employee with ID {employeeId} not found.");
                    return rsp;
                }
                // Delete the employee from the database
                employeeRep.DeleteEmployee(employee);
                rsp.SetMessage("Employee deleted successfully.");
            }
            catch (Exception ex)
            {
                rsp.SetError(ex.StackTrace);
                rsp.SetMessage("Failed to delete employee.");
            }

            return rsp;
        }

        public Employee GetEmployeeByID(int id)
        {
            Employee employee = employeeRep.GetEmployeeByID(id);
            if (employee == null)
            {
                return null;
            }
            return employee;
        }

        #endregion
    }
}
