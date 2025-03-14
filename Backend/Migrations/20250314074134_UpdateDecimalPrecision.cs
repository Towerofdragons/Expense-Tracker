using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDecimalPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Income",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Expense",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Budget",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1001",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54e2fb1a-364e-4f33-9bf0-72b221675af6", new DateTime(2025, 3, 14, 7, 41, 33, 253, DateTimeKind.Utc).AddTicks(732), "AQAAAAIAAYagAAAAEKgI4D2zN+IrHn2CcUC796HeTa0oszSS2t8SQfBHfFtf8KPDq2/1Kej6Zz6iO3jX8w==", "8ee1687d-97a3-49d6-8ce5-cb871da0b15e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1002",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8121bd0f-ba95-4e63-9969-7bdd1aae1a07", new DateTime(2025, 3, 14, 7, 41, 33, 400, DateTimeKind.Utc).AddTicks(3572), "AQAAAAIAAYagAAAAEHW6hMHo3SYmtBBAtakVYy3yDrq1rU4M995Cq82UhAy0ZAcprRAN9ZyLRS9dOVjx5w==", "ab9efb47-35d3-43f2-a44a-3f4874f65fbd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Income",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Expense",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Budget",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1001",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78961dd8-06d1-4af3-a05e-639e6cefdb71", new DateTime(2025, 3, 14, 7, 33, 34, 9, DateTimeKind.Utc).AddTicks(9609), "AQAAAAIAAYagAAAAEGAX12U9B54BCa8+90vau/c8DUYuRxJDq1u5Xp41UCG/Xe/GyMZFd5Zt2VYxUgKkzw==", "315208ee-b083-4b6a-a04c-9b17fcc1fb90" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1002",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0f9c9870-382b-4f56-a445-8ac6d770e80d", new DateTime(2025, 3, 14, 7, 33, 34, 100, DateTimeKind.Utc).AddTicks(4448), "AQAAAAIAAYagAAAAEB92PrUZiz8GFVNWhD5wOwIWPzoz4IJp42UXHpRjc6vWSvhj/djus115ffzsGArCUw==", "309f44cc-d130-4d33-a847-52fbb0062942" });
        }
    }
}
