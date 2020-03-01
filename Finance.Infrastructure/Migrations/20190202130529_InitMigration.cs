using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Finance.Infrastructure.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "bank_account_id_seq");

            migrationBuilder.CreateSequence<int>(
                name: "budget_id_seq");

            migrationBuilder.CreateSequence<int>(
                name: "category_id_seq");

            migrationBuilder.CreateSequence<int>(
                name: "role_id_seq");

            migrationBuilder.CreateSequence<int>(
                name: "transaction_id_seq");

            migrationBuilder.CreateSequence<int>(
                name: "user_id_seq");

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    role_id = table.Column<int>(nullable: false),
                    role_name = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false),
                    email = table.Column<string>(type: "varchar(40)", nullable: false),
                    password = table.Column<string>(type: "varchar(200)", nullable: false),
                    salt = table.Column<string>(type: "varchar(56)", nullable: false),
                    username = table.Column<string>(type: "varchar(20)", nullable: true),
                    role_id = table.Column<int>(nullable: false),
                    firstname = table.Column<string>(type: "varchar(20)", nullable: true),
                    lastname = table.Column<string>(type: "varchar(20)", nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_user_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bank_account",
                columns: table => new
                {
                    bank_account_id = table.Column<int>(nullable: false),
                    user_id = table.Column<int>(nullable: false),
                    account_name = table.Column<string>(type: "varchar(20)", nullable: false),
                    initial_balance = table.Column<decimal>(nullable: false),
                    balance = table.Column<decimal>(nullable: false),
                    currency = table.Column<string>(type: "varchar(3)", nullable: false),
                    hex_color = table.Column<string>(type: "varchar(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bank_account", x => x.bank_account_id);
                    table.ForeignKey(
                        name: "FK_bank_account_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "budget",
                columns: table => new
                {
                    budget_id = table.Column<int>(nullable: false),
                    user_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(type: "varchar(20)", nullable: false),
                    color_hex = table.Column<string>(type: "varchar(7)", nullable: false),
                    icon_name = table.Column<string>(type: "varchar(20)", nullable: false),
                    budget_limit = table.Column<decimal>(nullable: false),
                    actual_expanditure = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_budget", x => x.budget_id);
                    table.ForeignKey(
                        name: "FK_budget_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    category_id = table.Column<int>(nullable: false),
                    parent_id = table.Column<int>(nullable: false),
                    user_id = table.Column<int>(nullable: false),
                    icon_name = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    color_hex = table.Column<string>(type: "varchar", maxLength: 7, nullable: false),
                    name = table.Column<string>(type: "varchar(20)", nullable: false),
                    budget_id = table.Column<int>(nullable: false),
                    is_basic = table.Column<bool>(nullable: false),
                    is_subcategory = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.category_id);
                    table.ForeignKey(
                        name: "FK_category_budget_budget_id",
                        column: x => x.budget_id,
                        principalTable: "budget",
                        principalColumn: "budget_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_category_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    transaction_id = table.Column<int>(nullable: false),
                    bank_account_id = table.Column<int>(nullable: false),
                    user_id = table.Column<int>(nullable: false),
                    description = table.Column<string>(type: "varchar(40)", nullable: true),
                    value_id = table.Column<decimal>(nullable: false),
                    category_id = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    transaction_type = table.Column<string>(nullable: false),
                    destination_bank_account_id = table.Column<int>(nullable: true),
                    source_bank_account_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.transaction_id);
                    table.ForeignKey(
                        name: "FK_transaction_bank_account_bank_account_id",
                        column: x => x.bank_account_id,
                        principalTable: "bank_account",
                        principalColumn: "bank_account_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transaction_category_category_id",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transaction_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bank_account_user_id",
                table: "bank_account",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_budget_user_id",
                table: "budget",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_category_budget_id",
                table: "category",
                column: "budget_id");

            migrationBuilder.CreateIndex(
                name: "IX_category_user_id",
                table: "category",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_bank_account_id",
                table: "transaction",
                column: "bank_account_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_category_id",
                table: "transaction",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_user_id",
                table: "transaction",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_id",
                table: "user",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "bank_account");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "budget");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropSequence(
                name: "bank_account_id_seq");

            migrationBuilder.DropSequence(
                name: "budget_id_seq");

            migrationBuilder.DropSequence(
                name: "category_id_seq");

            migrationBuilder.DropSequence(
                name: "role_id_seq");

            migrationBuilder.DropSequence(
                name: "transaction_id_seq");

            migrationBuilder.DropSequence(
                name: "user_id_seq");
        }
    }
}
