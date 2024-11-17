using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonedaConvert.Migrations
{
    /// <inheritdoc />
    public partial class AddFavoriteCurrenciesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "FavoriteCurrencies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteCurrencies_CurrencyId",
                table: "FavoriteCurrencies",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteCurrencies_Currencies_CurrencyId",
                table: "FavoriteCurrencies",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteCurrencies_Currencies_CurrencyId",
                table: "FavoriteCurrencies");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteCurrencies_CurrencyId",
                table: "FavoriteCurrencies");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "FavoriteCurrencies");
        }
    }
}
