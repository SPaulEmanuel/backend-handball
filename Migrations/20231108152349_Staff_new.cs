using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aplicatieHandbal.Migrations
{
    /// <inheritdoc />
    public partial class Staff_new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffID", "ImageUrl", "Name", "Vorname" },
                values: new object[] { new Guid("64a01342-4ffa-41d4-87d2-d0af3ec8127b"), "empty", "dewdew", "dewd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "StaffID",
                keyValue: new Guid("64a01342-4ffa-41d4-87d2-d0af3ec8127b"));
        }
    }
}
