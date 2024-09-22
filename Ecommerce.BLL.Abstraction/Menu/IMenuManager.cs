using Ecommerce.BLL.Abstraction.Base;
using System;
using Ecommerce.Models.Entities.Menues;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Abstraction.Menu
{
    public interface IMenuManager: IManager<Ecommerce.Models.Entities.Menues.Menu>
    {
        Task<IList<Ecommerce.Models.Entities.Menues.Menu>> GetMenuList(string role);
        Task<IList<Ecommerce.Models.Entities.Menues.Menu>> GetTopMenu();
        Task<IList<Ecommerce.Models.Entities.Menues.Menu>> GetPermitedMenuByRoles(IList<string> roles);
    }
}
