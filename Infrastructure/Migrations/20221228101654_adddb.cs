using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class adddb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("28ac8d6c-bfd6-4709-bb81-338856d5010d"),
                column: "ConcurrencyStamp",
                value: "48d3d9d3-897e-4b8b-93ca-705a4f859d51");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b7204ac2-b4d9-4d17-8551-28320cb2560d"),
                column: "ConcurrencyStamp",
                value: "2a913fe2-c81f-4600-a98e-7404efe4f0da");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0b13b705-3d44-4586-b513-04065b351dad"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a35ce2a4-c292-40bc-80e8-09e69b45d434", "AQAAAAEAACcQAAAAEPhe25Ej3hcfPnJ32tmkg+QiOa0XCjG55ifBeQ7fD/5MiaaOO+VXUNqdqjn6R2nWYw==", "fe05a0a4-dc83-489c-b9eb-ed57d0c10261" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("28ac8d6c-bfd6-4709-bb81-338856d5010d"),
                column: "ConcurrencyStamp",
                value: "f74db2ba-f775-4d6a-9049-27bd7b32a594");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b7204ac2-b4d9-4d17-8551-28320cb2560d"),
                column: "ConcurrencyStamp",
                value: "775770e5-a575-4f86-84b2-f1f38c27a4a5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0b13b705-3d44-4586-b513-04065b351dad"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e16b361-7d92-423d-b0c5-b8e399c32cc7", "AQAAAAEAACcQAAAAEJJxM1SXxjAHWF2T7OT7r89NluemvnFBZB/5WpcmUrUQOSn0xlFZKUgvG+vhrXoayA==", "20262f9f-0386-47fa-9639-ed6ed811e154" });
        }
    }
}
