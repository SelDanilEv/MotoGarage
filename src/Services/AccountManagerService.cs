using System;
using Infrastructure.Result.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Infrastructure.Models.Identity;
using Infrastructure.Result;
using Services.Interfaces;
using System.Security.Claims;
using Infrastructure.Dto.User;
using Infrastructure.Enums;
using Infrastructure.Models.CommonModels;
using AutoMapper;
using Infrastructure.Models.ResetPassword;
using Infrastructure.Models.User;

namespace Services
{
    public class AccountManagerService : IAccountManagerService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AccountManagerService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IApplicationUserService applicationUserService,
            IEmailService emailService,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _applicationUserService = applicationUserService;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<IResultWithData<ApplicationUser>> GetUserByEmail(string email)
        {
            var result = Result<ApplicationUser>.SuccessResult();

            var getUserResult = await _userManager.FindByEmailAsync(email);

            if (getUserResult == null)
            {
                var errorMessage = ResultMessages.UserNotFoundByEmail;
                var errorResult = Result<ApplicationUser>.ErrorResult().
                    BuildMessage(errorMessage).
                    AddError(ValidationField.Email.ToString(), errorMessage);

                return errorResult;
            }

            result.Data = getUserResult;

            return result;
        }

        public async Task<IResultWithData<ApplicationUser>> GetApplicationUser(ClaimsPrincipal User)
        {
            var result = await _userManager.GetUserAsync(User);

            if (result == null)
            {
                return Result<ApplicationUser>.ErrorResult().BuildMessage("User not authorized");
            }

            return Result<ApplicationUser>.SuccessResult(result);
        }

        public async Task<IResultWithData<CurrentUser>> GetCurrentUser(ApplicationUser user)
        {
            CurrentUser appUser = null;

            if (user != null)
            {
                appUser = _mapper.Map<CurrentUser>(user);

                var getRoleResult = await GetRoleById(appUser.Id.ToString());

                if (getRoleResult.IsSuccess)
                {
                    appUser.Role = getRoleResult.GetData;
                }
            }
            else
            {
                return Result<CurrentUser>.ErrorResult().BuildMessage("User not authorized");
            }

            return Result<CurrentUser>.SuccessResult(appUser);
        }

        public async Task<IResult> CreateUser(CreateUserDto user)
        {
            var result = Result.SuccessResult();

            var appUser = _mapper.Map<ApplicationUser>(user);

            var createResult = await _userManager.CreateAsync(appUser, user.Password);

            if (!createResult.Succeeded)
            {
                var errorResult = Result.ErrorResult()
                    .BuildMessage("Create user operation is failed\n");

                foreach (var error in createResult.Errors)
                {
                    errorResult.AppendMessage(error.Description + "\n");

                    string errorField = IdentityErrorCode.GetField(error.Code);
                    if (!string.IsNullOrWhiteSpace(errorField))
                    {
                        errorResult.AddError(errorField, error.Description);
                    }
                }

                return errorResult;
            }

            await GrantRoleByEmail(user.Email, "Client");

            result.BuildMessage("User created Successfully");

            return result;
        }

        public async Task<IResult> UpdateUserInfo(UpdateUserInfo updateUserInfo)
        {
            var result = Result.SuccessResult();

            var appUser = (await GetUserByEmail(updateUserInfo.Email)).GetData;

            if (!string.IsNullOrWhiteSpace(updateUserInfo.Name))
            {
                appUser.Name = updateUserInfo.Name;

                var updateNameResult = await _applicationUserService.UpdateItem(appUser);

                if (!updateNameResult.IsSuccess)
                {
                    result.Status = ResultStatus.Warning;
                    result.AddError("Name", "Name has not been changed");
                }
            }

            if (!string.IsNullOrWhiteSpace(updateUserInfo.OldPassword) && !string.IsNullOrWhiteSpace(updateUserInfo.NewPassword))
            {
                var updatePasswordModel = _mapper.Map<UpdatePassword>(updateUserInfo);
                updatePasswordModel.User = appUser;

                var updateNameResult = await UpdatePassword(updatePasswordModel);

                if (!updateNameResult.IsSuccess)
                {
                    result.Status = ResultStatus.Error;
                    result.AddError("Password", "Password has not been changed");
                }
            }

            return result;
        }

        public async Task<IResultWithData<string>> GetRoleByEmail(string email)
        {
            var result = Result<string>.SuccessResult();

            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles != null)
                {
                    if (roles.Count > 0)
                    {
                        result.Data = roles[0];
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(result.GetData))
            {
                return Result.ErrorResult();
            }

            return result;
        }

        public async Task<IResultWithData<string>> GetRoleById(string id)
        {
            var result = Result<string>.SuccessResult();

            ApplicationUser user = null;

            if (Guid.TryParse(id, out var guid))
            {
                user = await _userManager.FindByIdAsync(id);
            }

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles != null)
                {
                    if (roles.Count > 0)
                    {
                        result.Data = roles[0];
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(result.GetData))
            {
                return Result.ErrorResult();
            }

            return result;
        }

