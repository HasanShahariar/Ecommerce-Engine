using Bms.Models.Enums;
using Ecommerce.Models.Common;
using Ecommerce.Models.Entities.Order;
using Ecommerce.Models.Entities.Setup;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bms.Models.Entities.Order
{
    [Table("ECOMMERCE_ORDERS")]
    public class Orders : AuditableEntity
    {
        public int Id { get; set; }  
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }  
        public DateTime OrderDate { get; set; } 
        public DateTime? ShippedDate { get; set; }  
        public OrderStatus Status { get; set; } 
        public decimal TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool IsPaymentConfirmed { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingMethod { get; set; }
        public decimal ShippingCost { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
