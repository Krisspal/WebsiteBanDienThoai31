﻿using System;
using System.Collections.Generic;
using System.Text;
using WebDT.Common.Req;
using WebDT.Common.Rsp;
using WebDT.DAL.Models;
using WebDT.DAL;
using WebDT.Common.BLL;

namespace WebDT.BLL
{
    public class CustomerSvc : GenericSvc<CustomerRep, Customer>
    {
        private CustomerRep customerRep;
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
        }
        public SingleRsp CreateCustomer(CustomerReq customerReq)
        {
            var res = new SingleRsp();
            Customer c = new Customer();

            c.CustomerName = customerReq.CustomerName;
            c.UserId = customerReq.UserId;
            c.Phone = customerReq.Phone;
            c.Address = customerReq.Address;

            res = customerRep.CreateCustomer(c);

            return res;
        }

        public SingleRsp UpdateCustomer(CustomerReq customerReq)
        {

            var res = new SingleRsp();
            //Employee ex = new Employee();
            var cx = customerRep.Read(customerReq.CustomerId);
            //Cap nhat
            cx.CustomerName = customerReq.CustomerName;
            cx.UserId = customerReq.UserId;
            cx.Phone = customerReq.Phone;
            cx.Address = customerReq.Address;
            res = customerRep.UpdateCustomer(cx);
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
                rsp.SetMessage("Employee deleted successfully.");
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
