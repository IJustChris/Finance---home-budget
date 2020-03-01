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
    public class TransactionRepository : ITransactionRepository
    {
        private FinanceContext _context;

        public TransactionRepository(FinanceContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsByBankId(int bankId)
                => await _context.Transactions.Where(x => x.BankAccountId == bankId).ToListAsync();

        public async Task<IEnumerable<Transaction>> GetAllTransactionsByCategoryId(int userId, int categoryId)
                => await _context.Transactions.Where(x => x.UserId == userId && x.CategoryId == categoryId).ToListAsync();


        public async Task<IEnumerable<Transaction>> GetAllTransactionsByUserId(int userId)
                => await _context.Transactions.Where(x => x.UserId == userId).ToListAsync();

        public async Task<Transaction> GetAsync(int id)
                => await _context.Transactions.FirstOrDefaultAsync(x => x.TransactionId == id);

        public async Task<int> NextId()
        {
            int nextId;

            _context.Database.OpenConnection();

            var cmd = new NpgsqlCommand("SELECT nextval('transaction_id_seq')", (NpgsqlConnection)_context.Database.GetDbConnection());
            var id = await cmd.ExecuteScalarAsync();
            nextId = (int)(long)id;

            _context.Database.CloseConnection();
            
            return nextId;
        }

        public async Task RemoveAsync(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();   
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
