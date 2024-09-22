using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Database.Migrations
{
    /// <inheritdoc />
    public partial class add_code_generation_operation_Type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            SET IDENTITY_INSERT ECOMMERCE_CODE_GENERATION_OPERATION_TYPE ON;
            MERGE INTO ECOMMERCE_CODE_GENERATION_OPERATION_TYPE AS target
            USING (VALUES
                (1, 'Unit', 'UNIT'),
                (2, 'Bank', 'BANK'),
                (3, 'Branch', 'BRANCH'),
                (4, 'ItemType', 'ITM'),
                (5, 'Supplier', 'SUP'),
                (6, 'Product', 'PROD'),
                (7, 'Sale', 'SAL'),
                (8, 'Purchase', 'PUR'),
                (9, 'WorkOrder', 'WO'),
                (10, 'Production', 'PROD'),
                (11, 'Customer', 'CUST')
            ) AS source (Id, Name, Code)
            ON target.Id = source.Id
            WHEN NOT MATCHED BY TARGET THEN
                INSERT (Id, Name, Code) 
                VALUES (source.Id, source.Name, source.Code);
            SET IDENTITY_INSERT ECOMMERCE_CODE_GENERATION_OPERATION_TYPE OFF;


            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
