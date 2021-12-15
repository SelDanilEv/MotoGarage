using AutoMapper;
using Infrastructure.Dto.User;
using Infrastructure.ResponseModels;
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
