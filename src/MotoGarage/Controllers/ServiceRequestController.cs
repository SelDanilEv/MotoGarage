using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;
using System.Threading.Tasks;
using Infrastructure.Models.ServiceRequests;
using Infrastructure.Attributes;
using Infrastructure.Enums;
using Infrastructure.Dto.ServiceRequest;
using AutoMapper;
using Infrastructure.Models.User;
using System;
using Infrastructure.Models.Reviews;

namespace MotoGarage.Controllers
{
    [Authorize]
    [Route("api/ServiceRequest")]
    public class ServiceRequestController : BaseController
    {
        private IServiceRequestService _serviceRequestService;

        public ServiceRequestController
            (IAccountManagerService accountManagerService,
            IServiceRequestService serviceRequestService,
            IMapper mapper) : base(accountManagerService, mapper)
        {
            _serviceRequestService = serviceRequestService;
        }

        [HttpGet]
        [AuthorizeClient]
        [Route("GetUserRequests")]
        public async Task<IActionResult> GetUserRequests()
        {
            var currentUser = _mapper.Map<UserModel>(CurrentUser);
            var result = await _serviceRequestService.GetUserRequests(currentUser);

            if (result == null || !result.IsSuccess)
            {
                Response.StatusCode = result.GetErrorResponse.Status;
                return Json(result?.Message ?? "Result is empty");
            }

            return Json(result);
        }

        [HttpGet]
        [AuthorizeAdmin]
        [Route("GetAllRequests")]
        public async Task<IActionResult> GetAllRequests()
        {
            var result = await _serviceRequestService.GetItems();

            if (result == null || !result.IsSuccess)
            {
                Response.StatusCode = result.GetErrorResponse.Status;
                return Json(result?.Message ?? "Result is empty");
            }

            return Json(result);
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
            var result = await _serviceRequestService.AddItem(newRequest);

            if (result == null || !result.IsSuccess)
            {
                Response.StatusCode = result.GetErrorResponse.Status;
                return Json(result?.Message ?? "Result is empty");
            }

            return Json(result);
        }

        [HttpPost]
        [AuthorizeClient]
        [Route("UpdateServiceRequest")]
        public async Task<IActionResult> UpdateServiceRequest(UpdateServiceRequestDto updateServiceRequest)
        {
            var newServiceRequest = _mapper.Map<ServiceRequest>(updateServiceRequest);

            var updateRequestResult = await _serviceRequestService.UpdateItem(newServiceRequest);

            if (!updateRequestResult.IsSuccess)
            {
                Response.StatusCode = updateRequestResult.GetErrorResponse.Status;
                return Json(updateRequestResult?.Message ?? "Change status failed");
            }

            return Json(updateRequestResult);
        }

        [HttpPost]
        [AuthorizeClient]
        [Route("AddRequestReview")]
        public async Task<IActionResult> AddRequestReview(AddRequestReviewDto addRequestReview)
        {
            var review = _mapper.Map<Review>(addRequestReview.Review);

            var updateRequestResult = await _serviceRequestService.AddReview(addRequestReview.Id, review);

            if (!updateRequestResult.IsSuccess)
            {
                Response.StatusCode = updateRequestResult.GetErrorResponse.Status;
                return Json(updateRequestResult?.Message ?? "Change status failed");
            }

            return Json(updateRequestResult);
        }

        [HttpPost]
        [AuthorizeEmployee]
        [Route("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeRequestStatusDto changeRequestStatusDto)
        {
            var updateRequestResult = await _serviceRequestService.ChangeStatus(changeRequestStatusDto.RequestId, changeRequestStatusDto.NewStatus);

            if (!updateRequestResult.IsSuccess)
            {
                Response.StatusCode = updateRequestResult.GetErrorResponse.Status;
                return Json(updateRequestResult?.Message ?? "Change status failed");
            }

            return Json(updateRequestResult);
        }

        [HttpPost]
        [AuthorizeAdmin]
        [Route("AssigneeServiceRequest")]
        public async Task<IActionResult> AssigneeServiceRequest(AssigneeServiceRequestDto assigneeServiceRequestDto)
        {
            var getRequestResult = await _serviceRequestService.GetItemById(assigneeServiceRequestDto.RequestId);

            if (!getRequestResult.IsSuccess)
            {
                Response.StatusCode = getRequestResult.GetErrorResponse.Status;
                return Json(getRequestResult?.Message ?? "Request is not found");
            }

            var updateRequestResult = await _serviceRequestService.AssigneeServiceRequest(getRequestResult.GetData, assigneeServiceRequestDto.UserId);

            if (!updateRequestResult.IsSuccess)
            {
                Response.StatusCode = updateRequestResult.GetErrorResponse.Status;
                return Json(updateRequestResult?.Message ?? "Change status failed");
            }

            return Json(updateRequestResult);
        }

        [HttpPost]
        [AuthorizeAdmin]
        [Route("RemoveServiceRequest")]
        public async Task<IActionResult> RemoveServiceRequest(Guid guid)
        {
            var result = await _serviceRequestService.RemoveItem(guid);

            if (result == null || !result.IsSuccess)
            {
                Response.StatusCode = result.GetErrorResponse.Status;
                return Json(result?.Message ?? "Result is empty");
            }

            return Json(result);
        }
    }
}
