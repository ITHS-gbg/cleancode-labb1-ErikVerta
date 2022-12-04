using Server.Interfaces;
using Shared;

namespace Server.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public ProductService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await UnitOfWork.ProductRepository.GetAllAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await UnitOfWork.ProductRepository.GetAsync(id);
        }

        public async Task CreateProduct(Product newProduct)
        {
            var products = await UnitOfWork.ProductRepository.GetAllAsync();
            var product = products.FirstOrDefault(p => p.Name == newProduct.Name);
            if (product != null)
            {
                throw new Exception("product already exists");
            }

            await UnitOfWork.ProductRepository.CreateAsync(newProduct);
            await UnitOfWork.SaveAsync();
        }
    }
}
