using Ecommerce.Models.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.Request.Role
{
    public class RoleAssignCreateDto
    {
        public int userId { get; set; }
        public List<string> role { get; set; }
    }
}
