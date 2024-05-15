using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using System.Linq;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser()
        {
            var order = _context.Orders.Where(o => o.CreatedAt.Year == 2003 && o.Status == OrderStatus.Delivered)
                                        .OrderByDescending(o => o.Price*o.Quantity).FirstOrDefault();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == order.UserId);
            return user;
        }
        public async Task<List<User>> GetUsers()  
        {
            return await _context.Users.Where(u => u.Orders.Any(o => o.Status == OrderStatus.Paid && o.CreatedAt.Year == 2010)).ToListAsync();
        }
    }
}
