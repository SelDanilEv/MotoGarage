using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;
using System.Threading.Tasks;
using Infrastructure.Models.ServiceRequest;
using Infrastructure.Attributes;
using Infrastructure.Enums;
using Infrastructure.Dto.ServiceRequest;
using AutoMapper;

namespace MotoGarage.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class ServiceRequestController : BaseController
    {
        private IServiceRequestService serviceRequestService;

        public ServiceRequestController
            (IAccountManagerService accountManagerService,
            IServiceRequestService serviceRequestService,
            IMapper mapper) : base(accountManagerService,mapper)
        {
            this.serviceRequestService = serviceRequestService;
        }

        [HttpGet]
        [AuthorizeAdmin]
        public async Task<IActionResult> GetAllRequests(CreateServiceRequestDto createServiceRequestDto)
        {
            var result = await serviceRequestService.GetItems();

            if (result == null || !result.IsSuccess)
            {
                return BadRequest(result?.Message ?? "Result is empty");
            }

            return Ok(result);
        }

        [HttpPost]
        [AuthorizeClient]
        public async Task<IActionResult> CreateServiceRequest(CreateServiceRequestDto createServiceRequestDto)
        {
            var newRequest = new ServiceRequest()
            {
                Title = createServiceRequestDto.Title,
                Message = createServiceRequestDto.Message,
                Status = ServiceRequestStatus.Triage,
                ReporterId = CurrentUser.Id
            };
            var result = await serviceRequestService.AddItem(newRequest);

            if (result == null || !result.IsSuccess)
            {
                return BadRequest(result?.Message ?? "Result is empty");
            }

            return Ok(result);
        }

        [HttpPost]
        [AuthorizeEmployee]
        public async Task<IActionResult> ChangeStatus(ChangeRequestStatusDto changeRequestStatusDto)
        {
            var getRequestResult = await serviceRequestService.GetItemById(changeRequestStatusDto.RequestId);

            if (!getRequestResult.IsSuccess)
            {
                return BadRequest(getRequestResult?.Message ?? "Request is not found");
            }

            var updateRequestResult = await serviceRequestService.ChangeStatus(getRequestResult.GetData, changeRequestStatusDto.NewStatus);

            if (!updateRequestResult.IsSuccess)
            {
                return BadRequest(updateRequestResult?.Message ?? "Change status failed");
            }

            return Ok(updateRequestResult);
        }

        [HttpPost]
        [AuthorizeAdmin]
        public async Task<IActionResult> AssigneeServiceRequest(AssigneeServiceRequestDto assigneeServiceRequestDto)
        {
            var getRequestResult = await serviceRequestService.GetItemById(assigneeServiceRequestDto.RequestId);

            if (!getRequestResult.IsSuccess)
            {
                return BadRequest(getRequestResult?.Message ?? "Request is not found");
            }

            var updateRequestResult = await serviceRequestService.AssigneeServiceRequest(getRequestResult.GetData, assigneeServiceRequestDto.UserId);

            if (!updateRequestResult.IsSuccess)
            {
                return BadRequest(updateRequestResult?.Message ?? "Change status failed");
            }

            return Ok(updateRequestResult);
        }
    }
}
