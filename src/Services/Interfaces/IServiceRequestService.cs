using Infrastructure.Enums;
using Infrastructure.Models.Identity;
using Infrastructure.Models.ServiceRequests;
using Infrastructure.Result.Interfaces;
using System;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IServiceRequestService : ICommonRepositoryService<ServiceRequest>
    {
        Task<IResult> AssigneeServiceRequest(ServiceRequest serviceRequest, Guid employeeId);
        Task<IResult> ChangeStatus(ServiceRequest serviceRequest, ServiceRequestStatus status);
    }
}
