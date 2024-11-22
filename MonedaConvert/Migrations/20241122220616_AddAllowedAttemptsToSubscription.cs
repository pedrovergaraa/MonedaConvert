using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MonedaConvert.Migrations
{
    /// <inheritdoc />
    public partial class AddAllowedAttemptsToSubscription : Migration
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

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "SubId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "SubId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "SubId",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "AllowedAttempts",
                table: "Subscriptions",
                newName: "Conversions");

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "CurrencyId", "IC", "IsDefault", "Legend", "Symbol", "UserId" },
                values: new object[,]
                {
                    { 10, 0.72f, true, "AUD", "$", null },
                    { 11, 1.05f, true, "CHF", "$", null },
                    { 12, 0.005f, true, "ARS", "$", null },
                    { 13, 1f, true, "USD", "$", null },
                    { 14, 1.1f, true, "EUR", "€", null },
                    { 15, 1.3f, true, "GBP", "£", null },
                    { 16, 0.007f, true, "JPY", "¥", null },
                    { 17, 0.75f, true, "CAD", "$", null }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "SubId", "Conversions", "Name", "Price" },
                values: new object[,]
                {
                    { 4, 10, "Free", 0m },
                    { 5, 100, "Trial", 10m },
                    { 6, 2147483647, "Premium", 15m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 10);

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
                table: "Subscriptions",
                keyColumn: "SubId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "SubId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "SubId",
                keyValue: 6);

            migrationBuilder.RenameColumn(
                name: "Conversions",
                table: "Subscriptions",
                newName: "AllowedAttempts");

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "CurrencyId", "IC", "IsDefault", "Legend", "Symbol", "UserId" },
                values: new object[,]
                {
                    { 1, 0.72f, true, "AUD", "$", null },
                    { 2, 1.05f, true, "CHF", "$", null },
                    { 3, 0.005f, true, "ARS", "$", null },
                    { 4, 1f, true, "USD", "$", null },
                    { 5, 1.1f, true, "EUR", "€", null },
                    { 6, 1.3f, true, "GBP", "£", null },
                    { 7, 0.007f, true, "JPY", "¥", null },
                    { 8, 0.75f, true, "CAD", "$", null }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "SubId", "AllowedAttempts", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 10, "Free", 0m },
                    { 2, 100, "Trial", 10m },
                    { 3, 2147483647, "Premium", 15m }
                });
        }
    }
}
