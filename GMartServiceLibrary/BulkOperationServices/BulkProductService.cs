using System.Collections.Generic;
using GMartDataLibrary;
using System;
using GMartDataLibrary.Models;


namespace GMartServiceLibrary.BulkOperationServices
{
    public class BulkProductService : IBulkProductService
    {
        GMartDbContext dbContext = new GMartDbContext();
        public List<int> AddProducts(List<Product> newProductsList)
        {
            List<int> newProductsIDList = new List<int>();
            try
            {
                foreach (Product product in newProductsList)
                {
                    dbContext.Products.Add(product);
                    newProductsIDList.Add(product.ID);
                }
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                dbContext = null;
                Console.WriteLine("Type:" + e.GetType().ToString() + " Message:" + e.Message);
            }
            return newProductsIDList;
        }

        public void DeleteProducts(List<int> deletedProductIDList)
        {
            throw new NotImplementedException();
        }
    }
}