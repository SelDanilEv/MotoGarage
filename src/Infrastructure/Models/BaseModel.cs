using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Infrastructure.Models
{
    public abstract class BaseModel : IBaseModel
    {
        public Guid Id { get; set; }
    }
}