        public async Task<IResult> CreateRole(string name)
        {
            var result = Result.SuccessResult();

            IdentityResult createResult = await _roleManager.CreateAsync(new ApplicationRole() { Name = name });
            if (createResult.Succeeded)
                result.BuildMessage("Role created");
            else
            {
                var errorResult = Result.ErrorResult().BuildMessage("Create role operation is failed\n");

                foreach (var error in createResult.Errors)
                {
                    errorResult.AppendMessage(error.Description + "\n");
                }

                return errorResult;
            }

            return result;
        }

        public async Task<IResult> GrantRoleByEmail(string email, string roleName)
        {
            var result = Result.SuccessResult();

            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, roles);

                    var grantResult = await _userManager.AddToRoleAsync(user, roleName);
                    if (grantResult.Succeeded)
                    {
                        result.BuildMessage("Role granted");
                    }
                    else
                    {
                        var errorResult = Result.ErrorResult().BuildMessage("Grante role operation is failed\n");

                        foreach (var error in grantResult.Errors)
                        {
                            errorResult.AppendMessage(error.Description + "\n");
                        }

                        return errorResult;
                    }
                }
            }
            catch (Exception ex) { }

            return result;
        }

        public async Task<IResult> GrantRoleById(string id, string roleName)
        {
            var result = Result.SuccessResult();

            ApplicationUser user = null;

            if (Guid.TryParse(id, out var guid))
            {
                user = await _userManager.FindByIdAsync(id);
            }

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);

                var grantResult = await _userManager.AddToRoleAsync(user, roleName);
                if (grantResult.Succeeded)
                {
                    result.BuildMessage("Role granted");
                }
                else
                {
                    var errorResult = Result.ErrorResult().BuildMessage("Grante role operation is failed\n");

                    foreach (var error in grantResult.Errors)
                    {
                        errorResult.AppendMessage(error.Description + "\n");
                    }

                    return errorResult;
                }
            }

            return result;
        }

        public async Task<IResult> RemoveRolesByEmail(string email)
        {
            var result = Result.SuccessResult();

            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var removeResult = await _userManager.RemoveFromRolesAsync(user, roles);

                if (removeResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Client);

                    result.BuildMessage("Role removed");
                }
                else
                {
                    var errorResult = Result.ErrorResult().BuildMessage("Remove role operation is failed\n");

                    foreach (var error in removeResult.Errors)
                    {
                        errorResult.AppendMessage(error.Description + "\n");
                    }

                    return errorResult;
                }
            }

            return result;
        }

        public async Task<IResult> RemoveRolesById(string id)
        {
            var result = Result.SuccessResult();

            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var removeResult = await _userManager.RemoveFromRolesAsync(user, roles);

                if (removeResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Client);

                    result.BuildMessage("Role removed");
                }
                else
                {
                    var errorResult = Result.ErrorResult().BuildMessage("Remove role operation is failed\n");

                    foreach (var error in removeResult.Errors)
                    {
                        errorResult.AppendMessage(error.Description + "\n");
                    }

                    return errorResult;
                }
            }

            return result;
        }

        public async Task<IResult> SendResetPasswordMessage(ForgotPassword model, string baseUrl)
        {
            var result = Result.SuccessResult();

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return result;
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = $"{baseUrl}Account/ResetPassword?userEmail={user.Email}&code={code}";

            await _emailService.SendEmailAsync(model.Email, "Reset Password",
                $"To reset your password, follow the <a href='{callbackUrl}'>link</a>");

            return result;
        }

        public async Task<IResult> ResetPassword(ResetPassword model)
        {
            var result = Result.SuccessResult();

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return result;
            }

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (!resetPasswordResult.Succeeded)
            {
                result = Result.ErrorResult();
                foreach (var error in resetPasswordResult.Errors)
                {
                    result.AddError(error.Code, error.Description);
                }
            }

            return result;
        }

        public async Task<IResult> UpdatePassword(UpdatePassword model)
        {
            try
            {
                if (_userManager.PasswordHasher.
                    VerifyHashedPassword(model.User, model.User.PasswordHash, model.OldPassword) == PasswordVerificationResult.Failed)
                {
                    return Result.ErrorResult().BuildMessage("Incorrect old password").
                        AddError("OldPassword", "Old password is not correct");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(model.User);

                var result = await _userManager.ResetPasswordAsync(model.User, token, model.NewPassword);

                if (!result.Succeeded)
                {
                    var errorResult = Result.ErrorResult().BuildMessage("Errors during update password");

                    foreach (var error in result.Errors)
                    {
                        errorResult.AddError(error.Code, error.Description);
                    }

                    return errorResult;
                }
            }
            catch (Exception ex)
            {
                return Result.ErrorResult().BuildMessage(ex.Message);
            }

            return Result.SuccessResult();
        }
    }
}
