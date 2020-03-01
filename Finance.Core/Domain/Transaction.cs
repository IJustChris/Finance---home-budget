using Finance.Core.Domain.Exceptions;
using Finance.Core.Domain.Extensions;
using System;

namespace Finance.Core.Domain
{
    public class Transaction
    {
        // PROPERTIES

        public int TransactionId { get; protected set; }
        public int BankAccountId { get; protected set; }
        public int UserId { get; protected set; }
        public string Description { get; protected set; }
        public decimal Value { get; protected set; }
        public int CategoryId { get; protected set; }
        public DateTime Date { get; protected set; }


        // EVENTS

        public delegate bool BankAccountIDChangedEventHandler(Transaction transaction, int bankID);
        public event BankAccountIDChangedEventHandler BankAccountIDChanedEvent;
        protected bool InvokeBankAccountIDChanedEvent(Transaction transaction, int bankId)
            => BankAccountIDChanedEvent(transaction, bankId);

        public delegate void ValueChangedEventHandler(Transaction transaction, decimal previousValue);
        public event ValueChangedEventHandler ValueChangedEvent;
        protected void InvokeValueChangedEvent(Transaction transaction, decimal previousValue)
            => ValueChangedEvent(transaction, previousValue);




        // CONSTRUCTORS

        protected Transaction()
        {

        }

        protected Transaction(int transactionId, string description, int categoryId, decimal value, DateTime date, int bankAccountId, int userId)
        {
            if (transactionId.isEmpty() || categoryId.isEmpty() || categoryId.isEmpty() || bankAccountId.isEmpty() || userId.isEmpty())
                throw new DomainException(DomainErrorCodes.EmptyId);

            TransactionId = transactionId;
            setCategory(categoryId);
            setBankAccount(bankAccountId);
            UserId = userId;

            Description = description;
            Value = value;
            SetDate(date);


        }

        public static Transaction Create(int transactionId, string description, int categoryId, decimal value, DateTime date, int bankAccountId, int userId)
        {
            return new Transaction(transactionId, description, categoryId, value, date, bankAccountId, userId);
        }


        //SETTERS

        public virtual void setValue(decimal value)
        {
            var old = Value;
            Value = value;

            ValueChangedEvent?.Invoke(this, old);
        }

        public virtual void setDescription(string description)
        {
            if (description.Length > 40)
            {
                throw new DomainException(DomainErrorCodes.StringTooLong, "Description cant be longer than 40 characters");
            }
            Description = description;

        }

        public virtual void setCategory(int categoryId)
        {
            if (categoryId.isEmpty())
                throw new DomainException(DomainErrorCodes.EmptyId);

            CategoryId = categoryId;
        }

        /// <summary>
        /// When user change the BankID of transaction it is necessary to delete this transaction from BankAccount that not belong to anymore And add transaction to the list of transaction of new BankAccount. 
        /// 
        /// <para>This function fires up an event that BankAccount Subscribed, and the BankAccount fires up an event that User subscribe.
        /// If User private delegate will say that BankAccount with given ID exists it will move transaction to choosen BankAccount list. </para>
        /// 
        /// <para>Throws DomainException with error code= "bank_acc_nfound" if BankAccount with given ID won't be found</para>
        /// </summary>
        /// <param name="bankAccountId"> id of new bank account</param>
        public virtual void setBankAccount(int bankAccountId)
        {
            if (bankAccountId.isEmpty())
                throw new DomainException(DomainErrorCodes.EmptyId);

            if (BankAccountId.isEmpty())
            {
                BankAccountId = bankAccountId;
            }
            else if (BankAccountId != bankAccountId)
            {
                if (BankAccountIDChanedEvent.Invoke(this, bankAccountId))
                {
                    BankAccountId = bankAccountId;
                }
            }
        }

        public virtual void SetDate(DateTime date)
            => Date = date;


    }
}
