using Finance.Infrastructure.DTO;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task<UserDto> GetUserAsync(string email);

        Task RegisterAsync(string email, string password, string username, int roleId);

        Task LoginAsync(string username, string password);

    }
}
