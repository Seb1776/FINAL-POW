using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace POWAPI.Models;

public class Student
{
    [BsonId]
    [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("student_name"), BsonRepresentation(BsonType.String)]
    public string? FirstName { get; set; }
    
    [BsonElement("student_lastname"), BsonRepresentation(BsonType.String)]
    public string? LastName { get; set; }
}