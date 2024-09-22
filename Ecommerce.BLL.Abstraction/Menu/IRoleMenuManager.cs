using Ecommerce.BLL.Abstraction.Base;
using Ecommerce.Models.Common;
using Ecommerce.Models.Entities.Menues;
using Ecommerce.Models.Request.Menus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Abstraction.Menu
{
    public interface IRoleMenuManager : IManager<RoleMenu>
    {
        Task<Result> AddOrUpdate(RoleMenuPermissionDto model);
        Task<long[]> GetPermitedMenuIds(string roleName);

        //Task<ICollection<Ecommerce.Models.Entities.Menues.Menu>> GetTopMenu();
        Task<List<int>> GetMenuWieseUsers(long menuId);
    }
}
