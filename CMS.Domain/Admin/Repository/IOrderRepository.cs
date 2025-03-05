using CMS.Domain.Admin.Orders;

namespace CMS.Domain.Admin.Repository;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>?> GetUserOrders(string userId);
}