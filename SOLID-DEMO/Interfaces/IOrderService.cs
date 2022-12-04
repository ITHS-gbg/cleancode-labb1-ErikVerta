using Shared;

namespace Server.Interfaces;

public interface IOrderService : IService
{
    Task<IEnumerable<Order>> GetAllOrders();
    Task<IEnumerable<Order>> GetOrdersForCustomer(int id);
    Task CreateOrder(CustomerCart customerCart);
    Task DeleteOrder(int id);
    Task AddToOrder(CustomerCart itemsToAdd, int id);
    Task RemoveFromOrder(CustomerCart itemsToRemove, int id);
}