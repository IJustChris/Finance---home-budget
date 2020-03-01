using Finance.Infrastructure.DTO;

namespace Finance.Infrastructure.Services.Interfaces
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(int userId, string role);
    }
}
