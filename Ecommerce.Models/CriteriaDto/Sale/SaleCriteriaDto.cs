using Ecommerce.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.CriteriaDto.Sale
{
    public class SaleCriteriaDto
    {
        public SaleCriteriaDto()
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
