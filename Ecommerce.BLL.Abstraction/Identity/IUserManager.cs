using Ecommerce.BLL.Abstraction.Base;
using Ecommerce.Models.Common.Paging;
using Ecommerce.Models.Entities.Identity;
using Ecommerce.Models.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Abstraction.Identity
{
    public interface IUserManager : IManager<User>
    {
        Task<PagedList<User>> GetByCriteria(UserCriteriaDto criteriaDto);
    }
}
