using Ecommerce.Models.Common;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Models.DbModels.Views;
using Ecommerce.Repo.Abstraction.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repo.Abstraction.Setup
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<Result> DeleteProduct(int productId);
        IQueryable<Product> GetByCriteria(ProductCriteriaDto criteriaDto);
        IQueryable<VW_Product> GetAllFromVW_Product(ProductCriteriaDto criteriaDto);
    }
}
