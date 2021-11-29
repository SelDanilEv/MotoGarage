using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace MotoGarage.Controllers
{
    [AllowAnonymous]
    [Route("Home")]
    public class HomeController : BaseController
    {
        public HomeController(IAccountManagerService accountManagerService, IMapper mapper) : base(accountManagerService, mapper)
        {
        }
    }
}
