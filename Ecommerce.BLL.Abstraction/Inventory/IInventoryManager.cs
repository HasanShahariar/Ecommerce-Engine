using Ecommerce.BLL.Abstraction.Base;
using Ecommerce.Models.Common.Paging;
using Ecommerce.Models.CriteriaDto.Inventory;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.DbModels.Views;
using Ecommerce.Models.Entities.Inventory;
using Ecommerce.Models.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Abstraction.Inventory
{
    public interface IInventoryManager:IManager<ProductInventory>
    {
        Task<PagedList<VW_Inventory>> GetByCriteria(InventoryCriteriaDto criteriaDto);
    }
}
