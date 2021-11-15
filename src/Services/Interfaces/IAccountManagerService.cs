using Infrastructure.Dto.User;
using Infrastructure.Models.Identity;
using Infrastructure.Result.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAccountManagerService
    {
        Task<IResultWithData<ApplicationUser>> GetUserByEmail(string email);
        Task<IResultWithData<ApplicationUser>> GetUserByLogin(string login);
        Task<IResultWithData<ApplicationUser>> GetCurrentUser(ClaimsPrincipal User);

        Task<IResult> CreateUser(UserDto user);

        Task<IResultWithData<string>> GetRoleByEmailOrLoginOrId(string idOrEmailOrLogin);

        Task<IResult> CreateRole(string roleName);
        Task<IResult> GrantRoleByEmailOrLoginOrId(string idOrEmailOrLogin, string roleName);
        Task<IResult> RemoveRolesByEmailOrLoginOrId(string idOrEmailOrLogin);
    }
}
