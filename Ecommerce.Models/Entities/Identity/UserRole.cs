using Microsoft.AspNetCore.Identity;
using Ecommerce.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.Models.Entities.Identity
{
    public class UserRole : IdentityUserRole<int>, IDeletable
    {
        public bool IsSoftDelete { get; set; }
        [NotMapped]
        public  User User { get; set; }
        [NotMapped]
        public  Role Role { get; set; }
        public int ClientId { get; set; }

        public bool Delete()
        {
            return IsSoftDelete = true;
        }
    }
}
