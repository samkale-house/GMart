using GMartDataLibrary.Models;

namespace GMartDataLibrary.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product entity);
        void SaveDb();
    }
}
