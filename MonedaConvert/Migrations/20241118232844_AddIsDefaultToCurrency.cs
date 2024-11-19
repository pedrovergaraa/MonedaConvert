using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonedaConvert.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDefaultToCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Users_UserId",
                table: "Currencies");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteCurrencies_Currencies_CurrencyId",
                table: "FavoriteCurrencies");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteCurrencies_Users_UserId",
                table: "FavoriteCurrencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Subscriptions_SubscriptionId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriptionId",
                table: "Users",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true,
                oldDefaultValue: 1);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Currencies",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 1,
                column: "IsDefault",
                value: true);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 2,
                column: "IsDefault",
                value: false);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 3,
                column: "IsDefault",
                value: false);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 4,
                column: "IsDefault",
                value: false);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 5,
                column: "IsDefault",
                value: false);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 6,
                column: "IsDefault",
                value: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Users_UserId",
                table: "Currencies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteCurrencies_Currencies_CurrencyId",
                table: "FavoriteCurrencies",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteCurrencies_Users_UserId",
                table: "FavoriteCurrencies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Subscriptions_SubscriptionId",
                table: "Users",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "SubId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Users_UserId",
                table: "Currencies");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteCurrencies_Currencies_CurrencyId",
                table: "FavoriteCurrencies");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteCurrencies_Users_UserId",
                table: "FavoriteCurrencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Subscriptions_SubscriptionId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Currencies");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriptionId",
                table: "Users",
                type: "INTEGER",
                nullable: true,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Users_UserId",
                table: "Currencies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteCurrencies_Currencies_CurrencyId",
                table: "FavoriteCurrencies",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteCurrencies_Users_UserId",
                table: "FavoriteCurrencies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Subscriptions_SubscriptionId",
                table: "Users",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "SubId");
        }
    }
}
