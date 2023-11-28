using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseApp.Migrations
{
    /// <inheritdoc />
    public partial class RemovedIsUniquePropertyFromEmployeeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Expenses_EmployeeId",
                table: "Expenses");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_EmployeeId",
                table: "Expenses",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Expenses_EmployeeId",
                table: "Expenses");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_EmployeeId",
                table: "Expenses",
                column: "EmployeeId",
                unique: true);
        }
    }
}
