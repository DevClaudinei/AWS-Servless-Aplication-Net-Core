using MongoDB.Driver;
using MongoDB.Infrastructure;

namespace Infraestructure.Data.Context;

public class ApplicationDbContext : MongoDbContext
{
    public ApplicationDbContext(string connectionString, string databaseName, MongoDatabaseSettings databaseSettings = null)
                : base(connectionString, databaseName, databaseSettings)
    {
        AcceptAllChangesOnSave = true;
        ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}