using Infrastructure.Dto.Reviews;
using Infrastructure.Dto.ServiceRequest;
using Infrastructure.Enums;
using Infrastructure.Models.Reviews;
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
        //TODO redo with ID
        Task<IResult> AssigneeServiceRequest(ServiceRequest serviceRequest, Guid employeeId);
        Task<IResult> ChangeStatus(Guid serviceRequest, ServiceRequestStatus status);
        Task<IResult> AddReview(Guid requestId, Review review);
        Task<IResultWithData<IList<ReviewDisplayDto>>> GetAllReviews();

        Task<IResultWithData<IList<ServiceRequestDto>>> GetUserRequests(UserModel user);
    }
}
