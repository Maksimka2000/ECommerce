using E_commerce.Entities;
using E_commerce.Models;

namespace ECommerce.Services;

public interface IOrderService
{
    Task<Order> PlaceOrderAsync(string userId, CheckoutViewModel model);
    Task<List<Order>> GetOrdersAsync(string userId);
}
