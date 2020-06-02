using GMartDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GMartDataLibrary.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product entity);
        void SaveDb();
    }
}
