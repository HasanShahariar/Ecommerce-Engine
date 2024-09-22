using Ecommerce.BLL.Abstraction.Base;
using Ecommerce.Models.Common.Paging;
using Ecommerce.Models.CriteriaDto.Purchase;
using Ecommerce.Models.CriteriaDto.Sale;
using Ecommerce.Models.Entities.Purchase;
using Ecommerce.Models.Entities.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Abstraction.Sale
{
    public interface ISaleManager:IManager<Sales>
    {
        Task<PagedList<Sales>> GetByCriteria(SaleCriteriaDto criteriaDto);
        Task<Sales> GetById(int Id);
    }
}
