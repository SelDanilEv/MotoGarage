using Infrastructure.Extensions;
using Infrastructure.Models.CommonModels;
using Microsoft.AspNetCore.Mvc.Filters;
using MotoGarage.Controllers;
using System.Threading.Tasks;

namespace MotoGarage.Filters
{
    public class ExtractUserAttribute : ActionFilterAttribute
    {
        private const string _userSessionKey = "CurrentUser";

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var thisController = ((BaseController)context.Controller);

            thisController.CurrentUser = context.HttpContext.Session.GetObjectFromJson<CurrentUser>(_userSessionKey);

            if (thisController.CurrentUser == default(CurrentUser))
            {
                var getCurrentUserResult = await thisController._accountManagerService.GetApplicationUser(context.HttpContext.User);

                if (getCurrentUserResult.IsSuccess)
                {
                    var appUser = getCurrentUserResult.GetData;

                    thisController.CurrentUser = thisController._mapper.Map<CurrentUser>(appUser);

                    var getRoleResult = await thisController._accountManagerService.GetRoleById(appUser.Id.ToString());

                    if (getRoleResult.IsSuccess)
                    {
                        thisController.CurrentUser.Role = getRoleResult.GetData;
                    }

                    context.HttpContext.Session.SetObjectAsJson(_userSessionKey, thisController.CurrentUser);
                }
            }

            await next();
        }
    }
}
