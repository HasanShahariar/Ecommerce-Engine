
using Ecommerce.BLL.Abstraction.Identity;
using Ecommerce.BLL.Base;
using Ecommerce.Models.Common.Paging;
using Ecommerce.Models.Entities.Identity;
using Ecommerce.Models.IdentityDto;
using Ecommerce.Repo.Abstraction.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Identity
{
    public class UserManager : Manager<User>, IUserManager
    {
        private readonly IUserRepository _repositories;
      
        public UserManager(IUserRepository repositories) : base(repositories)
        {
            _repositories = repositories;
        }

       

        public async Task<PagedList<User>> GetByCriteria(UserCriteriaDto criteriaDto)
        {
            var data = _repositories.GetByCriteria(criteriaDto);

            if (criteriaDto != null)
            {
                var result = await PagedList<User>.CreateAsync(data, criteriaDto.PageParams.PageNumber, criteriaDto.PageParams.PageSize);
                return result;
            }
            else
            {
                var totalDataCount = data.Count();

                return new PagedList<User>(data.ToList(), data.Count(), 1, totalDataCount);
            }
        }

      







    }
}
