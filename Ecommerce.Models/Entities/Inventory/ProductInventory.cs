using Ecommerce.Models.Entities.Setup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Entities.Inventory
{
    [Table("ECOMMERCE_PRODUCT_INVENTORY")]
    public class ProductInventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public float Quantity { get; set; }
    }
}
