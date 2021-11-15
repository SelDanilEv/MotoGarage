using Infrastructure.Models.Identity;
using Services.Interfaces;
using Microsoft.Extensions.Options;
using Infrastructure.Options;

namespace Services
{
    public class ApplicationUserService : CommonRepositoryService<ApplicationUser>, IApplicationUserService
    {
        public ApplicationUserService(IOptions<MongoDbOption> mongoOption) : base(mongoOption)
        {
        }
    }
}
