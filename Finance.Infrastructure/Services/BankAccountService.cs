using AutoMapper;
using Finance.Core.Repositories;
using Finance.Infrastructure.DTO;
using Finance.Infrastructure.Extensions;
using Finance.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services
{
    public class BankAccountService : IBankAccountService
    {
        private IBankAccountRepository _bankAccountRepository;
        private IUserRepository _userRepository;
        private ITransactionService _transactionService;
        private readonly IMapper _mapper;



        public BankAccountService(IBankAccountRepository bankAccountRepository, IUserRepository userRepository, ITransactionService transactionService, IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _userRepository = userRepository;
            _transactionService = transactionService;
            _mapper = mapper;
        }

        public async Task CreateNewBankAccountAsync(int userId, string accountName, decimal initialBalance, string currency, string hexColor)
        {
            var user = await _userRepository.GetOrFailAsync(userId);

            var newBankAcc = user.CreateBankAccount(await _bankAccountRepository.NextId(), accountName, initialBalance, currency, hexColor);
            await _bankAccountRepository.AddAsync(newBankAcc);
        }

        public async Task<IEnumerable<BankAccountDto>> GetAllBankAccountsByUserIdAsync(int userId)
        {
            var banks = await _bankAccountRepository.GetAllBankAccountsByUserIdAsync(userId);
            var accounts = new List<BankAccountDto>();


            foreach (var bank in banks)
            {
                var bankDto = _mapper.Map<BankAccountDto>(bank);
                accounts.Add(bankDto);
            }

            return accounts;
        }

        public async Task<BankAccountDto> GetAsync(int id)
        {
            var bank = await _bankAccountRepository.GetAsync(id);
            var bankDto = _mapper.Map<BankAccountDto>(bank);

            return bankDto;

        }

        //Removes Bank Account and connected Transactions
        public async Task RemoveBankAccountAsync(int id)
        {
            var bank = await _bankAccountRepository.GetAsync(id);
            if (bank == null)
            {
                throw new Exception("Bank with given id doesnt exist");
            }

            await _transactionService.RemoveTransactionsByBankAccountIdAsync(bank.BanAccountId);
            await _bankAccountRepository.RemoveAsync(bank);

        }

        public async Task UpdateAsync(BankAccountDto dto)
        {
            var bank = await _bankAccountRepository.GetAsync(dto.BanAccountId);

            bank.SetCurrency(dto.Currency);
            bank.SetHexColor(dto.HexColor);
            bank.SetAccountName(dto.AccountName);
            bank.SetInitialBalance(dto.InitialBalance);

            await _bankAccountRepository.UpdateAsync(bank);
        }
    }
}
