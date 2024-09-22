using Ecommerce.Models.Common;
using Ecommerce.Models.Entities.Setup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Entities.Purchase
{
    [Table("ECOMMERCE_PURCHASE_DETAILS")]
    public class PurchaseDetails: AuditableEntity
    {
        public long Id { get; set; }
        public Purchases Purchase { get; set; }
        public long PurchaseId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Unit Unit { get; set; }
        public int UnitId { get; set; }
        public float Quantity { get; set; }
        public double Price { get; set; }
        public string? Remarks { get; set; }

    }
}
