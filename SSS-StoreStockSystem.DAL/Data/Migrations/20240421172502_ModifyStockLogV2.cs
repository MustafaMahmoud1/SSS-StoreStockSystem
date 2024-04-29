using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSS_StoreStockSystem.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyStockLogV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_StockLogs",
                table: "StockLogs",
                columns: new[] { "StoreId", "ItemId", "DateTime" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StockLogs",
                table: "StockLogs");
        }
    }
}
