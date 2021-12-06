using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IApplicationUserForeignKey
    {
        Task<T> SetApplicationUserFields<T>(object item);
    }
}
