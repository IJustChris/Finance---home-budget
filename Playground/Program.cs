using Finance.Core.Domain;
using Finance.Infrastructure.Database;
using Finance.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;

namespace Playground
{


    class Program
    {


        static void Main(string[] args)
        {
            string conn = $"Server=localhost; Port=5432; Database=test_Finance; Username=postgres;Password=Passwd!@#";

            //TODO: Empty settings passed
            var _context = new FinanceContext(new SqlDatabaseSettings());
            var usr = User.Create(1, "email@email.pl", "user1", 1, "password", "sdsadasdsad");

            //TODO: Empty settings passed
            using (_context = new FinanceContext(new SqlDatabaseSettings()))
            {
                _context.Users.Add(usr);
                _context.SaveChanges();
            }

        }


        public static void BankChangeTest()
        {
            var user = User.Create(1, "test@email.com", "chris", (int)UserRole.admin, "password123", "1234567891");
            var category = user.CreateCategory(2, "category 1", "test-icon", "#ffffff");

            var bank1 = user.CreateBankAccount(3, "bank account1", 100.23m, "PLN", "fff");
            var bank2 = user.CreateBankAccount(4, "bank account2", 300.50m, "PLN", "fff");


            Console.WriteLine($"Bank 1 ID: {bank1.BanAccountId}\nBank 2 ID: {bank2.BanAccountId}\n");
            Console.WriteLine($"Bank 1 Balance: {bank1.Balance}\nBank 2 Balance: {bank2.Balance}\n");

            var transaction1 = bank1.CreateTransaction(5, "seks", category.CategoryId, -59.50m, DateTime.UtcNow);

            Console.WriteLine("+ New transaction in bank 1");

            Console.WriteLine("-----------------------------------------");

            Console.WriteLine($"Bank 1 Balance: {bank1.Balance}\nBank 2 Balance: {bank2.Balance}\n");

            Console.WriteLine("Transactions in Bank 1:");
            foreach (var t in bank1.Transactions)
            {
                Console.WriteLine($"id: {t.TransactionId}\nDescription: {t.Description}\nBank ID: {t.BankAccountId}\n");
            }

            Console.WriteLine("Transactions in Bank 2:");
            foreach (var t in bank2.Transactions)
            {
                Console.WriteLine($"id: {t.TransactionId}\nDescription: {t.Description}\nBank ID: {t.BankAccountId}\n");
            }

            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Change BankId of transaction");
            transaction1.setBankAccount(bank2.BanAccountId);
            Console.WriteLine($"Bank 1 Balance: {bank1.Balance}\nBank 2 Balance: {bank2.Balance}\n");

            Console.WriteLine("Transactions in Bank 1:");
            foreach (var t in bank1.Transactions)
            {
                Console.WriteLine($"id: {t.TransactionId}\nDescription: {t.Description}\nBank ID: {t.BankAccountId}\n");
            }

            Console.WriteLine("Transactions in Bank 2:");
            foreach (var t in bank2.Transactions)
            {
                Console.WriteLine($"id: {t.TransactionId}\nDescription: {t.Description}\nBank ID: {t.BankAccountId}\n");
            }

            Console.WriteLine("-----------------------------------------");

            Console.WriteLine("Change BankId of transaction");
            transaction1.setBankAccount(bank1.BanAccountId);
            Console.WriteLine($"Bank 1 Balance: {bank1.Balance}\nBank 2 Balance: {bank2.Balance}\n");

            Console.WriteLine("Transactions in Bank 1:");
            foreach (var t in bank1.Transactions)
            {
                Console.WriteLine($"id: {t.TransactionId}\nDescription: {t.Description}\nBank ID: {t.BankAccountId}\n");
            }

            Console.WriteLine("Transactions in Bank 2:");
            foreach (var t in bank2.Transactions)
            {
                Console.WriteLine($"id: {t.TransactionId}\nDescription: {t.Description}\nBank ID: {t.BankAccountId}\n");
            }
        }

