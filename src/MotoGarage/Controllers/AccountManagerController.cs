using AutoMapper;
using Infrastructure.Attributes;
using Infrastructure.Dto.User;
using Infrastructure.Models.CommonModels;
using Infrastructure.Models.Identity;
using Infrastructure.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotoGarage.Controllers
{
    [AuthorizeAdmin]
    [Route("api/AccountManager")]
    public class AccountManagerController : BaseController
    {
        private IApplicationUserService _applicationUserService;

        public AccountManagerController
            (IApplicationUserService applicationUserService ,
            IAccountManagerService accountManagerService,
            IMapper mapper) : base(accountManagerService, mapper)
        {
            this._applicationUserService = applicationUserService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var getAllResult = await _applicationUserService.GetItems();

            if (!getAllResult.IsSuccess)
            {
                Response.StatusCode = getAllResult.GetErrorResponse.Status;
                return Json(getAllResult.Message);
            }

            var userList = new List<Infrastructure.Models.User.UserModel>();

            foreach (var user in getAllResult.GetData)
            {
                var model = _mapper.Map<Infrastructure.Models.User.UserModel>(user);
                model.Role = (await _accountManagerService.GetRoleByEmail(model.Email)).GetData;

                userList.Add(model);
            }

            return Json(userList);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(LoginUserDto user)
        {
            var result = await _accountManagerService.CreateUser(user);

            if (!result.IsSuccess)
            {
                Response.StatusCode = result.GetErrorResponse.Status;
                return Json(result.GetErrorResponse);
            }

            return Json(result.Message);
        }

        [HttpPost]
        [AuthorizeAdmin]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserDto user)
        {
            var appUser = (await _applicationUserService.GetItemById(user.Id)).GetData;

            appUser.Name = user.Name;

            var result = await _applicationUserService.UpdateItem(appUser);

            if (!result.IsSuccess)
            {
                Response.StatusCode = result.GetErrorResponse.Status;
                return Json(result.GetErrorResponse);
            }

            await _accountManagerService.GrantRoleByEmail(user.Email, user.Role);

            return Json(result.Message);
        }

        [HttpPost]
        [AuthorizeAdmin]
        [Route("RemoveUser")]
        public async Task<IActionResult> RemoveUser(Guid id)
        {
            var result = await _applicationUserService.RemoveItem(id);

            if (!result.IsSuccess)
            {
                Response.StatusCode = result.GetErrorResponse.Status;
                return Json(result.GetErrorResponse);
            }

            return Json(result.Message);
        }

        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole(RoleDto role)
        {
            var result = await _accountManagerService.CreateRole(role.Name);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok("Role created Successfully");
        }

        [HttpPost]
        [Route("AddRole")]
        public async Task<IActionResult> AddRole(GrantRoleToUserDto grantDto)
        {
            var result = await _accountManagerService.GrantRoleById(grantDto.Id, grantDto.RoleName);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        [HttpPost]
        [Route("RemoveRole")]
        public async Task<IActionResult> RemoveRole(GrantRoleToUserDto revokeDto)
        {
            var result = await _accountManagerService.RemoveRolesById(revokeDto.Id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }
    }
}
