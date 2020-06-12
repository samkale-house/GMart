using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using GMartDataLibrary.Models;
using LumenWorks.Framework.IO.Csv;


namespace GMartUtilityLibrary
{
    public static class CSVImportHelper
    {
        public static List<Product> ReadFromCsvToProductList(string CsvPath)
        {
            //get from csv to DataTable
            DataTable csvDt = ReadToCSVTable(CsvPath);

            List<Product> productsList = ReadFromTableToProductList(csvDt);

            return productsList;
        }
        public static DataTable ReadToCSVTable(string csvPath)
        {
            DataTable csvTable = new DataTable();
            //streaReader to give csvpath and ,true represents if the first line of csv is column heading
            using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(csvPath)), true))
            {
                csvTable.Load(csvReader);
            }
            return csvTable;
        }
         public static List<Product> ReadFromTableToProductList(DataTable csvTable)
        {
            List<Product> productListFromCSV = new List<Product>();
            for (int i = 0; i < csvTable.Rows.Count-1; i++)
            {
                productListFromCSV.Add(new Product()
                {
                    Product_Name = csvTable.Rows[i][0].ToString(),
                    Company = csvTable.Rows[i][1].ToString(),
                    Product_Price = Convert.ToDecimal(csvTable.Rows[i][2]),
                   // Product_Type = Convert.ToInt32(csvTable.Rows[i][3])    to do :fix, work with navigation property                 
                });
            }
            return productListFromCSV;
        }
    }
}