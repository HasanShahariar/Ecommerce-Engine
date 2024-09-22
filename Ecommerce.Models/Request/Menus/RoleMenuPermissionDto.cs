using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.Request.Menus
{
    public class RoleMenuPermissionDto
    {
        public RoleMenuPermissionDto()
        {
            MenuIds = new List<long>();
        }

        public string RoleName { get; set; }
        public List<long> MenuIds { get; set; }
    }
}
