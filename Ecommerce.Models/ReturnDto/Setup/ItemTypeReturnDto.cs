﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.ReturnDto.Setup
{
    public class ItemTypeReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int PageCount { get; set; }
        public int? Sl { get; set; }

    }
}
