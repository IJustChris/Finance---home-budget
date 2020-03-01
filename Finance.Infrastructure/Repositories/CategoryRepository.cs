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
    public class CategoryRepository : ICategoryRepository
    {
        private FinanceContext _context;

        public CategoryRepository(FinanceContext context)
        {
            _context = context;
        }


        public async Task<int> NextId()
        {
            int nextId;

            _context.Database.OpenConnection();

            var cmd = new NpgsqlCommand("SELECT nextval('category_id_seq')", (NpgsqlConnection)_context.Database.GetDbConnection());
            var id = await cmd.ExecuteScalarAsync();
            nextId = (int)(long)id;

            _context.Database.CloseConnection();
            

            return nextId;
        }


        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            _context.SaveChanges();
        }


        public async Task<IEnumerable<Category>> GetAllCategoriesByUserIdAsync(int userId)
             => await _context.Categories.Where(x => x.UserId == userId).ToListAsync();

        


        public async Task<Category> GetAsync(int categoryId)
                => await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == categoryId);


        public async Task RemoveAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
