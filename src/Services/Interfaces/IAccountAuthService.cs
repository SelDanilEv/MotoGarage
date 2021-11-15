using Infrastructure.Dto;
using Infrastructure.Models;
using Infrastructure.Result.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAccountAuthService
    {
        Task<IResult> LoginByLoginOrEmail(string emailOrLogin, string password);

        Task<IResult> Logout();
    }
}
