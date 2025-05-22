using System.ComponentModel.DataAnnotations;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace POWAPI.Models;

[CollectionName("users")]
public class User : MongoIdentityUser<Guid>
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}