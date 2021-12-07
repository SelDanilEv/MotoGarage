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

namespace Services
{
    public class ApplicationUserService : CommonRepositoryService<ApplicationUser>, IApplicationUserService
    {
        private readonly IMapper _mapper;

        public ApplicationUserService(IOptions<MongoDbOption> mongoOption, IMapper mapper) : base(mongoOption)
        {
            _mapper = mapper;
        }

        // Remove that method
        public async Task<IResultWithData<List<UserEditModel>>> GetAllUserModels()
        {
            var result = Result<List<UserEditModel>>.SuccessResult();

            var getAllResult = await this.GetItems();

            if (!getAllResult.IsSuccess)
            {
                return Result<List<UserEditModel>>.ErrorResult().BuildMessage("Error while get all users");
            }

            var userList = new List<UserEditModel>();

            foreach (var user in getAllResult.GetData)
            {
                var model = _mapper.Map<UserEditModel>(user);
                //model.Role = (await _accountManagerService.GetRoleByEmail(model.Email)).GetData;

                userList.Add(model);
            }

            result.Data = userList;

            return result;
        }
    }
}
