using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addOnDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

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
                columns: new[] { "Active", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { true, "1e16b361-7d92-423d-b0c5-b8e399c32cc7", "AQAAAAEAACcQAAAAEJJxM1SXxjAHWF2T7OT7r89NluemvnFBZB/5WpcmUrUQOSn0xlFZKUgvG+vhrXoayA==", "20262f9f-0386-47fa-9639-ed6ed811e154" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("28ac8d6c-bfd6-4709-bb81-338856d5010d"),
                column: "ConcurrencyStamp",
                value: "4f3eb436-4ee4-4a99-a435-7b407476d01f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b7204ac2-b4d9-4d17-8551-28320cb2560d"),
                column: "ConcurrencyStamp",
                value: "b8742785-9e9b-463e-aefb-1a3bb968cb30");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0b13b705-3d44-4586-b513-04065b351dad"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b0f96d9-4e59-475a-8b81-823eacd7b9ec", "AQAAAAEAACcQAAAAEIJUWguXD8MN/lSzlM/06whoiYCitXefqxOjDeVVR28DIZOxKquHmJFky0+JEmBXyw==", "6ab22124-e8dc-4c77-9d80-c6a9243e40e9" });
        }
    }
}
