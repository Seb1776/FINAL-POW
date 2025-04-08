using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace POWWEB.Models;

public class StudentViewModel
{
    [BsonId]
    [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [DisplayName("Student Name")]
    [BsonElement("student_name"), BsonRepresentation(BsonType.String)]
    public string? FirstName { get; set; }
    
    [BsonElement("student_lastname"), BsonRepresentation(BsonType.String)]
    public string? LastName { get; set; }
}