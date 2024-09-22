using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Database.Migrations
{
    /// <inheritdoc />
    public partial class create_VW_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE VIEW VW_Product AS
            select p.*,
            pu.Name PurchaseUnitName,
            su.Name SaleUnitName
            from ECOMMERCE_PRODUCT p
            left join ECOMMERCE_UNIT su on p.PurchaseUnitId=su.Id
            left join ECOMMERCE_UNIT pu on p.PurchaseUnitId=pu.Id
            WHERE p.IsSoftDelete=0;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
