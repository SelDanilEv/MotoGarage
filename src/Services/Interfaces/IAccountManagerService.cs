using Infrastructure.Dto.User;
using Infrastructure.Models.CommonModels;
using Infrastructure.Models.Identity;
using Infrastructure.Models.ResetPassword;
using Infrastructure.Models.User;
using Infrastructure.Result.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAccountManagerService
    {
        Task<IResultWithData<ApplicationUser>> GetUserByEmail(string email);

        Task<IResultWithData<ApplicationUser>> GetApplicationUser(ClaimsPrincipal User);
        Task<IResultWithData<CurrentUser>> GetCurrentUser(ApplicationUser User);

        Task<IResult> CreateUser(CreateUserDto user);
        Task<IResult> UpdateUserInfo(UpdateUserInfo updateUserInfo);

        Task<IResult> SendResetPasswordMessage(ForgotPassword model, string baseUrl);
        Task<IResult> ResetPassword(ResetPassword model);
        Task<IResult> UpdatePassword(UpdatePassword model);

        Task<IResultWithData<string>> GetRoleByEmail(string email);
        Task<IResultWithData<string>> GetRoleById(string id);

        Task<IResult> CreateRole(string roleName);
        Task<IResult> GrantRoleByEmail(string email, string roleName);
        Task<IResult> GrantRoleById(string id, string roleName);

        Task<IResult> RemoveRolesByEmail(string email);
        Task<IResult> RemoveRolesById(string id);
    }
}
