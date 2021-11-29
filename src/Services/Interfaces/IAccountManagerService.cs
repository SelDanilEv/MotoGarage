using Infrastructure.Dto.User;
using Infrastructure.Models.CommonModels;
using Infrastructure.Models.Identity;
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

        Task<IResult> CreateUser(UserDto user);

        Task<IResultWithData<string>> GetRoleByEmail(string email);
        Task<IResultWithData<string>> GetRoleById(string id);

        Task<IResult> CreateRole(string roleName);
        Task<IResult> GrantRoleByEmail(string email, string roleName);
        Task<IResult> GrantRoleById(string id, string roleName);

        Task<IResult> RemoveRolesByEmail(string email);
        Task<IResult> RemoveRolesById(string id);
    }
}
