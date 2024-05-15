using Microsoft.EntityFrameworkCore;
using System.Linq;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Order> GetOrder()
        {
            return await _context.Orders.OrderByDescending(o => o.CreatedAt).FirstOrDefaultAsync(o => o.Quantity > 1);

        }

        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.Where(o => o.User.Status == UserStatus.Active).OrderBy(o => o.CreatedAt).ToListAsync();
        }
    }
}
