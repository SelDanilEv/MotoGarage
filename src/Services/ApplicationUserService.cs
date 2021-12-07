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
        public async Task<IResultWithData<List<UserModel>>> GetAllUserModels()
        {
            var result = Result<List<UserModel>>.SuccessResult();

            var getAllResult = await this.GetItems();

            if (!getAllResult.IsSuccess)
            {
                return Result<List<UserModel>>.ErrorResult().BuildMessage("Error while get all users");
            }

            var userList = new List<UserModel>();

            foreach (var user in getAllResult.GetData)
            {
                var model = _mapper.Map<UserModel>(user);
                //model.Role = (await _accountManagerService.GetRoleByEmail(model.Email)).GetData;

                userList.Add(model);
            }

            result.Data = userList;

            return result;
        }
    }
}
