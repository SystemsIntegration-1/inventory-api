using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inventoryapi.Migrations.ProductDbContextMigrations
{
    /// <inheritdoc />
    public partial class Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableQuantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EntryDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "SharedId",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: false),
                    EntryDate = table.Column<long>(type: "bigint", nullable: false),
                    ExpirationDate = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Batches_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Batches_ProductId",
                table: "Batches",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropColumn(
                name: "SharedId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "AvailableQuantity",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "EntryDate",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ExpirationDate",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
