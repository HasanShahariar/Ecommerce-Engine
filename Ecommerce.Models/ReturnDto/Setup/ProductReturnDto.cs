using Ecommerce.Models.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.ReturnDto.Setup
{
    public class ProductReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int PurchaseUnitId { get; set; }
        public string? PurchaseUnitName { get; set; }
        public int SaleUnitId { get; set; }
        public string? SaleUnitName { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal? VAT { get; set; }
        public bool IsEnable { get; set; }
        public string? ProductImageUrl { get; set; }
        public string? Details { get; set; }
        public int? Sl { get; set; }
    }
}
