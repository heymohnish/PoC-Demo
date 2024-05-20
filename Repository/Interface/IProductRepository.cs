using PoC_Demo.Model;

namespace PoC_Demo.Repository.Interface
{
    public interface IProductRepository
    {
        Task<bool> AddProductAsync(Product product);

        Task<bool> UpdateProductAsync(Product product);

        Task<List<Product>> GetProducts();

        Task<bool> ValidateUser(Login login);

        Task<bool> RemoveProduct(int id);

        Task<List<UserTask>> GetTask(DateTime? date);
    }
}
