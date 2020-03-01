using Finance.Core.Domain.Exceptions;
using Finance.Core.Domain.Extensions;
using System;

namespace Finance.Core.Domain
{
    public class Transfer : Transaction
    {
        // PROPERTIES
        public int DestinationBankAccountId { get; protected set; }
        public int SourceBankAccountId { get; protected set; }


        // EVENETS
        public delegate bool DestinationBankAccountIDChangedEventHandler(Transfer transfer, int bankID);
        public event DestinationBankAccountIDChangedEventHandler DestinationBankAccountIDChanedEvent;

        public delegate bool SourceBankAccountIDChangedEventHandler(Transfer transfer, int bankID);
        public event SourceBankAccountIDChangedEventHandler SourceBankAccountIDChanedEvent;

        // CONSTRUCTORS
        protected Transfer() : base()
        {

        }

        protected Transfer(int transactionId, int destinationBankId, string description, decimal value, DateTime date, int bankAccountId, int userId)
        {
            if (transactionId.isEmpty() || destinationBankId.isEmpty() || bankAccountId.isEmpty() || userId.isEmpty())
                throw new DomainException(DomainErrorCodes.EmptyId);

            BankAccountId = bankAccountId;
            TransactionId = bankAccountId;
            DestinationBankAccountId = destinationBankId;
            setDescription(description);
            setValue(value);
            SetDate(date);
            UserId = userId;
        }

        public static Transfer Create(int transactionId, int destinationBankId, string description, decimal value, DateTime date, int bankAccountId, int userId)
            => new Transfer(transactionId, destinationBankId, description, value, date, bankAccountId, userId);

        /// <summary>
        /// Transfer dont have a category. This void wont set any category for this object
        /// </summary>
        /// <param name="categoryId"></param>
        public override void setCategory(int categoryId)
        {

        }

        public override void setValue(decimal value)
        {
            if (value < 0)
                throw new DomainException(DomainErrorCodes.NegativeTransferValue);

            base.setValue(value);
        }


        //Change Source BankID
        /// <summary>
        /// Throws exception - cant change BankAccount of transfer- use SetSourceBankAccount or SetDestinationBankAccont instead
        /// </summary>
        /// <param name="bankAccountId"></param>
        public override void setBankAccount(int bankAccountId)
            => throw new DomainException(DomainErrorCodes.TransferDontHaveBankId);

        /// <summary>
        /// Throws Exception When Trying to add emptyID, when bankAccountId was not found or when trying to set Source and Destination Bank Id to the same value
        /// </summary>
        /// <param name="bankAccountId"></param>
        public void setSourceBankAccount(int bankAccountId)
        {
            if (bankAccountId.isEmpty())
                throw new DomainException(DomainErrorCodes.EmptyId);

            if (bankAccountId == DestinationBankAccountId)
                throw new DomainException(DomainErrorCodes.SourceAndDestiantionIDCantBeSame);

            if (BankAccountId.isEmpty())
            {
                BankAccountId = bankAccountId;
            }
            else if (BankAccountId != bankAccountId)
            {
                if (SourceBankAccountIDChanedEvent.Invoke(this, bankAccountId))
                {
                    BankAccountId = bankAccountId;
                }
            }
        }

        /// <summary>
        /// Throws Exception When Trying to add emptyID, when bankAccountId was not found or when trying to set Source and Destination Bank Id to the same value
        /// </summary>
        /// <param name="bankAccountId"></param>
        public void setDestinationBankAccount(int bankAccountId)
        {
            if (bankAccountId.isEmpty())
                throw new DomainException(DomainErrorCodes.EmptyId);

            if (bankAccountId == SourceBankAccountId)
                throw new DomainException(DomainErrorCodes.SourceAndDestiantionIDCantBeSame);

            if (DestinationBankAccountId.isEmpty())
            {
                DestinationBankAccountId = bankAccountId;
            }
            else if (DestinationBankAccountId != bankAccountId)
            {
                if (DestinationBankAccountIDChanedEvent.Invoke(this, bankAccountId))
                {
                    DestinationBankAccountId = bankAccountId;
                }
            }
        }

    }
}
