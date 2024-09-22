using Ecommerce.Models.Common;
using Ecommerce.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models.Entities.Identity
{
    [Table("ECOMMERCE_USER_DEVICE_INFO")]
    public class UserDeviceInfo :  AuditableEntity, IEntity , IDeletable
    {
        public long Id { get; set; }
        public int? ClientId { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public bool IsInActive { get; set; } 
        public string Remarks { get; set; }
        public string DeviceInfo { get; set; }
        public bool IsSoftDelete { get; set; }
        public bool Delete()
        {
            return IsSoftDelete = true;
        }

    }
}
