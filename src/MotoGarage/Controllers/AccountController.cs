using AutoMapper;
using Infrastructure.Dto.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;

namespace MotoGarage.Controllers
{
    [AllowAnonymous]
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
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            if (ModelState.IsValid)
            {
                var loginResultByEmail = await accountAuthService.LoginByLoginOrEmail(loginUserDto.LoginOrEmail, loginUserDto.Password);
                if (loginResultByEmail.IsSuccess)
                {
                    return Redirect("/");
                }

                return BadRequest(loginResultByEmail.Message);
            }

            return BadRequest("");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await accountAuthService.Logout();
            return Ok();
        }
    }
}
