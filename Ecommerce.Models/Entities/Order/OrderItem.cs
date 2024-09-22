using Bms.Models.Entities.Order;
using Ecommerce.Models.Common;
using Ecommerce.Models.Entities.Setup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Entities.Order
{
    [Table("ECOMMERCE_ORDER_ITEM")]
    public class OrderItem : AuditableEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Orders Order { get; set; }  
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } 
        public int Quantity { get; set; }  
        public decimal UnitPrice { get; set; }  
        public decimal TotalPrice => UnitPrice * Quantity;  
        public decimal? Discount { get; set; }
        public decimal? Tax { get; set; }
    }
}
