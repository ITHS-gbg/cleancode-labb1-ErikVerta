using Server.Interfaces;
using Shared;

namespace Server.Services
{
    public class OrderService : IOrderService
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public OrderService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await UnitOfWork.OrderRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersForCustomer(int id)
        {
            var orders = await UnitOfWork.OrderRepository.GetAllAsync();
            return orders.Where(o => o.Customer.Id == id);
        }
        public async Task CreateOrder(CustomerCart customerCart)
        {
            var customer = await UnitOfWork.CustomerRepository.GetAsync(customerCart.CustomerId);
            if (customer is null)
            {
                throw new NullReferenceException("customer is null");
            }
            var orderItems = new List<OrderItem>();

            foreach (var prodId in customerCart.ProductIds)
            {
                var prod = await UnitOfWork.ProductRepository.GetAsync(prodId);
                if (prod is null)
                {
                    throw new NullReferenceException("product is null");
                }

                var orderItem = orderItems.FirstOrDefault(oi => oi.Product == prod);
                if (orderItem is null)
                {
                    orderItems.Add(new OrderItem() { Product = prod, Quantity = 1 });
                }
                else
                {
                    orderItem.Quantity++;
                }
            }

            var order = new Order() { Customer = customer, OrderItems = orderItems };
            var now = DateTime.Now;
            order.ShippingDate = now.AddDays(5);

            await UnitOfWork.OrderRepository.CreateAsync(order);
            await UnitOfWork.SaveAsync();
        }

        public async Task DeleteOrder(int id)
        {
            var order = await UnitOfWork.OrderRepository.GetAsync(id);
            if (order is null)
            {
                throw new NullReferenceException("order is null");
            }

            await UnitOfWork.OrderRepository.DeleteAsync(order);
            await UnitOfWork.SaveAsync();
        }

        public async Task AddToOrder(CustomerCart itemsToAdd, int id)
        {
            var customer = await UnitOfWork.CustomerRepository.GetAsync(itemsToAdd.CustomerId);
            if (customer is null)
            {
                throw new NullReferenceException("customer is null");
            }

            var order = await UnitOfWork.OrderRepository.GetAsync(id);
            if (order is null)
            {
                throw new NullReferenceException("order is null");
            }

            foreach (var prodId in itemsToAdd.ProductIds)
            {
                var prod = await UnitOfWork.ProductRepository.GetAsync(prodId);
                if (prod is null)
                {
                    throw new NullReferenceException("product is null");
                }

                var orderItem = order.OrderItems.FirstOrDefault(oi => oi.ProductId == prodId);
                if (orderItem is null)
                {
                    order.OrderItems.Add(new OrderItem() { Product = prod, Quantity = 1 });
                }
                else
                {
                    orderItem.Quantity++;
                }
            }

            order.ShippingDate = DateTime.Now.AddDays(5);
            await UnitOfWork.SaveAsync();
        }

        public async Task RemoveFromOrder(CustomerCart itemsToRemove, int id)
        {
            var customer = await UnitOfWork.CustomerRepository.GetAsync(itemsToRemove.CustomerId);
            if (customer is null)
            {
                throw new NullReferenceException("customer is null");
            }

            var order = await UnitOfWork.OrderRepository.GetAsync(id);
            if (order is null)
            {
                throw new NullReferenceException("order is null");
            }
            order.ShippingDate = DateTime.Now.AddDays(5);

            foreach (var prodId in itemsToRemove.ProductIds)
            {
                var prod = await UnitOfWork.ProductRepository.GetAsync(prodId);
                if (prod is null)
                {
                    throw new NullReferenceException("product is null");
                }

                var orderItem = order.OrderItems.FirstOrDefault(oi => oi.ProductId == prodId);
                if (orderItem is null)
                {
                    throw new NullReferenceException("orderItem is null");
                }

                if (orderItem.Quantity == 1)
                {
                    order.OrderItems.Remove(orderItem);
                }
                else
                {
                    orderItem.Quantity--;
                }
            }
            await UnitOfWork.SaveAsync();
        }
    }
}
