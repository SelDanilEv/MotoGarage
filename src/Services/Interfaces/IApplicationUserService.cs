using Infrastructure.Models.Identity;
using Infrastructure.Models.User;
using Infrastructure.Result.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IApplicationUserService : ICommonRepositoryService<ApplicationUser>
    {
        Task<IResultWithData<List<UserModel>>> GetAllEmployees();
    }
}
