using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Database.Migrations
{
    /// <inheritdoc />
    public partial class create_VW_Inventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE OR ALTER VIEW VW_Inventory
            AS
            SELECT pi.Id,
                 pi.ProductId,
                 p.Name Product,
                pi.Quantity StockQty
    
            FROM ECOMMERCE_PRODUCT_INVENTORY pi
                LEFT JOIN ECOMMERCE_PRODUCT p ON pi.ProductId = p.Id
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
