namespace Finance.Core.Domain.Exceptions
{
    public static class DomainErrorCodes
    {
        //COMMON
        public static string InvalidDate => "invalid_date";
        public static string NullValue => "null_value";
        public static string EmptyString => "empty_string";
        public static string ValueTooLow => "value_too_low";
        public static string StringTooShort => "string_too_short";
        public static string StringTooLong => "string_too_long";
        public static string EmptyId => "empty_id";



        //USER
        public static string InvalidPassword => "invalid_password";
        public static string InvalidEmail => "invalid_email";
        public static string InvalidUsername => "invalid_username";

        //CATEGORY
        public static string InvalidCategoryName => "invalid_category_name";
        public static string SubcategoryBuisnessLogicError => "subcategory_buisness_logic_error";
        public static string InvalidColor => "invalid_color";
        public static string CategoryNotFound => "category_nfound";
        public static string CategoryInUse => "category_in_use";

        //BANK ACCOUNT
        public static string InvalidBankAccountName => "invalid_bank_account_name";
        public static string BankAccountNotFound => "bank_acc_nfound";
        public static string WrongBankId => "wrong_bankid";


        //TRANSACTION
        public static string TransactionNotFound => "transaction_not_found";
        public static string NegativeTransferValue => "negative_transfer_value";
        public static string TransferNotBelongToThisBank => "transfer_not_belong_to_current_bank";
        public static string TransferDontHaveBankId => "transfer_dont_have_bankID";
        public static string SourceAndDestiantionIDCantBeSame => "same_destination_and_source_bank";


        //BUDGET
        public static string BudgetNotFound => "budget_nfound";
        public static string BudgetEventNotAssignedToAnyUser => "budget_event_not_assign_to_any_user";





    }
}
