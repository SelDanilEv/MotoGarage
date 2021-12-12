using Infrastructure.Models.Identity;
using Services.Interfaces;
using Microsoft.Extensions.Options;
using Infrastructure.Options;
using System.Threading.Tasks;
using Infrastructure.Result.Interfaces;
using Infrastructure.Models.User;
using System.Collections.Generic;
using Infrastructure.Result;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Enums;

namespace Services
{
    public class ApplicationUserService : CommonRepositoryService<ApplicationUser>, IApplicationUserService
    {
        private readonly IMapper _mapper;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationUserService(
            IOptions<MongoDbOption> mongoOption,
            RoleManager<ApplicationRole> roleManager,
            IMapper mapper
            ) : base(mongoOption)
        {
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<IResultWithData<List<UserModel>>> GetAllEmployees()
        {
            var result = Result<List<UserModel>>.SuccessResult();

            var getAllResult = await this.GetItems();

            if (!getAllResult.IsSuccess)
            {
                return Result<List<UserModel>>.ErrorResult().BuildMessage("Error while get all users");
            }

            var employeeRole = await _roleManager.FindByNameAsync(Roles.Employee);

            var employeesList = getAllResult.GetData.Where(x => x.Roles.First() == employeeRole.Id).ToList();

            result.Data = _mapper.Map<List<UserModel>>(employeesList);

            return result;
        }
    }
}
