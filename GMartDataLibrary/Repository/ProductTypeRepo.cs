using GMartDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace GMartDataLibrary.Repository
{
    public class ProductTypeRepo : Repository<ProductType>,IProductTypeRepo
    {
        private readonly GMartDbContext _gMartDbContext;
        public ProductTypeRepo(GMartDbContext gMartDbContext):base(gMartDbContext)
        {
            _gMartDbContext = gMartDbContext;
        }

        public void SaveDb()
        {
            _gMartDbContext.SaveChanges();
        }
    }
}
