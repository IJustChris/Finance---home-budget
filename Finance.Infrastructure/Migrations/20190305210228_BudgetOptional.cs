using Microsoft.EntityFrameworkCore.Migrations;

namespace Finance.Infrastructure.Migrations
{
    public partial class BudgetOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_category_budget_budget_id",
                table: "category");

            migrationBuilder.DropIndex(
                name: "IX_category_budget_id",
                table: "category");

            migrationBuilder.AddColumn<int>(
                name: "BudgetId1",
                table: "category",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_category_BudgetId1",
                table: "category",
                column: "BudgetId1");

            migrationBuilder.AddForeignKey(
                name: "FK_category_budget_BudgetId1",
                table: "category",
                column: "BudgetId1",
                principalTable: "budget",
                principalColumn: "budget_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_category_budget_BudgetId1",
                table: "category");

            migrationBuilder.DropIndex(
                name: "IX_category_BudgetId1",
                table: "category");

            migrationBuilder.DropColumn(
                name: "BudgetId1",
                table: "category");

            migrationBuilder.CreateIndex(
                name: "IX_category_budget_id",
                table: "category",
                column: "budget_id");

            migrationBuilder.AddForeignKey(
                name: "FK_category_budget_budget_id",
                table: "category",
                column: "budget_id",
                principalTable: "budget",
                principalColumn: "budget_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
