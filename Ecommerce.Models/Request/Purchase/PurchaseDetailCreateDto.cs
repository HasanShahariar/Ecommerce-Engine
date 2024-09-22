using Ecommerce.Models.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Request.Purchase
{
    public class PurchaseDetailCreateDto
    {
        public long Id { get; set; }
        public long PurchaseId { get; set; }
        public int ProductId { get; set; }
        public int UnitId { get; set; }
        public float Quantity { get; set; }
        public double Price { get; set; }
        public string? Remarks { get; set; }
    }
}
