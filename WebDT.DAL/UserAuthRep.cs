using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDT.Common.Rsp;
using WebDT.DAL.Models;

namespace WebDT.DAL
{
    public class UserAuthRep : IUserAuthRep
    {
        QuanLyBanDienThoaiContext dbcontext = new QuanLyBanDienThoaiContext();

        public UserAuthRep(QuanLyBanDienThoaiContext context)
        {
            dbcontext = context;
        }

        public async Task<User> GetUserByUserNameAndPassword(string username, string password)
        {
            return await dbcontext.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }

        public async Task<User> CheckExistUser(string username, string email, string phone)
        {
            return await dbcontext.Users.FirstOrDefaultAsync(u => u.UserName == username || u.Email == email);
        }

        public async Task<Customer> CheckExistCustomer(string phone)
        {
            return await dbcontext.Customers.FirstOrDefaultAsync(c => c.Phone == phone);
        }

        //public SingleRsp Register(User user, Customer customer)
        //{
        //    var res = new SingleRsp();
        //    var context = new QuanLyBanDienThoaiContext();
        //    using (var tran = context.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            context.Users.Add(user);
        //            context.Customers.Add(customer);
        //            context.SaveChanges();
        //            tran.Commit();
        //            //var newCus = context.Customers.OrderByDescending(c => c.CustomerId).FirstOrDefault();
        //            //var newUser = context.Users.OrderByDescending(c => c.UserId).FirstOrDefault();
        //            //if (newCus != null && newUser != null)
        //            //{
        //            //    newCus.UserId = newUser.UserId;
        //            //}
        //            //context.SaveChanges();
        //            //tran.Commit();
        //            res.SetMessage("Dang ky thanh cong");
        //        }
        //        catch (Exception ex)
        //        {
        //            tran.Rollback();
        //            res.SetError(ex.StackTrace);
        //            res.SetMessage("Dang ky that bai");
        //        }
        //    }
        //    return res;   
        //}

        public async Task<int> CreateUserAsync(User user)
        {
            dbcontext.Users.Add(user);
            return await dbcontext.SaveChangesAsync();
        }

        public async Task<int> CreateCustomerAsync(Customer customer)
        {
            dbcontext.Customers.Add(customer);
            return await dbcontext.SaveChangesAsync();
        }
    }

    public interface IUserAuthRep
    { 
        Task<User> GetUserByUserNameAndPassword(string email, string password);
        Task<User> CheckExistUser(string username, string email, string phone);
        Task<Customer> CheckExistCustomer(string phone);
        Task<int> CreateUserAsync(User user);
        Task<int> CreateCustomerAsync(Customer customer);
    }
}

