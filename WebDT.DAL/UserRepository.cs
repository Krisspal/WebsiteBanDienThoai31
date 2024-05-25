using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebDT.DAL.Models;

namespace WebDT.DAL
{
    public class UserRepository : IUserRepository
    {
        QuanLyBanDienThoaiContext dbcontext = new QuanLyBanDienThoaiContext();

        public UserRepository(QuanLyBanDienThoaiContext context)
        {
            dbcontext = context;
        }

        public async Task<User> GetUserByEmailAndPassword(string email, string password)
        {
            return await dbcontext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }

    public interface IUserRepository
    {
        Task<User> GetUserByEmailAndPassword(string email, string password);
    }
}

