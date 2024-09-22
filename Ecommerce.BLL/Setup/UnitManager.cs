using Ecommerce.BLL.Abstraction.Setup;
using Ecommerce.BLL.Base;
using Ecommerce.Models.Common.Paging;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Repo.Abstraction.Setup;
using Ecommerce.Repo.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Setup
{
    public class UnitManager : Manager<Unit>, IUnitManager
    {
        private readonly IUnitRepository _unitRepository;

        public UnitManager(IUnitRepository UnitRepository) : base(UnitRepository)
        {
            _unitRepository = UnitRepository;
        }

        public async Task<PagedList<Unit>> GetByCriteria(UnitCriteriaDto criteriaDto)
        {
            var data = _unitRepository.GetByCriteria(criteriaDto);

            if (criteriaDto != null)
            {
                var result = await PagedList<Unit>.CreateAsync(data, criteriaDto.PageParams.PageNumber, criteriaDto.PageParams.PageSize);
                return result;
            }
            else
            {
                var totalDataCount = data.Count();

                return new PagedList<Unit>(data.ToList(), data.Count(), 1, totalDataCount);
            }
        }

    }
}
