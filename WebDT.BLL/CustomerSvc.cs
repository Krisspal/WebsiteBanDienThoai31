using System;
using System.Collections.Generic;
using System.Text;
using WebDT.Common.Req;
using WebDT.Common.Rsp;
using WebDT.DAL.Models;
using WebDT.DAL;
using WebDT.Common.BLL;
using System.Linq;
using System.Security.Cryptography;

namespace WebDT.BLL
{
    public class CustomerSvc : GenericSvc<CustomerRep, Customer>
    {
        private CustomerRep customerRep;
        private EmployeeRep employeeRep;
        #region -- Overrides --

        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Read(id);
            if (res.Data == null)
            {
                res.SetMessage("Khong tim thay customer");
                res.SetError("404", "Khong tim thay customer");
            }
            return res;
        }

        #endregion

        #region -- Methods --

        public CustomerSvc()
        {
            customerRep = new CustomerRep();
            employeeRep = new EmployeeRep();
        }
        //public SingleRsp CreateCustomer(CustomerReq customerReq)
        //{
        //    var res = new SingleRsp();
        //    Customer c = new Customer();

        //    c.UserId = customerReq.UserId;
        //    c.CustomerName = customerReq.CustomerName;
        //    c.Phone = customerReq.Phone;
        //    c.Address = customerReq.Address;

        //    res = customerRep.CreateCustomer(c);

        //    return res;
        //}
        public SingleRsp CreateCustomerWithUserID(CustomerReq customerReq)
        {
            var res = new SingleRsp();

            try
            {
                Customer c = new Customer();
                int nextUserId = customerRep.GetNextUserId();

                var existingEmployee = employeeRep.All.FirstOrDefault(e => e.UserId == nextUserId);
                if (existingEmployee != null)
                {
                    res.SetError($"The UserId {nextUserId} is already associated with an employee.");
                    return res;
                }
                var existingCustomer = customerRep.All.FirstOrDefault(x => x.UserId == nextUserId);
                if (existingCustomer != null)
                {
                    res.SetError($"Customer with UserId {nextUserId} already exists.Please create new User");
                    return res;
                }

                c.CustomerName = customerReq.CustomerName;
                c.UserId = nextUserId;
                c.Phone = customerReq.Phone;
                c.Address = customerReq.Address;

                res = customerRep.CreateCustomer(c);
            }
            catch (Exception ex)
            {

                res.SetError(ex.StackTrace);
                res.SetMessage("Failed to create customer.");
            }

            return res;
        }

        public SingleRsp UpdateCustomer(int id,CustomerReq customerReq)
        {

            var res = new SingleRsp();
            //Employee ex = new Employee();
            var cx = customerRep.Read(id);
            //Cap nhat
            if(cx != null)
            {
                try
                {
                    cx.CustomerName = customerReq.CustomerName;
                    //cx.UserId = customerReq.UserId;
                    cx.Phone = customerReq.Phone;
                    cx.Address = customerReq.Address;
                    res = customerRep.UpdateCustomer(cx);
                }
                catch (Exception ex)
                {

                    res.SetMessage("Failed to update customer.");
                }
            }
            else
            {
                res.SetMessage("Failed to update customer.");
            }    
            return res;
        }

        public SingleRsp DeleteCustomer(int customerId)
        {
            var rsp = new SingleRsp();

            try
            {
                // Find the existing employee
                var customer = customerRep.Read(customerId);

                if (customer == null)
                {
                    rsp.SetError($"customer with ID {customerId} not found.");
                    return rsp;
                }
                // Delete the employee from the database
                customerRep.DeleteCustomer(customer);
                rsp.SetMessage("Customer deleted successfully.");
            }
            catch (Exception ex)
            {
                rsp.SetError(ex.StackTrace);
                rsp.SetMessage("Failed to delete customer.");
            }

            return rsp;
        }

        public Customer GetCustomerByID(int id)
        {
            Customer customer = customerRep.GetCustomerByID(id);
            if (customer == null)
            {
                return null;
            }
            return customer;
        }
        #endregion
    }
}
