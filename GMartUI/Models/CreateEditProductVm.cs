using GMartDataLibrary.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GMartUI.Models
{
    public class CreateEditProductVm
    {
        [ReadOnly(true)]
        public int ID { get; set; }

        [DisplayName("Title")]
        [StringLength(50,MinimumLength =5,ErrorMessage ="The Product name must contain at least 5 letters")]
        [Required(ErrorMessage ="Please enter Title")]
        public string Product_Name { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "The Company name must contain at least 2 letters")]
        [Required(ErrorMessage = "Please enter Company")]
        public string Company { get; set; }

        [DisplayName("Price")]
        [Range(0.24,7000,ErrorMessage ="Price must be greater than 0.24")]
        [Required(ErrorMessage = "Please enter Price for the Product")]
        public decimal Product_Price { get; set; }

        [DisplayName("Type")]
        [Required(ErrorMessage = "Please select Type")]
        public int Product_Type { get; set; }

        [DisplayName("Image")]
        [Required(ErrorMessage = "Please select image file")]
        public IFormFile Product_Image { get; set; }

        public List<ProductType> Types { get; set; }
        public string ImageFullPath { get; set; }


        public CreateEditProductVm()
        { 
            Types = new List<ProductType>(); 
        }
    }
}
