using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using MotoGarage.Filters;
using AutoMapper;
using Infrastructure.Models.CommonModels;

namespace MotoGarage.Controllers
{
    [ExtractUserAttribute]
    [ApiController]
    public class BaseController : Controller
    {
        public readonly IAccountManagerService _accountManagerService;
        public readonly IMapper _mapper;

        public CurrentUser CurrentUser;

        public BaseController(
            IAccountManagerService accountManagerService,
            IMapper mapper)
        {
            this._accountManagerService = accountManagerService;
            this._mapper = mapper;
        }
    }
}
