using Catalog.Data.DatabaseConfig;
using MongoDB.Driver;
using SharedKernel.Models;

namespace Catalog.Data
{
    internal class CatalogContext : ICatalogContext
    {
        public CatalogContext(IDatabaseConfig dbConfig)
        {
            var client = new MongoClient(dbConfig.GetConnectionString());
            var database = client.GetDatabase(dbConfig.GetDatabaseName());
            Products = database.GetCollection<Product>(dbConfig.GetCollectionName());

            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
