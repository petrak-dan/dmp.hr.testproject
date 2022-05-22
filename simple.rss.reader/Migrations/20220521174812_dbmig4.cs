using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace simple.rss.reader.Migrations
{
    public partial class dbmig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Feeds_FeedId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "FeedId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Feeds_FeedId",
                table: "Items",
                column: "FeedId",
                principalTable: "Feeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feeds_Config_DataModelId",
                table: "Feeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Config_DataModelId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Feeds_FeedId",
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

            migrationBuilder.AlterColumn<int>(
                name: "FeedId",
                table: "Items",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Feeds_FeedId",
                table: "Items",
                column: "FeedId",
                principalTable: "Feeds",
                principalColumn: "Id");
        }
    }
}
