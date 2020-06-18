namespace GMartDataLibrary.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GMartDbContext _gMartDbContext;


        public UnitOfWork(GMartDbContext gMartDbContext)//gmartdbcontext added for DI by gmartui startup configureservices
        {
            _gMartDbContext = gMartDbContext;
            productRepository = new ProductRepository(_gMartDbContext);
            productTypeRepo = new ProductTypeRepo(_gMartDbContext);
        }

        public IProductRepository productRepository { get; private set; }//repository object creation only in UOW class.Other dependant of UOW can only get object
        public IProductTypeRepo productTypeRepo { get; private set; }
        public void Save()
        {
            _gMartDbContext.SaveChanges();
        }
    }
}
