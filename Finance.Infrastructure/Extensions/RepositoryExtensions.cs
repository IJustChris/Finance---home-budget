using Finance.Core.Domain;
using Finance.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<User> GetOrFailAsync(this IUserRepository repository, int userId)
        {
            var user = await repository.GetAsync(userId);
            if (user == null)
            {
                throw new Exception($"User with id: {userId} was not found!");
            }

            return user;
        }

    }
}
