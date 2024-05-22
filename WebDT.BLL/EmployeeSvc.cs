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
    public class EmployeeSvc:GenericSvc<EmployeeRep,Employee>
    {
        private EmployeeRep employeeRep;
        #region -- Overrides --

        public override SingleRsp Read(int id)
        {
            var rsp = new SingleRsp();
            rsp.Data = _rep.Read(id);
            return rsp;
        }

        #endregion

        #region -- Methods --

        public EmployeeSvc() 
        {
            employeeRep = new EmployeeRep();
        }
        public SingleRsp CreateEmployee(EmployeeReq employeeReq)
        {
            var res = new SingleRsp();
            Employee e = new Employee();
            e.EmployeeId = employeeReq.EmployeeId;
            e.EmployeeName = employeeReq.EmployeeName;
            e.UserId = employeeReq.UserId;
            e.Gender = employeeReq.Gender;
            e.BirthDate = employeeReq.BirthDate;
            e.Idcard = employeeReq.Idcard;
            e.Title = employeeReq.Title;
            e.Phone = employeeReq.Phone;
            e.Salary = employeeReq.Salary;
            res = employeeRep.CreateEmployee(e);
            return res;
        }

        public SingleRsp UpdateEmployee(EmployeeReq employeeReq)
        {
            var res = new SingleRsp();
            Employee e = new Employee();
            e.EmployeeId = employeeReq.EmployeeId;
            e.EmployeeName = employeeReq.EmployeeName;
            e.UserId = employeeReq.UserId;
            e.Gender = employeeReq.Gender;
            e.BirthDate = employeeReq.BirthDate;
            e.Idcard = employeeReq.Idcard;
            e.Title = employeeReq.Title;
            e.Phone = employeeReq.Phone;
            e.Salary = employeeReq.Salary;
            res = employeeRep.UpdateEmployee(e);
            return res;
        }


        #endregion
    }
}
