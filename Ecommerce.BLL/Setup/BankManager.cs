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
    public class BankManager : Manager<Bank>, IBankManager
    {
        private readonly IBankRepository _bankRepository;

        public BankManager(IBankRepository bankRepository) : base(bankRepository)
        {
            _bankRepository = bankRepository;
        }


        public async Task<PagedList<Bank>> GetByCriteria(BankCriteriaDto criteriaDto)
        {
            var data = _bankRepository.GetByCriteria(criteriaDto);

            if (criteriaDto != null)
            {
                var result = await PagedList<Bank>.CreateAsync(data, criteriaDto.PageParams.PageNumber, criteriaDto.PageParams.PageSize);
                return result;
            }
            else
            {
                var totalDataCount = data.Count();

                return new PagedList<Bank>(data.ToList(), data.Count(), 1, totalDataCount);
            }
        }
    }
}
