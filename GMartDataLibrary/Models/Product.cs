using System.ComponentModel;

namespace GMartDataLibrary.Models
{
     public class Product
    {
        
        public int ID{get;set;}        

        [DisplayName("Title")]
        public string Product_Name{get;set;}        
        public string Company{get;set;}
        
        [DisplayName("Price")]
        public decimal Product_Price{get;set;}
        public int Product_Type{get;set;}

           
    }
}