using Ecommerce.Repo.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Models.Entities.Sale;
using Ecommerce.Models.CriteriaDto.Purchase;
using Ecommerce.Models.Entities.Purchase;
using Ecommerce.Models.CriteriaDto.Sale;

namespace Ecommerce.Repo.Abstraction.Sale
{
    public interface ISaleRepository:IRepository<Sales>
    {
        IQueryable<Sales> GetByCriteria(SaleCriteriaDto criteriaDto);
        Task<Sales> GetById(int Id);
    }
}
