﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.ReturnDto.Setup
{
    public class BranchReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int BankId { get; set; }
        public string BankName { get; set; }
        public int? Sl { get; set; }
    }
}