        public static void TransferTest()
        {
            var user = User.Create(6, "test@email.com", "chris", (int)UserRole.admin, "password123", "1234567891");
            var category = user.CreateCategory(7, "category 1", "test-icon", "#ffffff");

            var bank1 = user.CreateBankAccount(8, "bank account1", 100m, "PLN", "fff");
            var bank2 = user.CreateBankAccount(9, "bank account2", 100m, "PLN", "fff");
            var bank3 = user.CreateBankAccount(10, "bank account3", 100m, "PLN", "fff");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Bank 1 ID: {bank1.BanAccountId}\nBank 2 ID: {bank2.BanAccountId}\nBank 3 ID: {bank3.BanAccountId}\n");
            Console.WriteLine($"Bank 1 Balance: {bank1.Balance}\nBank 2 Balance: {bank2.Balance}\nBank 3 Balance: {bank3.Balance}\n");
            Console.ForegroundColor = ConsoleColor.White;


            var transfer = bank1.CreateTransfer(11, bank2.BanAccountId, "cos", 50m, DateTime.UtcNow);

            WriteBanksInfo(bank1, bank2, bank3);

            Console.WriteLine("Change Value of Transfer");
            transfer.setValue(10m);
            WriteBanksInfo(bank1, bank2, bank3);


            Console.WriteLine("Change Desctiprion of Transfer");
            transfer.setDescription("Nowy opis");
            WriteBanksInfo(bank1, bank2, bank3);

            Console.WriteLine("Change SourceID of Transfer");
            transfer.setSourceBankAccount(bank3.BanAccountId);
            WriteBanksInfo(bank1, bank2, bank3);

            Console.WriteLine("Change SourceID of Transfer");
            transfer.setSourceBankAccount(bank1.BanAccountId);
            WriteBanksInfo(bank1, bank2, bank3);

            Console.WriteLine("Change SourceID of Transfer");
            transfer.setSourceBankAccount(bank3.BanAccountId);
            WriteBanksInfo(bank1, bank2, bank3);

            Console.WriteLine("Change DestinationID of Transfer");
            transfer.setDestinationBankAccount(bank1.BanAccountId);
            WriteBanksInfo(bank1, bank2, bank3);

            Console.WriteLine("Change DestinationID of Transfer");
            transfer.setDestinationBankAccount(bank2.BanAccountId);
            WriteBanksInfo(bank1, bank2, bank3);

            Console.WriteLine("Change DestinationID of Transfer");
            transfer.setDestinationBankAccount(bank1.BanAccountId);
            WriteBanksInfo(bank1, bank2, bank3);

            Console.WriteLine("Remove Transfer");
            bank1.RemoveTransaction(transfer);
            WriteBanksInfo(bank1, bank2, bank3);



        }

        public static void WriteBanksInfo(BankAccount bank1, BankAccount bank2, BankAccount bank3)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("-----------------------------------------");

            Console.WriteLine($"Bank 1 Balance: {bank1.Balance}\nBank 2 Balance: {bank2.Balance}\nBank 3 Balance: {bank3.Balance}\n");


            Console.WriteLine("Transactions in Bank 1:");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Bank 1 id: {bank1.BanAccountId}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var t in bank1.Transactions)
            {
                var transfer = (Transfer)t;
                Console.WriteLine($"SourceID: {transfer.BankAccountId}\nDestinationID: {transfer.DestinationBankAccountId}\nValue: {transfer.Value}\nDescription: {transfer.Description}\nBank ID: {transfer.BankAccountId}\n");
            }

            Console.WriteLine("Transactions in Bank 2:");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Bank 2 id: {bank2.BanAccountId}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var t in bank2.Transactions)
            {
                var transfer = (Transfer)t;
                Console.WriteLine($"SourceID: {transfer.BankAccountId}\nDestinationID: {transfer.DestinationBankAccountId}\nValue: {transfer.Value}\nDescription: {transfer.Description}\nBank ID: {transfer.BankAccountId}\n");
            }

            Console.WriteLine("Transactions in Bank 3:");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Bank 3 id: {bank3.BanAccountId}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var t in bank3.Transactions)
            {
                var transfer = (Transfer)t;
                Console.WriteLine($"SourceID: {transfer.BankAccountId}\nDestinationID: {transfer.DestinationBankAccountId}\nValue: {transfer.Value}\nDescription: {transfer.Description}\nBank ID: {transfer.BankAccountId}\n");
            }

            Console.WriteLine("-----------------------------------------");

            Console.ForegroundColor = ConsoleColor.White;

        }

    }





}
