using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace POWAPI.Models;

[CollectionName("roles")]
public class Role : MongoIdentityRole<Guid>
{
    
}