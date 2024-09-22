using Ecommerce.Models.CriteriaDto.Purchase;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.Entities.Purchase;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Repo.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Ecommerce.Repo.Abstraction.Purchase
{
    public interface IPurchaseRepository : IRepository<Purchases>
    {
        IQueryable<Purchases> GetByCriteria(PurchaseCriteriaDto criteriaDto);
        Task<Purchases> GetById(int Id);
        
    }
}
