using GMartDataLibrary.Models;

namespace GMartDataLibrary.Repository
{
    public interface IProductTypeRepo : IRepository<ProductType>
    {
        void SaveDb();
    }
}