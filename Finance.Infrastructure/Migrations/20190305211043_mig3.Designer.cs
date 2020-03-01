﻿// <auto-generated />
using System;
using Finance.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Finance.Infrastructure.Migrations
{
    [DbContext(typeof(FinanceContext))]
    [Migration("20190305211043_mig3")]
    partial class mig3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("Relational:Sequence:.bank_account_id_seq", "'bank_account_id_seq', '', '1', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:.budget_id_seq", "'budget_id_seq', '', '1', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:.category_id_seq", "'category_id_seq', '', '1', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:.role_id_seq", "'role_id_seq', '', '1', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:.transaction_id_seq", "'transaction_id_seq', '', '1', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:.user_id_seq", "'user_id_seq', '', '1', '1', '', '', 'Int32', 'False'");

            modelBuilder.Entity("Finance.Core.Domain.BankAccount", b =>
                {
                    b.Property<int>("BanAccountId")
                        .HasColumnName("bank_account_id");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnName("account_name")
                        .HasColumnType("varchar(20)");

                    b.Property<decimal>("Balance")
                        .HasColumnName("balance");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnName("currency")
                        .HasColumnType("varchar(3)");

                    b.Property<string>("HexColor")
                        .IsRequired()
                        .HasColumnName("hex_color")
                        .HasColumnType("varchar(7)");

                    b.Property<decimal>("InitialBalance")
                        .HasColumnName("initial_balance");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("BanAccountId");

                    b.HasIndex("UserId");

                    b.ToTable("bank_account");
                });

            modelBuilder.Entity("Finance.Core.Domain.Budget", b =>
                {
                    b.Property<int>("BudgetId")
                        .HasColumnName("budget_id");

                    b.Property<decimal>("ActualExpenditure")
                        .HasColumnName("actual_expanditure");

                    b.Property<decimal>("BudgetLimit")
                        .HasColumnName("budget_limit");

                    b.Property<string>("ColorHex")
                        .IsRequired()
                        .HasColumnName("color_hex")
                        .HasColumnType("varchar(7)");

                    b.Property<string>("IconName")
                        .IsRequired()
                        .HasColumnName("icon_name")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("BudgetId");

                    b.HasIndex("UserId");

                    b.ToTable("budget");
                });

            modelBuilder.Entity("Finance.Core.Domain.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnName("category_id");

                    b.Property<int?>("BudgetId")
                        .HasColumnName("budget_id");

                    b.Property<string>("ColorHex")
                        .IsRequired()
                        .HasColumnName("color_hex")
                        .HasColumnType("varchar")
                        .HasMaxLength(7);

                    b.Property<string>("IconName")
                        .IsRequired()
                        .HasColumnName("icon_name")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.Property<bool>("IsSubcategory")
                        .HasColumnName("is_subcategory");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("ParentId")
                        .HasColumnName("parent_id");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.Property<bool>("isBasic")
                        .HasColumnName("is_basic");

                    b.HasKey("CategoryId");

                    b.HasIndex("BudgetId");

                    b.HasIndex("UserId");

                    b.ToTable("category");
                });

            modelBuilder.Entity("Finance.Core.Domain.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnName("role_id");

                    b.Property<string>("RoleName")
                        .HasColumnName("role_name")
                        .HasColumnType("varchar(10)");

                    b.HasKey("RoleId");

                    b.ToTable("role");
                });

            modelBuilder.Entity("Finance.Core.Domain.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .HasColumnName("transaction_id");

                    b.Property<int>("BankAccountId")
                        .HasColumnName("bank_account_id");

                    b.Property<int>("CategoryId")
                        .HasColumnName("category_id");

                    b.Property<DateTime>("Date")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("varchar(40)");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.Property<decimal>("Value")
                        .HasColumnName("value_id");

                    b.Property<string>("transaction_type")
                        .IsRequired();

                    b.HasKey("TransactionId");

                    b.HasIndex("BankAccountId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("transaction");

                    b.HasDiscriminator<string>("transaction_type").HasValue("transfer");
                });

            modelBuilder.Entity("Finance.Core.Domain.User", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Firstname")
                        .HasColumnName("firstname")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Lastname")
                        .HasColumnName("lastname")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("varchar(200)");

                    b.Property<int>("RoleId")
                        .HasColumnName("role_id");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnName("salt")
                        .HasColumnType("varchar(56)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at");

                    b.Property<string>("Username")
                        .HasColumnName("username")
                        .HasColumnType("varchar(20)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("user");
                });

            modelBuilder.Entity("Finance.Core.Domain.Transfer", b =>
                {
                    b.HasBaseType("Finance.Core.Domain.Transaction");

                    b.Property<int>("DestinationBankAccountId")
                        .HasColumnName("destination_bank_account_id");

                    b.Property<int>("SourceBankAccountId")
                        .HasColumnName("source_bank_account_id");

                    b.HasDiscriminator().HasValue("Transfer");
                });

            modelBuilder.Entity("Finance.Core.Domain.BankAccount", b =>
                {
                    b.HasOne("Finance.Core.Domain.User")
                        .WithMany("BankAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Finance.Core.Domain.Budget", b =>
                {
                    b.HasOne("Finance.Core.Domain.User")
                        .WithMany("Budgets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Finance.Core.Domain.Category", b =>
                {
                    b.HasOne("Finance.Core.Domain.Budget")
                        .WithMany()
                        .HasForeignKey("BudgetId");

                    b.HasOne("Finance.Core.Domain.User")
                        .WithMany("Categories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Finance.Core.Domain.Transaction", b =>
                {
                    b.HasOne("Finance.Core.Domain.BankAccount")
                        .WithMany("Transactions")
                        .HasForeignKey("BankAccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Finance.Core.Domain.Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Finance.Core.Domain.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Finance.Core.Domain.User", b =>
                {
                    b.HasOne("Finance.Core.Domain.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}