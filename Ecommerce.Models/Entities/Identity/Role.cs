using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.Models.Entities.Identity
{
    public class Role : IdentityRole<int>
    {
        public bool IsSoftDelete { get; set; }
    }
}
