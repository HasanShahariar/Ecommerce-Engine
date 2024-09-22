using Ecommerce.Database.Database;
using Ecommerce.Models.CriteriaDto.Inventory;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.DbModels.Views;
using Ecommerce.Models.Entities.Inventory;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Repo.Abstraction.Common;
using Ecommerce.Repo.Abstraction.Inventory;
using Ecommerce.Repo.Abstraction.Setup;
using Ecommerce.Repo.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repo.Inventory
{
    public class InventoryRepository: Repository<ProductInventory>, IInventoryRepository
    {
        private readonly ApplicationDbContext _context;


        public InventoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductInventory> GetByProductId(int productId)
        {
            return await _context.ProductInventorys.FirstOrDefaultAsync(c => c.ProductId == productId);
        }

        public IQueryable<VW_Inventory> GetByCriteria(InventoryCriteriaDto criteriaDto)
        {
            var data = _context.VW_Inventorys.AsQueryable();



            if (criteriaDto.ProductId != null)
            {
                data = data.Where(c => c.ProductId == criteriaDto.ProductId);
            }



            if (criteriaDto.PageParams != null)
            {
                if (!string.IsNullOrWhiteSpace(criteriaDto.PageParams.SearchKey))
                {
                    string searchKey = criteriaDto.PageParams.SearchKey.Replace("--", " ").Trim().ToLower();
                    data = data.Where(c => c.Product.ToLower().Contains(searchKey)

                    );
                }
            }

            return data.OrderBy(c => c.Id);
        }



    }

}
