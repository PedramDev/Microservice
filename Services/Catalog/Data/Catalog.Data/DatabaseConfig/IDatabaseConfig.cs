namespace Catalog.Data.DatabaseConfig
{
    internal interface IDatabaseConfig
    {
        string GetConnectionString();
        string GetDatabaseName();
        string GetCollectionName();
    }
}
