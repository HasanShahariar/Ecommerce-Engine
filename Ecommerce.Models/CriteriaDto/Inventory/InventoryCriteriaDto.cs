using Ecommerce.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.CriteriaDto.Inventory
{
    public class InventoryCriteriaDto
    {
        public InventoryCriteriaDto()
        {
            PageParams = new PageParams();
        }
        public int? ProductId { get; set; }

        public PageParams PageParams
        {
            get; set;
        }
    }
}
