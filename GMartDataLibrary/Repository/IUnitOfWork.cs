namespace GMartDataLibrary.Repository
{
    public interface IUnitOfWork
    {
        IProductRepository productRepository { get; }
        IProductTypeRepo productTypeRepo { get; }
        void Save();
    }
}
