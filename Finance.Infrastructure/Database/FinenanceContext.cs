using Finance.Core.Domain;
using Finance.Infrastructure.Database.Entities_Configurations;
using Finance.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Database
{
    public class FinanceContext : DbContext
    {
        private readonly SqlDatabaseSettings _settings;

        public DbSet<User> Users { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Role> Roles { get; set; }

        public FinanceContext(SqlDatabaseSettings settings) : base()
        {
            _settings = settings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_settings.Test == true)
            {
                optionsBuilder.UseNpgsql(_settings.ConnectionStringTest);
                base.OnConfiguring(optionsBuilder);
                return;
            }

            optionsBuilder.UseNpgsql(_settings.ConnectionString);
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new BankAccountConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new TransactionConfiguration());
            builder.ApplyConfiguration(new BudgetConfiguration());
            builder.ApplyConfiguration(new TransferConfiguration());

            builder.Entity<User>().ToTable("user");
            builder.Entity<BankAccount>().ToTable("bank_account");
            builder.Entity<Category>().ToTable("category");
            builder.Entity<Role>().ToTable("role");
            builder.Entity<Transaction>().ToTable("transaction");
            builder.Entity<Budget>().ToTable("budget");

            builder.HasSequence<int>("user_id_seq")
                .StartsAt(1)
                .IncrementsBy(1);

            builder.HasSequence<int>("bank_account_id_seq")
                .StartsAt(1)
                .IncrementsBy(1);

            builder.HasSequence<int>("category_id_seq")
                .StartsAt(1)
                .IncrementsBy(1);

            builder.HasSequence<int>("role_id_seq")
                .StartsAt(1)
                .IncrementsBy(1);

            builder.HasSequence<int>("transaction_id_seq")
                .StartsAt(1)
                .IncrementsBy(1);

            builder.HasSequence<int>("budget_id_seq")
                .StartsAt(1)
                .IncrementsBy(1);

            builder.Entity<User>()
                .HasMany(x => x.BankAccounts)
                .WithOne()
                .IsRequired();

            builder.Entity<User>()
                .HasMany(x => x.Budgets)
                .WithOne()
                .IsRequired();

            builder.Entity<User>()
                .HasMany(x => x.Categories)
                .WithOne()
                .IsRequired();

            builder.Entity<User>()
                .HasMany(typeof(Transaction))
                .WithOne()
                .IsRequired();

            builder.Entity<BankAccount>()
                .HasMany(x => x.Transactions)
                .WithOne()
                .IsRequired();

            builder.Entity<Category>()
                .HasMany(typeof(Transaction))
                .WithOne()
                .IsRequired();

            builder.Entity<Budget>()
                .HasMany(typeof(Category))
                .WithOne();

            builder.Entity<Transaction>()
                .HasDiscriminator<string>("transaction_type")
                .HasValue("transaction")
                .HasValue("transfer");

            builder.Entity<Role>()
                .HasMany(typeof(User))
                .WithOne();

            base.OnModelCreating(builder);

        }

    }
}
