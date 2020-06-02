using GMartDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GMartServiceLibrary.ProductOperationServices
{
    public interface IProductService
    {
        IEnumerable<Product> getAllProducts();

    }
}
