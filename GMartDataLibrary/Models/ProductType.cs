using System.Collections.Generic;

namespace GMartDataLibrary.Models
{
    public class ProductType
    {
        public int ID { get; set; }
        public string Type_Name { get; set; }

        //   [ForeignKey("Product_Type")]//name of forignkey from Product class
        public List<Product> products { get; set; }
    }
}
