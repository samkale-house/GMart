using System;
using System.Collections.Generic;
using System.Text;

namespace GMartDataLibrary.Models
{
    public class ProductType
    {
        public int ID { get; set; }
        public string Type_Name { get; set; }

        public List<Product> products { get; set; }
    }
}
