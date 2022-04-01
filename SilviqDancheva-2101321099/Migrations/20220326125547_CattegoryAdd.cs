using Microsoft.EntityFrameworkCore.Migrations;

namespace SilviqDancheva_2101321099.Migrations
{
    public partial class CattegoryAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "JobAds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Пълно работно време" },
                    { 2, "Почасова" },
                    { 3, "Подходяща за студенти" },
                    { 4, "Търговия и Продажби" },
                    { 5, "Инженери и Техници" },
                    { 6, "Архитектура, Строителство" },
                    { 7, "Маркетинг, Реклама, ПР" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobAds_CategoryId",
                table: "JobAds",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobAds_Categories_CategoryId",
                table: "JobAds",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAds_Categories_CategoryId",
                table: "JobAds");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_JobAds_CategoryId",
                table: "JobAds");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "JobAds");
        }
    }
}
