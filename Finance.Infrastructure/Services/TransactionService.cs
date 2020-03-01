using AutoMapper;
using Finance.Core.Repositories;
using Finance.Infrastructure.DTO;
using Finance.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;



        public TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository, IBankAccountRepository bankAccountRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _bankAccountRepository = bankAccountRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task AddTransactionAsync(string description, int categoryId, decimal value, DateTime date, int accountId, int userId)
        {
            var user = await _userRepository.GetAsync(userId);

            var category = user.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
            var bank = user.BankAccounts.FirstOrDefault(x => x.BanAccountId == accountId);


            if (category == null)
            {
                throw new Exception($"Cant Add Transaction becouse category with given ID:{categoryId} does not exists.");
            }
            if (bank == null)
            {
                throw new Exception($"Cant Add Transaction becouse bank with given ID:{accountId} does not exists.");
            }
            if (user == null)
            {
                throw new Exception($"Cant Add Transaction becouse user with given ID:{userId} does not exists.");
            }

            var transaction = bank.CreateTransaction(await _transactionRepository.NextId(), description, categoryId, value, date);

            await _userRepository.UpdateAsync(user);
            //await _transactionRepository.AddAsync(transaction);

        }

        public async Task<TransactionDto> GetAsync(int transactionId)
        {
            var transaction = await _transactionRepository.GetAsync(transactionId);

            return _mapper.Map<TransactionDto>(transaction);
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsByBankIdAsync(int bankId)
        {
            var transactions = await _transactionRepository.GetAllTransactionsByBankId(bankId);

            var transactionsDto = new List<TransactionDto>();

            foreach (var transaction in transactions)
            {
                transactionsDto.Add(_mapper.Map<TransactionDto>(transaction));
            }

            return transactionsDto;
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsByUserIdAsync(int userId)
        {
            var transactions = await _transactionRepository.GetAllTransactionsByUserId(userId);

            var transactionsDto = new List<TransactionDto>();

            foreach (var transaction in transactions)
            {
                transactionsDto.Add(_mapper.Map<TransactionDto>(transaction));
            }

            return transactionsDto;
        }

        public async Task RemoveTransactionAsync(int transactionId)
        {
            var transaction = await _transactionRepository.GetAsync(transactionId);

            if (transaction == null)
            {
                throw new Exception($"Cant remove transaction with ID:{transactionId} becouse transction does not exist");
            }

            await _transactionRepository.RemoveAsync(transaction);

        }

        public async Task RemoveTransactionsByBankAccountIdAsync(int bankAccountId)
        {
            var transactions = await _transactionRepository.GetAllTransactionsByBankId(bankAccountId);

            var tasks = new List<Task>();
            foreach (var transaction in transactions)
            {
                tasks.Add(_transactionRepository.RemoveAsync(transaction));
            }

            await Task.WhenAll(tasks);
        }

        public async Task RemoveTransactionsByCategoryIdAsync(int userId, int categoryId)
        {
            var transactions = await _transactionRepository.GetAllTransactionsByCategoryId(userId, categoryId);

            var tasks = new List<Task>();
            foreach (var transaction in transactions)
            {
                tasks.Add(_transactionRepository.RemoveAsync(transaction));
            }

            await Task.WhenAll(tasks);
        }

        public async Task RemoveTransactionsByUserIdAsync(int userId)
        {
            var transactions = await _transactionRepository.GetAllTransactionsByUserId(userId);

            var tasks = new List<Task>();
            foreach (var transaction in transactions)
            {
                tasks.Add(_transactionRepository.RemoveAsync(transaction));
            }

            await Task.WhenAll(tasks);
        }
    }
}
