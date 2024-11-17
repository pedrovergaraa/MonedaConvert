using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonedaConvert.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurandoSubs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "SubId",
                keyValue: 1,
                column: "Conversions",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "SubId",
                keyValue: 2,
                column: "Conversions",
                value: 100);

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "SubId",
                keyValue: 3,
                column: "Conversions",
                value: 2147483647);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "SubId",
                keyValue: 1,
                column: "Conversions",
                value: 10L);

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "SubId",
                keyValue: 2,
                column: "Conversions",
                value: 100L);

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "SubId",
                keyValue: 3,
                column: "Conversions",
                value: 9223372036854775807L);
        }
    }
}
