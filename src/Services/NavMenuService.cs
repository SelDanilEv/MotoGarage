using Infrastructure.Result.Interfaces;
using System.Threading.Tasks;
using Infrastructure.Result;
using Services.Interfaces;
using Infrastructure.Models.Menu;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using Infrastructure.Dto.NavMenu;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Helpers;
using AutoMapper;

namespace Services
{
    public class NavMenuService : CommonRepositoryService<NavMenuItem>, INavMenuService
    {
        private readonly IMapper _mapper;

        public NavMenuService(
            IMapper mapper,
            IOptions<MongoDbOption> mongoOption) : base(mongoOption)
        {
            this._mapper = mapper;
        }

        public async Task<IResultWithData<IList<NavMenuItemDto>>> GetAvailableNavMenuItems(string role = "")
        {
            var result = Result<IList<NavMenuItemDto>>.SuccessResult();

            var getAllMenuItemsResults = await GetItems();

            var avalaibleItems = new List<NavMenuItemDto>();

            if (getAllMenuItemsResults.IsSuccess)
            {
                var menuItemList = getAllMenuItemsResults.GetData.OrderBy(x => -x.Priority).ToList();

                foreach (var menuItem in menuItemList)
                {
                    if (RolesHelper.IsResourceAvailableForRole(role ?? "", menuItem.AccessLevel))
                    {
                        avalaibleItems.Add(
                            _mapper.Map<NavMenuItemDto>(menuItem));
                    }
                }

                result.Data = avalaibleItems;
            }
            else
            {
                return Result<IList<NavMenuItemDto>>.ErrorResult();
            }

            return result;
        }
    }
}
