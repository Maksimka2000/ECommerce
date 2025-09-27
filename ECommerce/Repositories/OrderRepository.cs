using E_commerce.Context;
using E_commerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly EdbContext _context;

    public OrderRepository(EdbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.Items)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }
}
