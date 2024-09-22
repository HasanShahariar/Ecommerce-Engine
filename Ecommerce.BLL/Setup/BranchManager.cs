using Ecommerce.BLL.Abstraction.Setup;
using Ecommerce.BLL.Base;
using Ecommerce.Models.Common.Paging;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Repo.Abstraction.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Setup
{
    public class BranchManager : Manager<Branch>, IBranchManager
    {
        private readonly IBranchRepository _branchRepository;

        public BranchManager(IBranchRepository branchRepository) : base(branchRepository)
        {
            _branchRepository = branchRepository;
        }


        public async Task<PagedList<Branch>> GetByCriteria(BranchCriteriaDto criteriaDto)
        {
            var data = _branchRepository.GetByCriteria(criteriaDto);

            if (criteriaDto != null)
            {
                var result = await PagedList<Branch>.CreateAsync(data, criteriaDto.PageParams.PageNumber, criteriaDto.PageParams.PageSize);
                return result;
            }
            else
            {
                var totalDataCount = data.Count();

                return new PagedList<Branch>(data.ToList(), data.Count(), 1, totalDataCount);
            }
        }
    }
}
