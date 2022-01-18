using Microsoft.Extensions.Configuration;

namespace Catalog.Data.DatabaseConfig
{
    internal class GetDatabaseConnection : IDatabaseConfig
    {
        private readonly IConfiguration _configuration;
        public GetDatabaseConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            return _configuration.GetSection("DatabaseSettings:ConnectionString").Value;
        }
        public string GetDatabaseName()
        {
            return _configuration.GetSection("DatabaseSettings:DatabaseName").Value;
        }
        public string GetCollectionName()
        {
            return _configuration.GetSection("DatabaseSettings:CollectionName").Value;
        }
    }
}
