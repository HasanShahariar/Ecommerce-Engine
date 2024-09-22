using Ecommerce.Models.CriteriaDto.Inventory;
using Ecommerce.Models.CriteriaDto.Purchase;
using Ecommerce.Models.DbModels.Views;
using Ecommerce.Models.Entities.Inventory;
using Ecommerce.Models.Entities.Purchase;
using Ecommerce.Repo.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repo.Abstraction.Inventory
{
    public interface IInventoryRepository:IRepository<ProductInventory>
    {
        Task<ProductInventory> GetByProductId(int productId);
        IQueryable<VW_Inventory> GetByCriteria(InventoryCriteriaDto criteriaDto);
    }
}
