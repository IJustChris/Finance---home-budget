using System;
using System.Collections.Generic;

namespace Finance.Infrastructure.DTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public IEnumerable<BankAccountDto> BankAccounts { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; }
        public IEnumerable<BudgetDto> Budgets { get; set; }

    }
}
