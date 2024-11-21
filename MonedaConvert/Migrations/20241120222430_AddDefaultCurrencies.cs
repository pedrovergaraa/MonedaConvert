using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MonedaConvert.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultCurrencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 1,
                columns: new[] { "IC", "IsDefault", "Legend" },
                values: new object[] { 0.005f, false, "ARS" });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 2,
                columns: new[] { "IC", "Legend", "Symbol" },
                values: new object[] { 1f, "USD", "$" });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 3,
                columns: new[] { "IC", "Legend", "Symbol" },
                values: new object[] { 1.1f, "EUR", "€" });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 4,
                columns: new[] { "IC", "Legend", "Symbol" },
                values: new object[] { 1.3f, "GBP", "£" });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 5,
                columns: new[] { "IC", "Legend", "Symbol" },
                values: new object[] { 0.007f, "JPY", "¥" });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 6,
                columns: new[] { "IC", "Legend" },
                values: new object[] { 0.75f, "CAD" });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "CurrencyId", "IC", "IsDefault", "Legend", "Symbol", "UserId" },
                values: new object[,]
                {
                    { 7, 0.72f, false, "AUD", "$", null },
                    { 8, 1.05f, false, "CHF", "$", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 1,
                columns: new[] { "IC", "IsDefault", "Legend" },
                values: new object[] { 1f, true, "USD" });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 2,
                columns: new[] { "IC", "Legend", "Symbol" },
                values: new object[] { 1.09f, "EUR", "€" });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 3,
                columns: new[] { "IC", "Legend", "Symbol" },
                values: new object[] { 0.8f, "GBP", "£" });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 4,
                columns: new[] { "IC", "Legend", "Symbol" },
                values: new object[] { 110f, "JPY", "¥" });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 5,
                columns: new[] { "IC", "Legend", "Symbol" },
                values: new object[] { 0.043f, "KC", "kc" });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 6,
                columns: new[] { "IC", "Legend" },
                values: new object[] { 0.002f, "ARS" });
        }
    }
}
