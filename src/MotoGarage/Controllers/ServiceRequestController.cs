﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;
using System.Threading.Tasks;
using Infrastructure.Models.ServiceRequests;
using Infrastructure.Attributes;
using Infrastructure.Enums;
using Infrastructure.Dto.ServiceRequest;
using AutoMapper;
using Infrastructure.Models.User;

namespace MotoGarage.Controllers
{
    [Authorize]
    [Route("api/ServiceRequest")]
    public class ServiceRequestController : BaseController
    {
        private IServiceRequestService serviceRequestService;

        public ServiceRequestController
            (IAccountManagerService accountManagerService,
            IServiceRequestService serviceRequestService,
            IMapper mapper) : base(accountManagerService, mapper)
        {
            this.serviceRequestService = serviceRequestService;
        }

        [HttpGet]
        [AuthorizeAdmin]
        [Route("GetUserRequests")]
        public async Task<IActionResult> GetUserRequests()
        {
            var currentUser = _mapper.Map<UserModel>(CurrentUser);
            var result = await serviceRequestService.GetUserRequests(currentUser);

            if (result == null || !result.IsSuccess)
            {
                Response.StatusCode = result.GetErrorResponse.Status;
                return BadRequest(result?.Message ?? "Result is empty");
            }

            return Json(result);
        }

        [HttpGet]
        [AuthorizeAdmin]
        [Route("GetAllRequests")]
        public async Task<IActionResult> GetAllRequests()
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
        [Route("CreateServiceRequest")]
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
        [Route("ChangeStatus")]
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
        [Route("AssigneeServiceRequest")]
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
