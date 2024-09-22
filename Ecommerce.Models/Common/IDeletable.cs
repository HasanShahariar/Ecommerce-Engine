using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.Common
{
    public interface IDeletable
    {
        public bool IsSoftDelete { get; set; }
        bool Delete();
    }
}
