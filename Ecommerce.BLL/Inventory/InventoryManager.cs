using Ecommerce.BLL.Abstraction.Inventory;
using Ecommerce.BLL.Base;
using Ecommerce.Models.Common.Paging;
using Ecommerce.Models.CriteriaDto.Inventory;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.DbModels.Views;
using Ecommerce.Models.Entities.Inventory;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Repo.Abstraction.Inventory;
using Ecommerce.Repo.Abstraction.Setup;
using Ecommerce.Repo.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Inventory
{
    public class InventoryManager : Manager<ProductInventory>, IInventoryManager
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryManager(IInventoryRepository inventoryRepository) : base(inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<PagedList<VW_Inventory>> GetByCriteria(InventoryCriteriaDto criteriaDto)
        {
            var data = _inventoryRepository.GetByCriteria(criteriaDto);

            if (criteriaDto != null)
            {
                var result = await PagedList<VW_Inventory>.CreateAsync(data, criteriaDto.PageParams.PageNumber, criteriaDto.PageParams.PageSize);
                return result;
            }
            else
            {
                var totalDataCount = data.Count();

                return new PagedList<VW_Inventory>(data.ToList(), data.Count(), 1, totalDataCount);
            }
        }
    }
}
