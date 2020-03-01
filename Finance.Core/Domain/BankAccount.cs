using Finance.Core.Domain.Exceptions;
using Finance.Core.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Finance.Core.Domain
{
    public class BankAccount
    {
        private readonly ISet<Transaction> _transactions = new HashSet<Transaction>();

        // EVENTS
        public delegate bool BankAccountIDChanged(Transaction transaction, int bankID);
        public event BankAccountIDChanged TransactionHaveChangedBankAccountIDEvent;

        public delegate bool TransferCreatedEventHandler(Transfer transaction);
        public event TransferCreatedEventHandler TransferCreatedEvent;

        public delegate void TransferRemovedEventHandler(int transferId, int bankId);
        public event TransferRemovedEventHandler TransferRemovedEvent;

        public delegate bool DestinationBankAccountIDOfTransferChangedEventHandler(Transfer transferId, int bankId);
        public event DestinationBankAccountIDOfTransferChangedEventHandler DestinationBankAccountIDOfTransferChangedEvent;

        public delegate bool SourceBankAccountIDOfTransferChangedEventHandler(Transfer transferId, int bankId);
        public event SourceBankAccountIDOfTransferChangedEventHandler SourceBankAccountIDOfTransferChangedEvent;

        // PROPERTIES
        public int BanAccountId { get; protected set; }
        public int UserId { get; protected set; }
        public string AccountName { get; protected set; }
        public decimal InitialBalance { get; protected set; }
        public decimal Balance { get; protected set; }
        public string Currency { get; protected set; }
        public string HexColor { get; protected set; }

        public IEnumerable<Transaction> Transactions => _transactions;


        // CONSTRUCTORS

        protected BankAccount()
        {

        }

        protected BankAccount(int id, string accountName, decimal initialBalance, int userId, string currency, string hexColor)
        {
            if (id.isEmpty() || userId.isEmpty())
                throw new DomainException(DomainErrorCodes.EmptyId);

            UserId = userId;
            BanAccountId = id;
            SetAccountName(accountName);
            InitialBalance = initialBalance;
            Balance = initialBalance;
            SetCurrency(currency);
            SetHexColor(hexColor);
        }

        public static BankAccount Create(int bankAccountId, string accountName, decimal initialBalance, int userId, string currency, string hexColor)
        {
            if (bankAccountId.isEmpty())
            {
                throw new DomainException(DomainErrorCodes.NullValue, "Cant create Bank Account. bankAccountId cant be null!");
            }
            if (userId.isEmpty())
            {
                throw new DomainException(DomainErrorCodes.NullValue, "Cant create Bank Account. userId cant be null!");
            }

            return new BankAccount(bankAccountId, accountName, initialBalance, userId, currency, hexColor);
        }


        // TRANSACTION CONTROLS
        public Transaction CreateTransaction(int transactionId, string description, int categoryId, decimal value, DateTime date)
        {
            var transaction = Transaction.Create(transactionId, description, categoryId, value, date, BanAccountId, UserId);
            transaction.BankAccountIDChanedEvent += OnBankAccountIDOfTransactionChanged;

            Balance += transaction.Value;
            _transactions.Add(transaction);

            return transaction;
        }

        public Transaction AddTransaction(Transaction transaction)
        {
            //if (transaction.BankAccountId != Id)
            //    throw new DomainException(DomainErrorCodes.WrongBankId);

            transaction.BankAccountIDChanedEvent += OnBankAccountIDOfTransactionChanged;
            transaction.ValueChangedEvent += OnValueOfTransactionChanged;
            if (transaction is Transfer transfer)
            {
                transfer.DestinationBankAccountIDChanedEvent += OnDestinationBankAccountIDOfTransferChanged;
                Balance += transaction.Value;

            }
            else
                Balance += transaction.Value;


            _transactions.Add(transaction);
            return transaction;
        }

        public Transaction GetTransaction(int id)
        {
            var transaction = _transactions.SingleOrDefault(x => x.TransactionId == id);

            return transaction;
        }

        public void RemoveTransaction(Transaction transaction)
        {
            if (transaction == null)
                return;

            if (transaction is Transfer transfer)
            {
                if (transfer.BankAccountId == BanAccountId)
                {
                    Balance += transfer.Value;
                    _transactions.Remove(transaction);
                    TransferRemovedEvent(transfer.TransactionId, transfer.DestinationBankAccountId);
                }
                else
                {
                    Balance -= transfer.Value;
                    _transactions.Remove(transaction);
                    TransferRemovedEvent(transfer.TransactionId, transfer.BankAccountId);
                }
            }
            else
            {
                Balance -= transaction.Value;
                _transactions.Remove(transaction);
            }
        }

        protected void RemoveTransactionAferBankChange(Transaction transaction)
        {
            if (transaction == null)
                return;

            if (transaction is Transfer transfer)
            {
                if (transfer.BankAccountId == BanAccountId)
                {
                    Balance += transfer.Value;
                    _transactions.Remove(transaction);
                }
                else
                {
                    Balance -= transfer.Value;
                    _transactions.Remove(transaction);
                }
            }
            else
            {
                Balance -= transaction.Value;
                _transactions.Remove(transaction);
            }
        }

        // TRANSFER CONTROLS

        public Transfer CreateTransfer(int transactionId, int destinationBankId, string description, decimal value, DateTime date)
        {
            var transfer = Transfer.Create(transactionId, destinationBankId, description, value, date, BanAccountId, UserId);

            //Returns False if second bankAccount was not found
            if (TransferCreatedEvent(transfer) == true)
                transfer = AddSourceTransfer(transfer);
            else
                throw new DomainException(DomainErrorCodes.BankAccountNotFound);

            return transfer;
        }

        public Transfer AddSourceTransfer(Transfer transfer)
        {
            transfer.ValueChangedEvent += OnValueOfTransactionChanged;
            transfer.SourceBankAccountIDChanedEvent += OnSourceBankAccountIDOfTransferChanged;

            Balance -= transfer.Value;
            _transactions.Add(transfer);

            return transfer;
        }

        public Transfer AddDestinationTransfer(Transfer transfer)
        {
            transfer.ValueChangedEvent += OnValueOfTransactionChanged;
            transfer.DestinationBankAccountIDChanedEvent += OnDestinationBankAccountIDOfTransferChanged;

            Balance += transfer.Value;
            _transactions.Add(transfer);

            return transfer;
        }


        // EVENT HANDLERS

        protected virtual void OnValueOfTransactionChanged(Transaction transaction, decimal oldValue)
        {
            if (transaction is Transfer transfer)
            {
                if (transfer.BankAccountId == BanAccountId)
                {
                    Balance += oldValue;
                    Balance -= transfer.Value;
                }
                else if (transfer.DestinationBankAccountId == BanAccountId)
                {
                    Balance -= oldValue;
                    Balance += transfer.Value;
                }
                else
                    throw new DomainException(DomainErrorCodes.TransferNotBelongToThisBank);
            }
            else
            {
                Balance -= oldValue;
                Balance += transaction.Value;
            }

        }

        protected virtual bool OnBankAccountIDOfTransactionChanged(Transaction transaction, int bankID)
        {

            if (TransactionHaveChangedBankAccountIDEvent.Invoke(transaction, bankID))
            {
                transaction.BankAccountIDChanedEvent -= OnBankAccountIDOfTransactionChanged;
                transaction.ValueChangedEvent -= OnValueOfTransactionChanged;
                RemoveTransactionAferBankChange(transaction);
                return true;
            }

            return false;
        }

        protected virtual bool OnDestinationBankAccountIDOfTransferChanged(Transfer transfer, int bankID)
        {

            if (DestinationBankAccountIDOfTransferChangedEvent.Invoke(transfer, bankID))
            {
                transfer.ValueChangedEvent -= OnValueOfTransactionChanged;
                transfer.DestinationBankAccountIDChanedEvent -= OnDestinationBankAccountIDOfTransferChanged;

                RemoveTransactionAferBankChange(transfer);
                return true;
            }

            return false;
        }

        protected virtual bool OnSourceBankAccountIDOfTransferChanged(Transfer transfer, int bankID)
        {

            if (SourceBankAccountIDOfTransferChangedEvent.Invoke(transfer, bankID))
            {
                transfer.ValueChangedEvent -= OnValueOfTransactionChanged;
                transfer.SourceBankAccountIDChanedEvent -= OnSourceBankAccountIDOfTransferChanged;
                RemoveTransactionAferBankChange(transfer);

                return true;
            }

            return false;
        }
        // BANK ACCOUNT SETTERS

        public void SetAccountName(string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new DomainException(DomainErrorCodes.InvalidBankAccountName, "Account name cant be empty!");
            }
            if (accountName.Length >= 20)
            {
                throw new DomainException(DomainErrorCodes.InvalidBankAccountName, "Account name cant be longer than 20 characters!");
            }

            AccountName = accountName;
        }

        public void SetInitialBalance(decimal value)
        {
            if (InitialBalance == value)
                return;

            InitialBalance = value;
        }


        public void SetCurrency(string currency)
        {
            Currency = currency;
        }

        public void SetHexColor(string hexColor)
        {
            hexColor = hexColor.ToUpper();

            if (!hexColor.isHexColor())
                throw new DomainException(DomainErrorCodes.InvalidColor);

            HexColor = hexColor;

        }

    }
}
