using System.Collections.Generic;
using GMartDataLibrary.Models;
namespace GMartServiceLibrary.BulkOperationServices
{
    public interface IBulkProductService
    {
        List<int> AddProducts(List<Product> newProductsList);
        void DeleteProducts(List<int> deletedProductIDList);
    }
}