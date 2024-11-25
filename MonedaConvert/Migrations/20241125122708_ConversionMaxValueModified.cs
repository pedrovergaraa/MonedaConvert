﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonedaConvert.Migrations
{
    /// <inheritdoc />
    public partial class ConversionMaxValueModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "SubId",
                keyValue: 3,
                column: "Conversions",
                value: -1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "SubId",
                keyValue: 3,
                column: "Conversions",
                value: 2147483647);
        }
    }
}
