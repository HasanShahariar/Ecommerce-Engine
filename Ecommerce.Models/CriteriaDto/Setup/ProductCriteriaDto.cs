using Ecommerce.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.CriteriaDto.Setup
{
    public class ProductCriteriaDto
    {
        public ProductCriteriaDto()
        {
            PageParams = new PageParams();
        }

        public string? Name { get; set; }
        public string? Code { get; set; }
        public bool? IsEnable { get; set; }

        public PageParams PageParams
        {
            get; set;
        }
    }
}
