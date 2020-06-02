using GMartDataLibrary.Models;
using GMartDataLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using GMartDataLibrary;

namespace GMartServiceLibrary.ProductOperationServices
{
    public class ProductService : IProductService
    {
        private readonly GMartDbContext _gMartDbContext;
        ProductRepository prodRepo;
        public ProductService(GMartDbContext gMartDbContext)
        {
            _gMartDbContext = gMartDbContext;
            prodRepo = new ProductRepository(_gMartDbContext);
        }
        
        public IEnumerable<Product> getAllProducts()
        {
          return prodRepo.GetAll();
        }
    }
}
