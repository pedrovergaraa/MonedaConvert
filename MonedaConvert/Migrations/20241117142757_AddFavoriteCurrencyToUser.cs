using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonedaConvert.Migrations
{
    /// <inheritdoc />
    public partial class AddFavoriteCurrencyToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Currencies_Legend_UserId",
                table: "Currencies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Legend_UserId",
                table: "Currencies",
                columns: new[] { "Legend", "UserId" },
                unique: true);
        }
    }
}
