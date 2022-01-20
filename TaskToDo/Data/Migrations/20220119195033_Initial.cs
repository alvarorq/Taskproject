using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskToDo.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDoList",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_USUARIO = table.Column<int>(nullable: false),
                    TASK = table.Column<string>(nullable: false),
                    CREATED = table.Column<DateTime>(nullable: false),
                    LAST_UPD = table.Column<DateTime>(nullable: false),
                    COMPLETED = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoList", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoList");
        }
    }
}
