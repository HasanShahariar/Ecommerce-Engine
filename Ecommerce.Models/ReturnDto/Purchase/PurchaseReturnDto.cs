using Ecommerce.Models.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.ReturnDto.Purchase
{
    public class PurchaseReturnDto
    {
        public int Id { get; set; }
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
        public int? Sl { get; set; }
        public ICollection<PurchaseDetailReturnDto> PurchaseDetail { get; set; }
    }
}
