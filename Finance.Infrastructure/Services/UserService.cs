using AutoMapper;
using Finance.Core.Domain;
using Finance.Core.Repositories;
using Finance.Infrastructure.DTO;
using Finance.Infrastructure.Services.Exceptions;
using Finance.Infrastructure.Services.Interfaces;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICategoryService _categoryService;
        private readonly IBankAccountService _bankAccountService;

        private readonly IMapper _mapper;
        private readonly IEncrypter _encrypter;


        public UserService(IUserRepository userRepository, ICategoryService categoryService, IBankAccountService bankAccountService, IMapper mapper, IEncrypter encrypter)
        {
            _userRepository = userRepository;

            _categoryService = categoryService;
            _bankAccountService = bankAccountService;

            _mapper = mapper;
            _encrypter = encrypter;
        }

        public async Task<UserDto> GetUserAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);
            var userDto = _mapper.Map<UserDto>(user);

            userDto.BankAccounts = await _bankAccountService.GetAllBankAccountsByUserIdAsync(user.UserId);
            userDto.Categories = await _categoryService.GetCategoriesByUserIdAsync(user.UserId);

            return userDto;
        }

        public async Task RegisterAsync(string email, string password, string username, int roleId)
        {
            if (await _userRepository.GetAsync(email) != null)
            {
                throw new ServiceException(ServiceErrorCodes.EmailInUse, $"Email '{email}' is already in use.");
            }

            var salt = _encrypter.GetSalt();
            var hash = _encrypter.GetHash(password, salt);
            var id = await _userRepository.NextId();
            var user = User.Create(id, email, username, roleId, hash, salt);
            await _userRepository.AddAsync(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);

            if (user == null)
            {
                throw new ServiceException(ServiceErrorCodes.InvalidCredentails, "Incorrect email or password");
            }

            var hash = _encrypter.GetHash(password, user.Salt);

            if (hash != user.Password)
            {
                throw new ServiceException(ServiceErrorCodes.InvalidCredentails, "Incorrect email or password");
            }

        }
    }
}
