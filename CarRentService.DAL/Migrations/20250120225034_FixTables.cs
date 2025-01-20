using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Insurances_CarId",
                table: "Insurances",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_Cars_CarId",
                table: "Insurances",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_Cars_CarId",
                table: "Insurances");

            migrationBuilder.DropIndex(
                name: "IX_Insurances_CarId",
                table: "Insurances");
        }
    }
}
