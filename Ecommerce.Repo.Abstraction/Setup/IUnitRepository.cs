using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Repo.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repo.Abstraction.Setup
{
    public interface IUnitRepository : IRepository<Unit>
    {
        IQueryable<Unit> GetByCriteria(UnitCriteriaDto criteriaDto);
    }
}
