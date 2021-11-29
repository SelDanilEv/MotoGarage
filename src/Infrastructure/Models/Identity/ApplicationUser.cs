using AspNetCore.Identity.MongoDbCore.Models;
using Infrastructure.Models.CommonModels;
using MongoDbGenericRepository.Attributes;
using System;

namespace Infrastructure.Models.Identity
{
    [CollectionName("ApplicationUser")]
    public class ApplicationUser : MongoIdentityUser<Guid>, IBaseModel
    {
        public string Name { get; set; }
    }
}
