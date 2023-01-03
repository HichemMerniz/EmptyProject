using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addOnDbFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("28ac8d6c-bfd6-4709-bb81-338856d5010d"),
                column: "ConcurrencyStamp",
                value: "acc15360-7ae1-4094-8e02-bb12640cdd43");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b7204ac2-b4d9-4d17-8551-28320cb2560d"),
                column: "ConcurrencyStamp",
                value: "033976f1-1e07-4353-9f98-36f053b0fd01");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0b13b705-3d44-4586-b513-04065b351dad"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84aa8e94-9e5e-4c11-9bd4-bd1bf5528a0b", "AQAAAAEAACcQAAAAEMf+lO3Vg100yRHJawuTTKmjqrDoyr1ZktAH9wJ/cndyr41+KzlLghPuknKVOLyboQ==", "fec26bda-0f60-45f8-9a0d-43b49081cadd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
