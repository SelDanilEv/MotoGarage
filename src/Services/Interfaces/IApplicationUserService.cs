using Infrastructure.Dto;
using Infrastructure.Models;
using Infrastructure.Models.Identity;
using Infrastructure.Models.ServiceRequest;
using Infrastructure.Result.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IApplicationUserService : ICommonRepositoryService<ApplicationUser>
    {
    }
}
