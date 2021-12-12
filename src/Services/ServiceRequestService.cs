using Infrastructure.Result.Interfaces;
using System.Threading.Tasks;
using Infrastructure.Result;
using Services.Interfaces;
using Microsoft.Extensions.Options;
using Infrastructure.Options;
using System;
using System.Collections.Generic;
using Infrastructure.Enums;
using AutoMapper;
using Infrastructure.Models.ServiceRequests;
using System.Linq;
using Infrastructure.Models.User;
using Infrastructure.Dto.ServiceRequest;
using Infrastructure.Models.Reviews;
using Infrastructure.Dto.Reviews;

namespace Services
{
    public class ServiceRequestService : CommonRepositoryService<ServiceRequest>, IServiceRequestService, IApplicationUserForeignKey
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IMapper _mapper;

        public ServiceRequestService(IOptions<MongoDbOption> mongoOption,
            IApplicationUserService applicationUserService,
            IMapper mapper) : base(mongoOption)
        {
            _applicationUserService = applicationUserService;
            _mapper = mapper;
        }

        public async Task<IResultWithData<IList<ServiceRequestDto>>> GetUserRequests(UserModel user)
        {
            var getAllResult = await GetItems();

            if (!getAllResult.IsSuccess)
            {
                return Result<IList<ServiceRequestDto>>.ErrorResult(getAllResult);
            }

            var data = _mapper.Map<IList<ServiceRequestDto>>(getAllResult.GetData);
            var result = Result<IList<ServiceRequestDto>>.SuccessResult(data);

            switch (user.Role)
            {
                case Roles.Client:
                    result.Data = result.GetData.Where(x => x.Reporter?.Id == user.Id).ToList();
                    break;
                case Roles.Employee:
                    result.Data = result.GetData.Where(x => x.Assignee?.Id == user.Id).ToList();
                    break;
                case Roles.Admin:
                    break;
            }

            return result;
        }

        public async Task<IResult> AssigneeServiceRequest(ServiceRequest serviceRequest, Guid employeeId)
        {
            var oldServiceRequest = await GetItemById(serviceRequest.Id);

            var newServiceRequest = oldServiceRequest.GetData;
            newServiceRequest.AssigneeId = employeeId;

            await UpdateItem(newServiceRequest);

            return Result.SuccessResult();
        }

        public async Task<IResult> ChangeStatus(Guid requestId, ServiceRequestStatus status)
        {
            var oldServiceRequest = await GetItemById(requestId);

            var newServiceRequest = oldServiceRequest.GetData;
            newServiceRequest.Status = status;

            var result = await UpdateItem(newServiceRequest);

            return result;
        }

        public async Task<IResult> AddReview(Guid requestId, Review review)
        {
            var oldServiceRequest = await GetItemById(requestId);

            var newServiceRequest = oldServiceRequest.GetData;
            newServiceRequest.Review = review;

            var result = await UpdateItem(newServiceRequest);

            return result;
        }

        public async Task<IResultWithData<IList<ReviewDisplayDto>>> GetAllReviews()
        {
            var serviseRequestsResult = await this.GetItems();

            if (!serviseRequestsResult.IsSuccess)
            {
                return Result<IList<ReviewDisplayDto>>.ErrorResult();
            }

            var filteredRequests = serviseRequestsResult.GetData.Where(x => x.Review != null).ToList();

            var reviews = _mapper.Map<IList<ReviewDisplayDto>>(filteredRequests);

            return Result<IList<ReviewDisplayDto>>.SuccessResult(reviews);
        }

        public override async Task<IResultWithData<ServiceRequest>> GetItemById(Guid id)
        {
            var result = await base.GetItemById(id);
            if (result.IsSuccess)
            {
                var item = result.GetData;
                await SetApplicationUserFields<ServiceRequest>(item);
            }
            return result;
        }

        public override async Task<IResultWithData<IList<ServiceRequest>>> GetItems()
        {
            var result = (Result<IList<ServiceRequest>>)await base.GetItems();

            if (result.IsSuccess)
            {
                for (int i = 0; i < result.GetData.Count; i++)
                {
                    var item = result.GetData[i];
                    await SetApplicationUserFields<ServiceRequest>(item);
                }
            }

            return result;
        }

        public async Task<T> SetApplicationUserFields<T>(object item)
        {
            if (item == null)
            {
                return (T)item;
            }

            var result = (ServiceRequest)item;

            var reporterResult = await _applicationUserService.GetItemById(result.ReporterId);
            var assigneeResult = await _applicationUserService.GetItemById(result.AssigneeId);

            if (reporterResult.IsSuccess && reporterResult.GetData != null)
            {
                result.Reporter = _mapper.Map<UserModel>(reporterResult.GetData);
            }

            if (assigneeResult.IsSuccess && assigneeResult.GetData != null)
            {
                result.Assignee = _mapper.Map<UserModel>(assigneeResult.GetData);
            }

            return (T)item;
        }
    }
}
