using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace POWWEB.Models;

public class Student
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId? Id { get; set; }
    
    [Display(Name = "Username")]
    public string username { get; set; } = null!;
    [Display(Name = "Username")]
    public string password { get; set; } = null!;
    [Display(Name = "Username")]
    public string studentName { get; set; } = null!;
    [Display(Name = "Username")]
    public string studentLastName { get; set; } = null!;
}