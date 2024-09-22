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
    [Table("ECOMMERCE_SALE")]
    public class Sales : AuditableEntity
    {
        public long Id { get; set; }  // Unique identifier for the sale
        public DateTime SaleDate { get; set; }  // Date and time of the sale
        public string? Code { get; set; }
        public int CustomerId { get; set; }  // Reference to the customer (if applicable)
        public decimal TotalAmount { get; set; }  // Total amount for the sale
        public decimal Discount { get; set; }  // Discount applied (if any)
        public decimal? TaxAmount { get; set; }  // Tax amount applied to the sale
        public decimal NetAmount { get; set; }  // Final amount after discount and tax
        public string? Remarks { get; set; }
        public ICollection<SaleDetail> SaleDetails { get; set; }  // List of items sold in the transaction

        // Navigation properties
        public Customer Customer { get; set; }  // Navigation property to the customer
    }
}
