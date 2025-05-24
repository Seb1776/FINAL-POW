using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;

namespace POWAPI.Models;

[Collection("students")]
public class Student
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    
    [Display(Name = "Username")]
    public string username { get; set; } = null!;
    [Display(Name = "Username")]
    public string password { get; set; } = null!;
    [Display(Name = "Username")]
    public string studentName { get; set; } = null!;
    [Display(Name = "Username")]
    public string studentLastName { get; set; } = null!;
}