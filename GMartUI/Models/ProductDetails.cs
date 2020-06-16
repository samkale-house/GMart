using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMartUI.Models
{
    public class ProductDetailsVM
    {
        public int Id { get; set; }

        [DisplayName("Product")]
        public string Product_Name { get; set; }
        public string Company { get; set; }        

        [DisplayName("Price")]
        public decimal Product_Price { get; set; }

        public string Product_ImageUrl { get; set; }

        [DisplayName("Type")]
        public string Product_Type { get; set; }
        public string ImageFullPath { get; set; }


    }
}
