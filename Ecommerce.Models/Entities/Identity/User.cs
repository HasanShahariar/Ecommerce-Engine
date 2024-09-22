
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

using Microsoft.AspNetCore.Identity;
using Ecommerce.Models.Common;
using Ecommerce.Models.Entities;


namespace Ecommerce.Models.Entities.Identity
{

    public class User : IdentityUser<int>, IAuditableEntity, IDeletable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }
        public long? CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? UpdatedById { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long? DeletedById { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsSoftDelete { get; set; }
        public string? UserCode { get; set; }
        public long UserCodePartSequence { get; set; }
        [NotMapped]
        public virtual ICollection<string> UserRoles { get; set; }


        public bool Delete()
        {
            return IsSoftDelete = true;
        }
    }
}
