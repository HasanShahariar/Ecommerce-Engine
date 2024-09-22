using Ecommerce.Models.Request.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.ReturnDto.Sale
{
    public class SaleReturnDto
    {
        public int Id { get; set; }  
        public string? Code { get; set; }
        public DateTime SaleDate { get; set; }  
        public int CustomerId { get; set; }  
        public decimal TotalAmount { get; set; }  
        public decimal Discount { get; set; }  
        public decimal? TaxAmount { get; set; } 
        public decimal NetAmount { get; set; } 
        public string? Remarks { get; set; }
        public string CustomerName { get; set; }
        public int Sl { get; set; }
        public ICollection<SaleDetailReturnDto> SaleDetails { get; set; }

    }
}
