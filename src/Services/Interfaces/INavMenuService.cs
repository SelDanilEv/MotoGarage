using Infrastructure.Dto.NavMenu;
using Infrastructure.Models.Menu;
using Infrastructure.Result.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface INavMenuService : ICommonRepositoryService<NavMenuItem>
    {
        Task<IResultWithData<IList<NavMenuItemDto>>> GetAvailableNavMenuItems(string role);
    }
}
