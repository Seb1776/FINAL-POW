using MongoDB.Driver;

namespace POWAPI.Controllers;

public class MongoService
{
    private readonly IConfiguration _config;
    private readonly IMongoDatabase? _db;

    public MongoService(IConfiguration config)
    {
        _config = config;

        var connectionString = _config.GetConnectionString("DbConnection");
        var mongoURL = MongoUrl.Create(connectionString);
        var mongoClient = new MongoClient(mongoURL);
        _db = mongoClient.GetDatabase(mongoURL.DatabaseName);
    }

    public IMongoDatabase? Database => _db;
}