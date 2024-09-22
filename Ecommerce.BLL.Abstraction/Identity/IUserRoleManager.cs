using Ecommerce.BLL.Abstraction.Base;
using Ecommerce.Models.Common;
using Ecommerce.Models.Entities.Identity;
using Ecommerce.Models.Request.Role;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Abstraction.Identity
{
    public interface IUserRoleManager : IManager<UserRole>
    {
        Task<Result> ClientWiseRoleAssign(int userId, string roleName, int clientId);
        Task<Result> RoleAssign(RoleAssignCreateDto roleAssign);
        Task<IList<int>> GetUserIdsByRole(string role);
    }
}
