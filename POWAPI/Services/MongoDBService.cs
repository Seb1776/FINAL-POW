using System;
using POWAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace POWAPI.Services;

public class MongoDBService
{
    private readonly IMongoCollection<Student> _userCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        var client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        var databaes = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _userCollection = databaes.GetCollection<Student>("students");
    }

    public async Task CreateStudentAsync(Student student)
    {
        await _userCollection.InsertOneAsync(student);
    }

    public async Task<List<Student>> GetStudentsAsync()
    {
        return await _userCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateStudentAsync(string id, Student student)
    {
        var filter = Builders<Student>.Filter.Eq("Id", id);
        var update = Builders<Student>.Update
            .Set("username", student.username)
            .Set("password", student.password)
            .Set("studentName", student.studentName)
            .Set("studentLastName", student.studentLastName);

        await _userCollection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteStudentAsync(string id)
    {
        var filter = Builders<Student>.Filter.Eq("Id", id);
        await _userCollection.DeleteOneAsync(filter);
    }
}