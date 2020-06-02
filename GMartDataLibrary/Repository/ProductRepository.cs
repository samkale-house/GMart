﻿using GMartDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMartDataLibrary.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly GMartDbContext _gMartDbContext;
        public ProductRepository(GMartDbContext gMartDbContext):base(gMartDbContext)
        {
            _gMartDbContext = gMartDbContext;
        }
        public void Update(Product product)
        {
            var productFromdb = _gMartDbContext.Products.FirstOrDefault(p=>p.ID==product.ID);
            if(productFromdb != null)
            {
                productFromdb.Product_Name = product.Product_Name;
                productFromdb.Product_Price = product.Product_Price;
                _gMartDbContext.SaveChanges();
            }
        }
        public void SaveDb()
        {
            _gMartDbContext.SaveChanges();
        }


    }


}