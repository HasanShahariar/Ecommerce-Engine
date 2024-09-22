﻿using Ecommerce.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Entities.Setup
{
    [Table("ECOMMERCE_UNIT")]
    public class Unit: AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
    }
}
