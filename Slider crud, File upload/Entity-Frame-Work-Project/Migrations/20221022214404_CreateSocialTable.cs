using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity_Frame_Work_Project.Migrations
{
    public partial class CreateSocialTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Socials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socials", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 10, 23, 1, 44, 4, 23, DateTimeKind.Local).AddTicks(3246));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2022, 10, 23, 1, 44, 4, 24, DateTimeKind.Local).AddTicks(3747));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2022, 10, 23, 1, 44, 4, 24, DateTimeKind.Local).AddTicks(3777));

            migrationBuilder.InsertData(
                table: "Socials",
                columns: new[] { "Id", "IsDeleted", "Name", "Url" },
                values: new object[,]
                {
                    { 1, false, "Facebook", "https://www.facebook.com/" },
                    { 2, false, "Instagram", "https://www.instagram.com/" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Socials");

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 10, 22, 20, 43, 54, 549, DateTimeKind.Local).AddTicks(5865));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2022, 10, 22, 20, 43, 54, 550, DateTimeKind.Local).AddTicks(8153));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2022, 10, 22, 20, 43, 54, 550, DateTimeKind.Local).AddTicks(8201));
        }
    }
}
