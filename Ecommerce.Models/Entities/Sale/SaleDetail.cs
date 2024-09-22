using Ecommerce.Models.Common;
using Ecommerce.Models.Entities.Setup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Entities.Sale
{
    [Table("ECOMMERCE_SALE_DETAILS")]
    public class SaleDetail : AuditableEntity
    {
        public long Id { get; set; }  // Unique identifier for the sale item
        public long SalesId { get; set; }  // Reference to the sale
        public int ProductId { get; set; }  // Reference to the product sold
        public int Quantity { get; set; }  // Quantity of the product sold
        public decimal UnitPrice { get; set; }  // Price per unit of the product
        public decimal TotalPrice { get; set; }  // Total price for this line item (Quantity * UnitPrice)

        // Navigation properties
        public Sales Sale { get; set; }  // Navigation property to the sale
        public Product Product { get; set; }  // Navigation property to the product
    }
}
