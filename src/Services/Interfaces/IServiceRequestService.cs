using Infrastructure.Enums;
using Infrastructure.Models.ServiceRequests;
using Infrastructure.Models.User;
using Infrastructure.Result.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IServiceRequestService : ICommonRepositoryService<ServiceRequest>
    {
        Task<IResult> AssigneeServiceRequest(ServiceRequest serviceRequest, Guid employeeId);
        Task<IResult> ChangeStatus(ServiceRequest serviceRequest, ServiceRequestStatus status);

        Task<IResultWithData<IList<ServiceRequest>>> GetUserRequests(UserModel user);
    }
}
