using E_commerce.Entities;
using E_commerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartRepository _cartRepository;

    public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository)
    {
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
    }

    // moves everything from the cart into a new order (fake delivery) and empties the cart
    public async Task<Order> PlaceOrderAsync(string userId, CheckoutViewModel model)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart.Count == 0)
        {
            throw new InvalidOperationException("Your cart is empty.");
        }

        var order = new Order
        {
            UserId = userId,
            CreatedAt = DateTime.Now,
            FullName = model.FullName,
            Phone = model.Phone,
            City = model.City,
            Address = model.Address,
            DeliveryMethod = model.DeliveryMethod,
            Total = cart.Sum(c => c.Product.Price * c.Quantity),
            Items = cart.Select(c => new OrderItem
            {
                ProductId = c.ProductId,
                ProductName = c.Product.Name,
                Price = c.Product.Price,
                Quantity = c.Quantity
            }).ToList()
        };

        await _orderRepository.AddAsync(order);
        await _cartRepository.ClearAsync(userId);
        return order;
    }

    public async Task<List<Order>> GetOrdersAsync(string userId)
    {
        return await _orderRepository.GetOrdersByUserIdAsync(userId);
    }
}
