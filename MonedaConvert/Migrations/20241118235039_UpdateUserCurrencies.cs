using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonedaConvert.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserCurrencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 1,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 2,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 3,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 4,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 5,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 6,
                column: "UserId",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 1,
                column: "UserId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 2,
                column: "UserId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 3,
                column: "UserId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 4,
                column: "UserId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 5,
                column: "UserId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 6,
                column: "UserId",
                value: 0);
        }
    }
}
