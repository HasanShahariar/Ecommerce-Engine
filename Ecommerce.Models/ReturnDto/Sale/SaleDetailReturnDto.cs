using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.ReturnDto.Sale
{
    public class SaleDetailReturnDto
    {
        public int Id { get; set; }  
        public int SaleId { get; set; }  
        public int ProductId { get; set; }  
        public string? ProductSaleUnitName { get; set; }
        public int Quantity { get; set; }  
        public decimal UnitPrice { get; set; }  
        public decimal TotalPrice { get; set; }
        public string ProductName { get; set; }

    }
}
