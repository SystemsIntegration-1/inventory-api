using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryService.Data.Migrations
{
    /// <inheritdoc />
    public partial class InsertWarehouseData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFilePath = Path.Combine("Persistence/Database/Scripts", "002_InsertTableData.sql");
            var sql = File.ReadAllText(sqlFilePath);
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
