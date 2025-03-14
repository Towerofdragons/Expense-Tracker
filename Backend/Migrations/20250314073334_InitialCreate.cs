using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1001",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f2c75a30-67b6-4df4-8e20-a5189febde9e", new DateTime(2025, 3, 14, 7, 26, 52, 268, DateTimeKind.Utc).AddTicks(9971), "AQAAAAIAAYagAAAAEBLdIVXYWu05CfS1ZSP4DlzYV5VckGluMTK2GLANTYV8l268oYPKu96kN60sj2QzWA==", "1360dd83-6ef9-46c8-abb0-3edee1c62c51" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1002",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1c0a047a-4031-4aa3-beca-5ce438e2a293", new DateTime(2025, 3, 14, 7, 26, 52, 362, DateTimeKind.Utc).AddTicks(1338), "AQAAAAIAAYagAAAAEPU+bjIjLy8z3nYAzXrcgO0vKWdyW02QwOpo0XUiCSjX91LJIuzpaisG57+CDKKg+g==", "df5810b9-8432-481a-830e-b413d8b6036e" });
        }
    }
}
