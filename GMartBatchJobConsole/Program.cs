using System;
using System.Collections.Generic;
using GMartUtilityLibrary;
using GMartDataLibrary.Models;
using GMartDataLibrary;
using GMartServiceLibrary.BulkOperationServices;

namespace GMartBatchJobConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            AddInBulk();
        }
        
        public static void AddInBulk()
        {
            //Read from file
            //save in list.
            Console.WriteLine("Enter CSV Path");
            string csvPath= @"C:\Users\SamGrishma\source\repos\GMart\ProductsList.csv";//Console.ReadLine();
            List<Product> productList=CSVImportHelper.ReadFromCsvToProductList(csvPath);
            
            //call service to add
            IBulkProductService bulkProductService=new BulkProductService();
            bulkProductService.AddProducts(productList);
        }
    }
}
