using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MonedaConvert.Migrations
{
    /// <inheritdoc />
    public partial class NewIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 8);

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "CurrencyId", "IC", "IsDefault", "Legend", "Symbol", "UserId" },
                values: new object[,]
                {
                    { 11, 0.005f, true, "ARS", "$", null },
                    { 12, 1f, true, "USD", "$", null },
                    { 13, 1.1f, true, "EUR", "€", null },
                    { 14, 1.3f, true, "GBP", "£", null },
                    { 15, 0.007f, true, "JPY", "¥", null },
                    { 16, 0.75f, true, "CAD", "$", null },
                    { 17, 0.72f, true, "AUD", "$", null },
                    { 18, 1.05f, true, "CHF", "$", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 18);

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "CurrencyId", "IC", "IsDefault", "Legend", "Symbol", "UserId" },
                values: new object[,]
                {
                    { 1, 0.005f, true, "ARS", "$", null },
                    { 2, 1f, true, "USD", "$", null },
                    { 3, 1.1f, true, "EUR", "€", null },
                    { 4, 1.3f, true, "GBP", "£", null },
                    { 5, 0.007f, true, "JPY", "¥", null },
                    { 6, 0.75f, true, "CAD", "$", null },
                    { 7, 0.72f, true, "AUD", "$", null },
                    { 8, 1.05f, true, "CHF", "$", null }
                });
        }
    }
}
