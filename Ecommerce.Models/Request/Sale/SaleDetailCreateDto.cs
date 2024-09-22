using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Request.Sale
{
    public class SaleDetailCreateDto
    {
        public int Id { get; set; } 
        public int SaleId { get; set; }  
        public int ProductId { get; set; }  
        public int Quantity { get; set; }  
        public decimal UnitPrice { get; set; }  
        public decimal TotalPrice { get; set; }  
    }
}
