using Finance.Core.Domain;
using Finance.Core.Repositories;
using Finance.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private FinanceContext _context;

        public BankAccountRepository(FinanceContext context)
        {
            _context = context;
        }
        public async Task AddAsync(BankAccount bankAccount)
        {
            _context.BankAccounts.Add(bankAccount);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BankAccount>> GetAllBankAccountsByUserIdAsync(int userID)
            => await _context.BankAccounts.Where(x => x.UserId == userID).ToListAsync();

        public async Task<BankAccount> GetAsync(int id)
            => await _context.BankAccounts.FirstOrDefaultAsync(x => x.BanAccountId == id);

        public async Task<int> NextId()
        {
            int nextId;

            _context.Database.OpenConnection();

            var cmd = new NpgsqlCommand("SELECT nextval('bank_account_id_seq')", (NpgsqlConnection)_context.Database.GetDbConnection());
            var id = await cmd.ExecuteScalarAsync();
            nextId = (int)(long)id;

            _context.Database.CloseConnection();
            

            return nextId;
        }

        public async Task RemoveAsync(BankAccount bankAccount)
        {
            _context.BankAccounts.Remove(bankAccount);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BankAccount bankAccount)
        {
            _context.BankAccounts.Update(bankAccount);
            await _context.SaveChangesAsync();
        }
    }
}
