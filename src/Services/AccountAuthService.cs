using Infrastructure.Result.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Infrastructure.Models.Identity;
using Infrastructure.Result;
using Services.Interfaces;

namespace Services
{
    public class AccountAuthService : IAccountAuthService
    {
        private IAccountManagerService accountManagerService;
        private SignInManager<ApplicationUser> signInManager;

        public AccountAuthService(IAccountManagerService accountManagerService, SignInManager<ApplicationUser> signInManager)
        {
            this.accountManagerService = accountManagerService;
            this.signInManager = signInManager;
        }

        public async Task<IResult> LoginByLoginOrEmail(string emailOrLogin, string password)
        {
            var result = Result.SuccessResult();
            var errorResult = Result.ErrorResult();

            var appUserResult = await accountManagerService.GetUserByEmail(emailOrLogin);

            if (!appUserResult.IsSuccess)
            {
                errorResult.BuildMessage(appUserResult.Message);

                appUserResult = await accountManagerService.GetUserByLogin(emailOrLogin);
                if (!appUserResult.IsSuccess)
                {
                    return errorResult.AppendMessage(appUserResult.Message);
                }
            }

            var appUser = appUserResult.GetData;

            var signInWithPasswordResult = await signInManager.PasswordSignInAsync(appUser, password, false, false);
            if (!signInWithPasswordResult.Succeeded)
            {
                return Result.ErrorResult().BuildMessage("Invalid password");
            }

            return result;
        }

        public async Task<IResult> Logout()
        {
            await signInManager.SignOutAsync();

            return Result.SuccessResult();
        }
    }
}
