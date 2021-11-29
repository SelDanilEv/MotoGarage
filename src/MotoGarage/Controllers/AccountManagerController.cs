using AutoMapper;
using Infrastructure.Attributes;
using Infrastructure.Dto.User;
using Infrastructure.Models.CommonModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotoGarage.Controllers
{
    [AuthorizeAdmin]
    [Route("AccountManager")]
    public class AccountManagerController : BaseController
    {
        private IApplicationUserService applicationUserService;

        public AccountManagerController
            (IApplicationUserService applicationUserService ,
            IAccountManagerService accountManagerService,
            IMapper mapper) : base(accountManagerService, mapper)
        {
            this.applicationUserService = applicationUserService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var getAllResult = await applicationUserService.GetItems();

            if (!getAllResult.IsSuccess)
            {
                return BadRequest(getAllResult.Message);
            }

            List<UserModel> userList = new List<UserModel>();

            foreach (var user in getAllResult.GetData)
            {
                userList.Add(_mapper.Map<UserModel>(user));
            }

            return Ok(userList);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(UserDto user)
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
