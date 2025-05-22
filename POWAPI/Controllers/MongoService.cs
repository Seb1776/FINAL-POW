using MongoDB.Bson;
using MongoDB.Driver;
using POWAPI.Models;

namespace WebApplication1.Controllers;

public class MongoService
{
    public string ConnectionString => "mongodb+srv://sebastianwho1776:lakezurich123@powcluster.vgn3vfq.mongodb.net/?retryWrites=true&w=majority&appName=POWCluster";
    private readonly IMongoCollection<User> _userCollection;
    public IMongoCollection<User> UserCollection => _userCollection;

    public MongoService()
    {
        var settings = MongoClientSettings.FromConnectionString(ConnectionString);

        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        var client = new MongoClient(settings);

        try
        {
            var database = client.GetDatabase("pow");
            _userCollection = database.GetCollection<User>("users");
            
            var result = client.GetDatabase("pow").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
            
            Console.WriteLine("Pinged deployement");
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        /*_config = config;

        var connectionString = _config.GetConnectionString("DbConnection");
        var mongoURL = MongoUrl.Create(connectionString);
        var mongoClient = new MongoClient(mongoURL);
        _db = mongoClient.GetDatabase(mongoURL.DatabaseName);*/
    }

    //public IMongoDatabase? Database => _db;
}