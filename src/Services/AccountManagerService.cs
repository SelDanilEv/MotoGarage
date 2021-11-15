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

namespace Services
{
    public class AccountManagerService : IAccountManagerService
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;

        public AccountManagerService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<IResultWithData<ApplicationUser>> GetUserByEmail(string email)
        {
            var result = Result<ApplicationUser>.SuccessResult();

            var getUserResult = await _userManager.FindByEmailAsync(email);

            if (getUserResult == null)
            {
                return Result<ApplicationUser>.ErrorResult().BuildMessage("User with this email doesn't exist");
            }

            result.Data = getUserResult;

            return result;
        }

        public async Task<IResultWithData<ApplicationUser>> GetUserByLogin(string login)
        {
            var result = Result<ApplicationUser>.SuccessResult();

            var getUserResult = await _userManager.FindByNameAsync(login);

            if (getUserResult == null)
            {
                return Result<ApplicationUser>.ErrorResult().BuildMessage("User with this email doesn't exist");
            }

            result.Data = getUserResult;

            return result;
        }

        public async Task<IResultWithData<ApplicationUser>> GetCurrentUser(ClaimsPrincipal User)
        {
            var result = await _userManager.GetUserAsync(User);

            if (result == null)
            {
                return Result<ApplicationUser>.ErrorResult().BuildMessage("User not authorized");
            }

            return Result<ApplicationUser>.SuccessResult(result);
        }

        public async Task<IResult> CreateUser(UserDto user)
        {
            var result = Result.SuccessResult();

            var appUser = new ApplicationUser
            {
                UserName = user.Login,
                Email = user.Email
            };

            var createResult = await _userManager.CreateAsync(appUser, user.Password);

            if (!createResult.Succeeded)
            {
                var errorResult = Result.ErrorResult().BuildMessage("Create user operation is failed\n");

                foreach (var error in createResult.Errors)
                {
                    errorResult.AppendMessage(error.Description + "\n");
                }

                return errorResult;
            }

            await GrantRoleByEmailOrLoginOrId(user.Email, "Client");

            result.BuildMessage("User created");

            return result;
        }

        public async Task<IResultWithData<string>> GetRoleByEmailOrLoginOrId(string idOrEmailOrLogin)
        {
            var result = Result<string>.SuccessResult();

            var user = await _userManager.FindByNameAsync(idOrEmailOrLogin) ??
                await _userManager.FindByEmailAsync(idOrEmailOrLogin);

            if (Guid.TryParse(idOrEmailOrLogin, out var guid) && user == null)
            {
                user = await _userManager.FindByIdAsync(idOrEmailOrLogin);
            }

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles != null)
                {
                    if (roles.Count > 0)
                    {
                        result.Data =  roles[0];
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

        public async Task<IResult> GrantRoleByEmailOrLoginOrId(string idOrEmailOrLogin, string roleName)
        {
            var result = Result.SuccessResult();

            var user = await _userManager.FindByNameAsync(idOrEmailOrLogin) ??
                await _userManager.FindByEmailAsync(idOrEmailOrLogin);

            if (Guid.TryParse(idOrEmailOrLogin, out var guid) && user == null)
            {
                user = await _userManager.FindByIdAsync(idOrEmailOrLogin);
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

        public async Task<IResult> RemoveRolesByEmailOrLoginOrId(string idOrEmailOrLogin)
        {
            var result = Result.SuccessResult();

            var user = await _userManager.FindByEmailAsync(idOrEmailOrLogin);

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
    }
}
