using MongoDB.Driver;
using SharedKernel.Models;

namespace Catalog.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
