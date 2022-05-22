using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace simple.rss.reader.Migrations
{
    public partial class dbmig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feeds_Config_DataModelId",
                table: "Feeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Config_DataModelId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Config");

            migrationBuilder.DropIndex(
                name: "IX_Items_DataModelId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Feeds_DataModelId",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "DataModelId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DataModelId",
                table: "Feeds");

            migrationBuilder.CreateTable(
                name: "DateConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateConfig", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DateConfig");

            migrationBuilder.AddColumn<int>(
                name: "DataModelId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DataModelId",
                table: "Feeds",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Config",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_DataModelId",
                table: "Items",
                column: "DataModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_DataModelId",
                table: "Feeds",
                column: "DataModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feeds_Config_DataModelId",
                table: "Feeds",
                column: "DataModelId",
                principalTable: "Config",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Config_DataModelId",
                table: "Items",
                column: "DataModelId",
                principalTable: "Config",
                principalColumn: "Id");
        }
    }
}
