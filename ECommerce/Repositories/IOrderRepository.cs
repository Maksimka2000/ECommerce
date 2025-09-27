using E_commerce.Entities;

namespace ECommerce.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<List<Order>> GetOrdersByUserIdAsync(string userId);
}
