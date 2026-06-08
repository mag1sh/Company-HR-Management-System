using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyHRManagementSystem.Migrations
{
    public partial class AddHistoryRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentHistories_Employees_EmployeeId",
                table: "EmploymentHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_SalaryHistories_Employees_EmployeeId",
                table: "SalaryHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentHistories_Employees_EmployeeId",
                table: "EmploymentHistories",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryHistories_Employees_EmployeeId",
                table: "SalaryHistories",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentHistories_Employees_EmployeeId",
                table: "EmploymentHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_SalaryHistories_Employees_EmployeeId",
                table: "SalaryHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentHistories_Employees_EmployeeId",
                table: "EmploymentHistories",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryHistories_Employees_EmployeeId",
                table: "SalaryHistories",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
