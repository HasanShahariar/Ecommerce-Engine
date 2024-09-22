
using Ecommerce.Models.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Request.Purchase
{
    public class PurchaseCreateDto
    {
        public int? Id { get; set; }
        public string? Code { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int SupplierId { get; set; }
        public DateTime EntryDate { get; set; }
        public string? BatchNumber { get; set; }
        public string? Remarks { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? ShippingCost { get; set; }
        public ICollection<PurchaseDetailCreateDto> PurchaseDetail { get; set; }
    }
}
