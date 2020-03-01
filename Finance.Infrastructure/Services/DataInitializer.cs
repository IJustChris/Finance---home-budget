using Finance.Core.Domain;
using Finance.Core.Repositories;
using Finance.Infrastructure.Services.Interfaces;
using NLog;
using System;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly IBankAccountService _bankAccountService;
        private readonly ITransactionService _transactionService;
        private readonly ICategoryService _categoryService;
        private readonly IUserRepository _userRepository;

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();


        public DataInitializer(IUserService userService, IBankAccountService bankAccountService, ITransactionService transactionService, ICategoryService categoryService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
            _bankAccountService = bankAccountService;
            _transactionService = transactionService;
            _categoryService = categoryService;

        }

        public async Task SeedAsync()
        {
            _logger.Trace("Initialiazing data...");

            _logger.Debug("Creating users");
            await RegisterUsersAsync();
            await RegisterAdminsAsync();

            _logger.Debug("Getting users");

            var users = await _userRepository.GetAllUsersAsync();
            var usr = await _userRepository.GetAsync(16);

            var usr2 = await _userRepository.GetAsync(16);

            _logger.Debug("Creating bank accounts and Categories");


            foreach (var user in users)
            {
                try
                {
                    await _bankAccountService.CreateNewBankAccountAsync(user.UserId, $"Bank", 100m, "PLN", "#FFF");
                    await _bankAccountService.CreateNewBankAccountAsync(user.UserId, $"Cash", 10m, "PLN", "#FFF");

                    await _categoryService.AddCategoryAsync("category1", user.UserId, "test-icon", "#FFF");
                    await _categoryService.AddCategoryAsync("category2", user.UserId, "test-icon", "#FFF");
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                    _logger.Error(ex.StackTrace);
                }

            }

            _logger.Debug("Creating transactions");
            try
            {
                foreach (var user in users)
                    foreach (var bank in user.BankAccounts)
                        foreach (var category in user.Categories)
                        {
                            await _transactionService.AddTransactionAsync($"transaction of user: {user.Username}", category.CategoryId, -30.23m, DateTime.UtcNow, bank.BanAccountId, user.UserId);
                            await _transactionService.AddTransactionAsync($"transaction of user: {user.Username}", category.CategoryId, -0.99m, DateTime.UtcNow, bank.BanAccountId, user.UserId);
                        }
            }
            catch (Exception ex)
            {
                _logger.Debug(ex.Message + ex.StackTrace);
                throw;
            }


            _logger.Debug("Test Debuggera");
            _logger.Trace("Data was initilized");


        }

        private async Task RegisterUsersAsync()
        {
            await _userService.RegisterAsync($"user1@email.com", "SecretPassword123", $"user1", (int)UserRole.user);
            await _userService.RegisterAsync($"user2@email.com", "SecretPassword123", $"user2", (int)UserRole.user);
            await _userService.RegisterAsync($"user3@email.com", "SecretPassword123", $"user3", (int)UserRole.user);
            await _userService.RegisterAsync($"user4@email.com", "SecretPassword123", $"user4", (int)UserRole.user);
            await _userService.RegisterAsync($"user5@email.com", "SecretPassword123", $"user5", (int)UserRole.user);
        }

        private async Task RegisterAdminsAsync()
        {
            await _userService.RegisterAsync($"admin1@email.com", "SecretPassword123", $"admin1", (int)UserRole.admin);
            await _userService.RegisterAsync($"admin2@email.com", "SecretPassword123", $"admin2", (int)UserRole.admin);
        }
    }
}
