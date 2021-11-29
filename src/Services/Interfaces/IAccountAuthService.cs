using Infrastructure.Dto;
using Infrastructure.Models;
using Infrastructure.Models.CommonModels;
using Infrastructure.Result.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAccountAuthService
    {
        Task<IResultWithData<CurrentUser>> LoginByEmail(string email, string password);

        Task<IResult> Logout();
    }
}
