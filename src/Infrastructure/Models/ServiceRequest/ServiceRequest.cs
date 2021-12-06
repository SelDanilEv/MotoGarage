using Infrastructure.Enums;
using Infrastructure.Models.CommonModels;
using Infrastructure.Models.Reviews;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Infrastructure.Models.ServiceRequests
{
    public class ServiceRequest : IBaseModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
        public ServiceRequestStatus Status { get; set; }
        public Review Review { get; set; }

        public Guid ReporterId { get; set; }
        public Guid AssigneeId { get; set; }

        [BsonIgnore]
        public UserModel Reporter { get; set; }
        [BsonIgnore]
        public UserModel Assignee { get; set; }
    }
}
