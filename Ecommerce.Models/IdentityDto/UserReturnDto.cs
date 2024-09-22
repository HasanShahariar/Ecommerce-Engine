using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.IdentityDto
{
    public class UserReturnDto
    {
        public long Id { get; set; }
        public long? Sl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name => FirstName + " " + LastName;
        public string Image { get; set; }
        public string Designation { get; set; }
        public string Phone { get; set; }
        public string About { get; set; }
        public string Address { get; set; }
        
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<string> RoleNames { get; set; }
        public long UserCodePartSequence { get; set; }
        public string UserCode { get; set; }
        [NotMapped]
        public string FirstNameWithLastName => FirstName + ' ' + LastName;
    }
}
