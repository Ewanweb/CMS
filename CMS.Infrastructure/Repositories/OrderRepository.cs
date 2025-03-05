using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Admin.Orders;
using CMS.Domain.Admin.Repository;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Repositories
{
    public class OrderRepository(DataContext context) : Repository<Order>(context), IOrderRepository
    {
        private readonly DataContext _context = context;

        public async Task<IEnumerable<Order>?> GetUserOrders(string userId)
        {
            var order = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderDetails)
                .ToListAsync();

            return order;
        }
    }
}
