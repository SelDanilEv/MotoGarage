using Infrastructure.Result.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Infrastructure.Models.Identity;
using Infrastructure.Result;
using Services.Interfaces;
using Infrastructure.Models.ServiceRequest;
using Microsoft.Extensions.Options;
using Infrastructure.Options;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Infrastructure.Enums;

namespace Services
{
    public class ServiceRequestService : CommonRepositoryService<ServiceRequest>, IServiceRequestService, IApplicationUserForeignKey
    {
        private IApplicationUserService _applicationUserService;

        public ServiceRequestService(IOptions<MongoDbOption> mongoOption , IApplicationUserService applicationUserService) : base(mongoOption)
        {
            this._applicationUserService = applicationUserService;
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

            if (reporterResult.IsSuccess)
            {
                result.Reporter = reporterResult.GetData?.ToModel();
            }

            if (assigneeResult.IsSuccess)
            {
                result.Assignee = assigneeResult.GetData?.ToModel();
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
    }
}
