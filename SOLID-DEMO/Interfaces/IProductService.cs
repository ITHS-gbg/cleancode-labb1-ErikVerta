using Shared;

namespace Server.Interfaces;

public interface IProductService : IService
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProduct(int id);
    Task CreateProduct(Product newProduct);
}