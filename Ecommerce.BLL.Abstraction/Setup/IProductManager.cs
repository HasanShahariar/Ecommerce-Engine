using Ecommerce.BLL.Abstraction.Base;
using Ecommerce.Models.Common.Paging;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.DbModels.Views;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Models.DbModels.Views;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Abstraction.Setup
{
    public interface IProductManager:IManager<Product>
    {
        Task<PagedList<Product>> GetByCriteria(ProductCriteriaDto criteriaDto);
        Task<PagedList<VW_Product>> GetAllFromVW_Product(ProductCriteriaDto criteriaDto);
    }
}
