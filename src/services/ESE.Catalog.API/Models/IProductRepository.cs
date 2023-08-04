using ESE.Core.Data;

namespace ESE.Catalog.API.Models
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(Guid id);

        void ToAdd(Product product);
        void Update(Product product);

    }
}
