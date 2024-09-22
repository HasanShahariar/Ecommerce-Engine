using Ecommerce.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Entities.Permissions
{
    [Table("ECOMMERCE_PERMISSION_MODULE")]
    public class PermissionModule : AuditableEntity, IEntity, IDeletable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public ICollection<PermissionFeature> PermissionFeatures { get; set; }
        public int? ClientId { get; set; }
        public bool IsSoftDelete { get; set; }

        public bool Delete()
        {
            return IsSoftDelete = true;
        }
    }
}
