using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;

namespace Infrastructure.Models.Identity
{
    [CollectionName("ApplicationRole")]
    public class ApplicationRole : MongoIdentityRole<Guid>
    {

    }
}
