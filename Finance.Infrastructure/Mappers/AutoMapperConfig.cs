using AutoMapper;
using Finance.Core.Domain;
using Finance.Infrastructure.DTO;

namespace Finance.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
               {
                   cfg.CreateMap<User, UserDto>();
                   cfg.CreateMap<Category, CategoryDto>();
                   cfg.CreateMap<Transaction, TransactionDto>();
                   cfg.CreateMap<BankAccount, BankAccountDto>();


               }).CreateMapper();

    }
}
