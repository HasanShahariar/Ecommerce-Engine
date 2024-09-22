using Ecommerce.BLL.Abstraction.Base;
using Ecommerce.Models.Common.Paging;
using Ecommerce.Models.CriteriaDto.Purchase;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.Entities.Purchase;
using Ecommerce.Models.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Abstraction.Purchase
{
    public interface IPurchaseManager : IManager<Purchases>
    {
        Task<PagedList<Purchases>> GetByCriteria(PurchaseCriteriaDto criteriaDto);
        Task<Purchases> GetById(int Id);
        
    }
}
