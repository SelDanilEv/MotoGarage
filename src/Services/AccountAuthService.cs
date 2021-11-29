using Infrastructure.Result.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Infrastructure.Models.Identity;
using Infrastructure.Result;
using Services.Interfaces;
using Infrastructure.Models.CommonModels;
using Infrastructure.Enums;

namespace Services
{
    public class AccountAuthService : IAccountAuthService
    {
        private IAccountManagerService _accountManagerService;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountAuthService(IAccountManagerService accountManagerService, SignInManager<ApplicationUser> signInManager)
        {
            this._accountManagerService = accountManagerService;
            this._signInManager = signInManager;
        }

        public async Task<IResultWithData<CurrentUser>> LoginByEmail(string email, string password)
        {
            var result = Result<CurrentUser>.SuccessResult();

            var appUserResult = await _accountManagerService.GetUserByEmail(email);

            if (!appUserResult.IsSuccess)
            {
                return Result<CurrentUser>.ErrorResult(appUserResult);
            }

            var appUser = appUserResult.GetData;

            var signInWithPasswordResult = await _signInManager.PasswordSignInAsync(appUser, password, false, false);
            if (!signInWithPasswordResult.Succeeded)
            {
                var invalidPasswordMessage = ResultMessages.UserInvalidPassword;

                var singInErrorResult = Result<CurrentUser>.
                    ErrorResult()
                    .BuildMessage(invalidPasswordMessage)
                    .AddError(ValidationField.Password.ToString(), invalidPasswordMessage);
                return singInErrorResult;
            }

            var getCurrentUserResult = await _accountManagerService.GetCurrentUser(appUser);
            result.Data = getCurrentUserResult.IsSuccess ? getCurrentUserResult.GetData : null;

            return result;
        }

        public async Task<IResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Result.SuccessResult();
        }
    }
}
