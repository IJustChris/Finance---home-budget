using Finance.Core.Domain.Exceptions;
using Finance.Core.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Finance.Core.Domain
{
    public class User
    {
        private static string usernameRegex = @"^[\w\d]+[\w\d]+$";
        private static string emailRegex = @"^[\w\d\.]+@[\w]+\.[\w\d]+$";


        private ISet<BankAccount> _bankAccounts = new HashSet<BankAccount>();
        private ISet<Category> _categories = new HashSet<Category>();
        private ISet<Budget> _budgets = new HashSet<Budget>();

        public int UserId { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Username { get; protected set; }
        public int RoleId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public IEnumerable<BankAccount> BankAccounts => _bankAccounts;
        public IEnumerable<Category> Categories => _categories;
        public IEnumerable<Budget> Budgets => _budgets;


        protected User()
        {

        }

        protected User(int userId, string email, string username, int roleId, string password, string salt)
        {
            if (userId.isEmpty())
            {
                throw new DomainException(DomainErrorCodes.NullValue, "Cant create user. UserId cant be null value!");
            }
            UserId = userId;

            SetEmail(email);
            SetPassword(password);
            Salt = salt;
            RoleId = roleId;
            SetUsername(username);
            CreatedAt = DateTime.UtcNow;
        }


        public static User Create(int userId, string email, string username, int roleId, string password, string salt)
        {
            return new User(userId, email, username, roleId, password, salt);
        }

        // USER SETTERS

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new DomainException(DomainErrorCodes.InvalidPassword, "Password cant be empty!");
            }
            if (password.Length < 6)
            {
                throw new DomainException(DomainErrorCodes.InvalidPassword, "Password have to be at least 6 characters long");
            }

            Password = password;
            Update();
        }

        public void SetUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new DomainException(DomainErrorCodes.EmptyString, "Username cant be empty!");
            }
            if (!Regex.IsMatch(username, usernameRegex))
            {
                throw new DomainException(DomainErrorCodes.InvalidUsername, "Username contains forbidden characters");
            }
            if (username.Length <= 3)
            {
                throw new DomainException(DomainErrorCodes.StringTooShort, "Username have to be longer than 3 characters");
            }
            if (username.Length > 20)
            {
                throw new DomainException(DomainErrorCodes.StringTooLong, "Username cant be longer than 20 characters");
            }

            if (username == Username)
            {
                return;
            }

            Username = username;
            Update();
        }

        public void SetFirstname(string firstname)
        {
            if (firstname.Length > 20)
                throw new DomainException(DomainErrorCodes.StringTooLong);

            if (firstname.isEmpty())
                throw new DomainException(DomainErrorCodes.EmptyString);

            Firstname = firstname;
        }

        public void SetLastname(string lastname)
        {
            if (lastname.Length > 20)
                throw new DomainException(DomainErrorCodes.StringTooLong);

            if (lastname.isEmpty())
                throw new DomainException(DomainErrorCodes.StringTooLong);

            Lastname = lastname;
        }

        public void SetEmail(string email)
        {
            if (!Regex.IsMatch(email, emailRegex))
            {
                throw new DomainException(DomainErrorCodes.InvalidEmail, "Provided invalid email!");
            }
            else if (email.Length > 40)
            {
                throw new DomainException(DomainErrorCodes.StringTooLong, "Email cant be longer than 40 characters");
            }
            if (Email != null && Email.ToLower() == email.ToLower())
            {
                return;
            }

            Email = email.ToLower();
            Update();
        }


        // BANK ACCOUNT CONTROLS


        public BankAccount CreateBankAccount(int bankAccountId, string bankAccountName, decimal initialBalance, string currency, string hexColor)
        {
            var acc = BankAccount.Create(bankAccountId, bankAccountName, initialBalance, UserId, currency, hexColor);

            acc.TransactionHaveChangedBankAccountIDEvent += OnTransactionChangeBankAccountId;
            acc.TransferCreatedEvent += OnTransferCreated;
            acc.TransferRemovedEvent += OnTransferRemoved;
            acc.SourceBankAccountIDOfTransferChangedEvent += OnSourceBankAccountIDChanged;
            acc.DestinationBankAccountIDOfTransferChangedEvent += OnDestinationBankAccountIDChanged;

            _bankAccounts.Add(acc);
            return acc;
        }

        public BankAccount GetBankAccount(int id)
        {
            var acc = _bankAccounts.SingleOrDefault(x => x.BanAccountId == id);

            if (acc == null)
                throw new DomainException(DomainErrorCodes.BankAccountNotFound);

            return acc;
        }

        public void RemoveBankAccountAndTransactionsAssignedToIt(int id)
        {
            var acc = GetBankAccount(id);

            foreach (var transaction in acc.Transactions)
                acc.RemoveTransaction(transaction);

            _bankAccounts.Remove(acc);
        }


        //EVENT HANDLERS

        private bool OnTransactionChangeBankAccountId(Transaction transaction, int bankID)
        {
            var oldBank = _bankAccounts.FirstOrDefault(x => x.BanAccountId == transaction.BankAccountId);
            var newBank = _bankAccounts.FirstOrDefault(x => x.BanAccountId == bankID);

            if (newBank == null)
                throw new DomainException(DomainErrorCodes.BankAccountNotFound);

            newBank.AddTransaction(transaction);
            return true;
        }

        private bool OnDestinationBankAccountIDChanged(Transfer transfer, int bankID)
        {
            var bank = GetBankAccount(bankID);

            if (bank == null)
                return false;

            bank.AddDestinationTransfer(transfer);
            return true;
        }

        private bool OnSourceBankAccountIDChanged(Transfer transfer, int bankID)
        {
            var bank = GetBankAccount(bankID);

            if (bank == null)
                return false;

            bank.AddSourceTransfer(transfer);
            return true;
        }


        private bool OnTransferCreated(Transfer transfer)
        {
            var bank = GetBankAccount(transfer.DestinationBankAccountId);

            if (bank == null)
                return false;
            else
                bank.AddDestinationTransfer(transfer);

            return true;
        }

        private void OnTransferRemoved(int transferId, int bankId)
        {
            var bank = GetBankAccount(bankId);
            var transaction = bank.GetTransaction(transferId);
            bank.RemoveTransaction(transaction);
        }

        private decimal OnGetActualBudgetExpenditure(IEnumerable<int> categoriesID)
        {
            decimal expanditure = new decimal();

            foreach (var bank in BankAccounts)
                foreach (var transaction in bank.Transactions)
                    if (categoriesID.Contains(transaction.CategoryId))
                        expanditure += transaction.Value;

            return expanditure;
        }


        // CATEGORIES CONTROLS

        public Category CreateCategory(int id, string name, string iconName, string hexColor)
        {
            var category = Category.Create(id, name, UserId, iconName, hexColor);
            _categories.Add(category);

            return category;
        }

        public Category CreateSubcategory(int parentId, int id, string name, string iconName, string hexColor)
        {
            var category = _categories.FirstOrDefault(x => x.CategoryId == parentId);

            if (category != null)
            {
                category = category.CreateSubcategory(id, name, iconName, hexColor);
            }
            else
                throw new DomainException(DomainErrorCodes.CategoryNotFound);

            return category;
        }

        public Category GetCategory(int id)
        {
            var cat = _categories.SingleOrDefault(x => x.CategoryId == id);

            if (cat == null)
                throw new DomainException(DomainErrorCodes.CategoryNotFound);

            return cat;
        }

        public void RemoveCategory(int id)
        {
            var category = GetCategory(id);

            if (CheckIfAnyTransactionIsUsingCategory(category.CategoryId))
                throw new DomainException(DomainErrorCodes.CategoryInUse);

            _categories.Remove(category);
        }

        private bool CheckIfAnyTransactionIsUsingCategory(int categoryID)
        {
            foreach (var bankacc in _bankAccounts)
                foreach (var transaction in bankacc.Transactions)
                    if (transaction.CategoryId == categoryID)
                        return true;

            return false;
        }


        // BUDGETS CONTROLS

        public Budget CreateBudget(string name, int id, string colorHex, string iconName, decimal budgetValue, IEnumerable<int> categoriesID)
        {
            var budget = Budget.Create(name, id, colorHex, iconName, budgetValue, categoriesID, UserId);

            _budgets.Add(budget);

            return budget;
        }

        public Budget GetBudget(int id)
        {
            var budget = _budgets.SingleOrDefault(x => x.BudgetId == id);

            if (budget == null)
                throw new DomainException(DomainErrorCodes.BudgetNotFound);

            return budget;
        }

        public void RemoveBudget(int id)
        {
            var budget = GetBudget(id);
            _budgets.Remove(budget);
        }


        protected void Update()
            => UpdatedAt = DateTime.UtcNow;

    }
}
