using AutoMapper;
using Infrastructure.Attributes;
using Infrastructure.Dto.ResetPassword;
using Infrastructure.Dto.User;
using Infrastructure.Models.ResetPassword;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;

namespace MotoGarage.Controllers
{
    [AllowAnonymous]
    [Route("api/Account")]
    public class AccountController : BaseController
    {
        private IAccountAuthService accountAuthService;

        public AccountController
            (IAccountManagerService accountManagerService,
            IAccountAuthService accountAuthService,
            IMapper mapper) : base(accountManagerService, mapper)
        {
            this.accountAuthService = accountAuthService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var loginResultByEmail = await accountAuthService.LoginByEmail(loginUserDto.Email, loginUserDto.Password);
            if (loginResultByEmail.IsSuccess)
            {
                return Json(loginResultByEmail.GetData);
            }

            return BadRequest(loginResultByEmail.GetErrorResponse);
        }

        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var model = _mapper.Map<ForgotPassword>(forgotPasswordDto);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";

            await _accountManagerService.SendResetPasswordMessage(model, baseUrl);

            return Ok();
        }

        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var model = _mapper.Map<ResetPassword>(resetPasswordDto);

            var result = await _accountManagerService.ResetPassword(model);

            if (!result.IsSuccess)
            {
                Response.StatusCode = result.GetErrorResponse.Status;
            }

            return Json(result);
        }

        [HttpPost("UpdatePassword")]
        [AuthorizeClient]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDto resetPasswordDto)
        {
            var model = _mapper.Map<ResetPassword>(resetPasswordDto);

            var result = await _accountManagerService.ResetPassword(model);

            return Json(result);
        }

        [Authorize]
        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await accountAuthService.Logout();
            return Ok();
        }
    }
}
