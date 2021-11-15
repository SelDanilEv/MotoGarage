using Infrastructure.Enums;
using Infrastructure.Models.CommonModels;
using Infrastructure.Models.Identity;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.ServiceRequest
{
    public class ServiceRequest : IBaseModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
        public ServiceRequestStatus Status { get; set; }

        public Guid ReporterId { get; set; }
        public Guid AssigneeId { get; set; }

        [BsonIgnore]
        public UserModel Reporter { get; set; }
        [BsonIgnore]
        public UserModel Assignee { get; set; }
    }
}
