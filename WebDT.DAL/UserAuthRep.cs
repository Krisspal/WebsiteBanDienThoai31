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

