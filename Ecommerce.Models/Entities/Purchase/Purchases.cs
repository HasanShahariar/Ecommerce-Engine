using Ecommerce.Models.Common;
using Ecommerce.Models.Entities.Setup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ecommerce.Models.Entities.Purchase
{
    [Table("ECOMMERCE_PURCHASE")]
    public class Purchases : AuditableEntity
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public DateTime EntryDate { get; set; }
        public string? BatchNumber { get; set; }
        public string? Remarks { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? ShippingCost { get; set; }
        public ICollection<PurchaseDetails> PurchaseDetail { get; set; }
    }
}
