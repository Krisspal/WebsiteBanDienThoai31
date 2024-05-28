using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebDT.DAL.Models;

namespace WebDT.BLL
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
    }

}
