using AutoMapper;
using Infrastructure.Dto.NavMenu;
using Infrastructure.Models.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;

namespace MotoGarage.Controllers
{
    [AllowAnonymous]
    [Route("api/NavMenu")]
    public class NavMenuController : BaseController
    {
        private INavMenuService _navMenuService;

        public NavMenuController
            (IAccountManagerService accountManagerService,
            INavMenuService navMenuService,
            IMapper mapper) : base(accountManagerService, mapper)
        {
            this._navMenuService = navMenuService;
        }

        [HttpGet]
        [Route("GetMenuItems")]
        public async Task<IActionResult> GetMenuItems()
        {
            var getAvailableMenuItemResult = await _navMenuService.GetAvailableNavMenuItems(CurrentUser?.Role);

            if (getAvailableMenuItemResult.IsSuccess)
            {
                return Json(getAvailableMenuItemResult.GetData);
            }

            return Json(getAvailableMenuItemResult.Message);
        }

        [HttpPost]
        [Route("CreateNavMenuItem")]
        public async Task<IActionResult> CreateNavMenuItem(CreateNavMenuItemDto createNavMenuItemDto)
        {
            var newMenuItem = _mapper.Map<NavMenuItem>(createNavMenuItemDto);

            var createNavMenuItemItem = await _navMenuService.AddItem(newMenuItem);

            if (createNavMenuItemItem.IsSuccess)
            {
                return Json("Created successfully");
            }

            return Json(createNavMenuItemItem.Message);
        }

        [HttpPost]
        [Route("UpdateNavMenuItem")]
        public async Task<IActionResult> UpdateNavMenuItem(UpdateNavMenuItemDto updateNavMenuItemDto)
        {
            var newMenuItem = _mapper.Map<NavMenuItem>(updateNavMenuItemDto);

            var createNavMenuItemItem = await _navMenuService.UpdateItem(newMenuItem);

            if (createNavMenuItemItem.IsSuccess)
            {
                return Json("Created successfully");
            }

            return Json(createNavMenuItemItem.Message);
        }
    }
}
