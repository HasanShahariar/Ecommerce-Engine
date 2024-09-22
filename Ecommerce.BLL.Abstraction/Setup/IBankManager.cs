using Ecommerce.BLL.Abstraction.Base;
using Ecommerce.Models.Common.Paging;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Abstraction.Setup
{
    public interface IBankManager : IManager<Bank>
    {
        Task<PagedList<Bank>> GetByCriteria(BankCriteriaDto criteriaDto);
    }
}
