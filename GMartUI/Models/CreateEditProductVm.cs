using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMartUI.Models
{
    public class CreateEditProductVm
    {
        public int ID { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage ="Please enter Title")]
        public string Product_Name { get; set; }


        [Required(ErrorMessage = "Please enter Company")]
        public string Company { get; set; }

        [DisplayName("Price")]
        [Required(ErrorMessage = "Please enter Price for the Product")]
        public decimal Product_Price { get; set; }

        [Required(ErrorMessage = "Please enter Type")]
        public int Product_Type { get; set; }

        [Required(ErrorMessage = "Please select image file")]
        public IFormFile Product_Image { get; set; }
    }
}
