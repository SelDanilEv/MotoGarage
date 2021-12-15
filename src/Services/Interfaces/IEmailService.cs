using Infrastructure.Result.Interfaces;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IEmailService
    {
        Task<IResult> SendEmailAsync(string recipient, string title, string body);
    }
}
