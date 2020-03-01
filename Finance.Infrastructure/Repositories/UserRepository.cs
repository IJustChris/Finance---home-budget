using Finance.Core.Domain;
using Finance.Core.Repositories;
using Finance.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private FinanceContext _context;

        public UserRepository(FinanceContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
            => await _context
                    .Users
                    .Include(x => x.BankAccounts)
                    .Include(x => x.Budgets)
                    .Include(x => x.Categories)
                    .ToListAsync();


        public async Task<User> GetAsync(string email)
            => await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User> GetAsync(int id)
            => await _context.Users
                    .Include(x => x.BankAccounts)
                    .Include(x => x.Budgets)
                    .Include(x => x.Categories)
                    .FirstOrDefaultAsync(x => x.UserId == id);


        public async Task<int> NextId()
        {
            int nextId;

            _context.Database.OpenConnection();

            var cmd = new NpgsqlCommand("SELECT nextval('user_id_seq')", (NpgsqlConnection)_context.Database.GetDbConnection());
            var id = await cmd.ExecuteScalarAsync();
            nextId = (int)(long)id;

            _context.Database.CloseConnection();
            
            return nextId;
        }

        public async Task RemoveAsync(User user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();    
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
