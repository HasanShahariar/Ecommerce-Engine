using Ecommerce.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Entities.Setup
{
    [Table("ECOMMERCE_PRODUCT")]
    public class Product: AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int PurchaseUnitId { get; set; }
        public Unit PurchaseUnit { get; set; }
        public int SaleUnitId { get; set; }
        public Unit SaleUnit { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal? VAT { get; set; }
        public bool IsEnable { get; set; }
        public string? ProductImageUrl { get; set; }
        public string? Details { get; set; }
    }
}
