using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity_Frame_Work_Project.Migrations
{
    public partial class CreateBlogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Desc = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Date", "Desc", "Image", "IsDeleted", "Title" },
                values: new object[] { 1, new DateTime(2022, 10, 22, 19, 43, 22, 101, DateTimeKind.Local).AddTicks(8017), "desc1", "blog-feature-img-1.jpg", false, "title1" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Date", "Desc", "Image", "IsDeleted", "Title" },
                values: new object[] { 2, new DateTime(2022, 10, 22, 19, 43, 22, 102, DateTimeKind.Local).AddTicks(3842), "desc2", "blog-feature-img-3.jpg", false, "title2" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Date", "Desc", "Image", "IsDeleted", "Title" },
                values: new object[] { 3, new DateTime(2022, 10, 22, 19, 43, 22, 102, DateTimeKind.Local).AddTicks(3860), "desc3", "blog-feature-img-4.jpg", false, "title3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}
