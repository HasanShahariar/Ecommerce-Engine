using Ecommerce.Database.Database;
using Ecommerce.Models.Common;
using Ecommerce.Models.Entities.Menues;
using Ecommerce.Repo.Abstraction.Menu;
using Ecommerce.Repo.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repo.Menu
{
    public class RoleMenuRepo : Repository<RoleMenu>, IRoleMenuRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly ICurrentUser _currentUser;
        public RoleMenuRepo(ApplicationDbContext db, ICurrentUser currentUser) : base(db)
        {
            _db = db;
            _currentUser = currentUser;
        }

        public async Task<List<int>> GetMenuWieseUsers(long menuId)
        {
            if (menuId <= 0) return null;

            var rolesForMenue = await _db.RoleMenus.Where(c => c.MenuId == menuId).Select(c => c.Role).ToListAsync();
            if (rolesForMenue == null || !rolesForMenue.Any()) return null;

            var roles = await _db.Roles.Where(c=> rolesForMenue.Contains(c.Name)).Select(c=>c.Id).ToListAsync();
            if (roles == null || !roles.Any()) return null;

            return await _db.UserRoles.Where(c => roles.Contains(c.RoleId)).Select(c => c.UserId).ToListAsync();
        }

        public async Task<IList<RoleMenu>> GetPermitedMenuesByClientId(int clientId)
        {
            return await _db.RoleMenus.Where(c => c.ClientId == clientId && c.IsSoftDelete == false).ToListAsync();
        }

        public async Task<long[]> GetPermitedMenuIds(string roleName)
        {
          
            return _db.RoleMenus.Where(c => c.Role == roleName && c.IsSoftDelete == false).Select(c => c.MenuId).ToArray();
        }


    }
}
