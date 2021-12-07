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
using Infrastructure.Models.CommonModels;
using Infrastructure.Models.ServiceRequests;
using System.Linq;
using Infrastructure.Models.User;

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

        public async Task<IResult> AssigneeServiceRequest(ServiceRequest serviceRequest, Guid employeeId)
        {
            var oldServiceRequest = await GetItemById(serviceRequest.Id);

            var newServiceRequest = oldServiceRequest.GetData;
            newServiceRequest.AssigneeId = employeeId;

            await UpdateItem(newServiceRequest);

            return Result.SuccessResult();
        }

        public async Task<IResult> ChangeStatus(ServiceRequest serviceRequest, ServiceRequestStatus status)
        {
            var oldServiceRequest = await GetItemById(serviceRequest.Id);

            var newServiceRequest = oldServiceRequest.GetData;
            newServiceRequest.Status = status;

            await UpdateItem(newServiceRequest);

            return Result.SuccessResult();
        }

        public async Task<T> SetApplicationUserFields<T>(object item)
        {
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


        public async Task<IResultWithData<IList<ServiceRequest>>> GetUserRequests(UserModel user)
        {
            var getAllResult = (Result<IList<ServiceRequest>>)await GetItems();

            if (!getAllResult.IsSuccess)
            {
                return getAllResult;
            }

            switch (user.Role)
            {
                case Roles.Client:
                    getAllResult.Data = getAllResult.GetData.Where(x => x.ReporterId == user.Id).ToList();
                    break;
                case Roles.Employee:
                    getAllResult.Data = getAllResult.GetData.Where(x => x.AssigneeId == user.Id).ToList();
                    break;
                case Roles.Admin:
                    break;
            }

            return getAllResult;
        }
    }
}
