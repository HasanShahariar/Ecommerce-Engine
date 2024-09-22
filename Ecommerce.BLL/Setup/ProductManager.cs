
using Ecommerce.BLL.Abstraction.Setup;
using Ecommerce.BLL.Base;
using Ecommerce.Models.Common;
using Ecommerce.Models.Common.Paging;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.Entities.Inventory;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Models.DbModels.Views;
using Ecommerce.Repo.Abstraction.Base;
using Ecommerce.Repo.Abstraction.Inventory;
using Ecommerce.Repo.Abstraction.Setup;
using Ecommerce.Repo.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Setup
{
    public class ProductManager:Manager<Product>, IProductManager
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public ProductManager(IProductRepository ProductRepository, IInventoryRepository inventoryRepository) : base(ProductRepository)
        {
            _productRepository = ProductRepository;
            _inventoryRepository = inventoryRepository;
        }

        public virtual async Task<Result> Add(Product product)
        {
            bool isAdded = await _productRepository.Add(product);

            if (isAdded)
            {
                var ProductInventory = new ProductInventory
                {
                    ProductId = product.Id,
                    Quantity = 0,
                };
                var result = await _inventoryRepository.Add(ProductInventory);
                if (result)
                {
                    return Result.Success();
                }
            }
            return Result.Failure(new[] { "Unable to save data!" });
        }

        public async Task<Result> DeleteProduct(int ProductId)
        {
            var result = await _productRepository.DeleteProduct(ProductId);
            return result;
        }

        public async Task<PagedList<Product>> GetByCriteria(ProductCriteriaDto criteriaDto)
        {
            var data = _productRepository.GetByCriteria(criteriaDto);

            if (criteriaDto != null)
            {
                var result = await PagedList<Product>.CreateAsync(data, criteriaDto.PageParams.PageNumber, criteriaDto.PageParams.PageSize);
                return result;
            }
            else
            {
                var totalDataCount = data.Count();

                return new PagedList<Product>(data.ToList(), data.Count(), 1, totalDataCount);
            }
        }

        public async Task<PagedList<VW_Product>> GetAllFromVW_Product(ProductCriteriaDto criteriaDto)
        {
            var data = _productRepository.GetAllFromVW_Product(criteriaDto);

            if (criteriaDto != null)
            {
                var result = await PagedList<VW_Product>.CreateAsync(data, criteriaDto.PageParams.PageNumber, criteriaDto.PageParams.PageSize);
                return result;
            }
            else
            {
                var totalDataCount = data.Count();

                return new PagedList<VW_Product>(data.ToList(), data.Count(), 1, totalDataCount);
            }
        }


    }
}
