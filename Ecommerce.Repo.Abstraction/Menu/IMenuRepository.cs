using Ecommerce.Repo.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repo.Abstraction.Menu
{
    public interface IMenuRepository: IRepository<Ecommerce.Models.Entities.Menues.Menu>
    {
        Task<IList<Ecommerce.Models.Entities.Menues.Menu>> GetAllMenu(string role);
        Task<IList<Models.Entities.Menues.Menu>> GetTopMenu();
        Task<IList<Models.Entities.Menues.Menu>> GetPermitedMenuByRoles(IList<string> roles);
    }
}
