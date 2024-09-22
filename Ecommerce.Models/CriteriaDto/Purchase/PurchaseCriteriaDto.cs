using Ecommerce.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.CriteriaDto.Purchase
{
    public class PurchaseCriteriaDto
    {
        public PurchaseCriteriaDto()
        {
            PageParams = new PageParams();
        }
        public string? Code { get; set; }

        public PageParams PageParams
        {
            get; set;
        }
    }
}
