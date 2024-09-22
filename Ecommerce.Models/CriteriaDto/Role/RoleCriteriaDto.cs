using Ecommerce.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.CriteriaDto.Role
{
    public class RoleCriteriaDto
    {
        public RoleCriteriaDto()
        {
            PageParams = new PageParams();
        }
        public long Id { get; set; }

        public PageParams PageParams { get; set; }
    }
}
