using Ecommerce.Models.Common;
using Ecommerce.Models.Entities.Menues;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Entities.Permissions
{
    [Table("ECOMMERCE_PERMISSION_FEATURE")]
    public class PermissionFeature : AuditableEntity, IEntity, IDeletable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long PermissionModuleId { get; set; }
        public PermissionModule PermissionModule { get; set; }
        public long? MenuId { get; set; }
        public Menu Menu { get; set; }
        public ICollection<Permission> Permissions { get; set; }
        public int? ClientId { get; set; }
        public bool IsSoftDelete { get; set; }

        public bool Delete()
        {
            return IsSoftDelete = true;
        }
    }
}
