using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Infrastructure.Models
{
    public interface IBaseModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }
    }
}
