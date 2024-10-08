﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.Common.Identity
{
     public class NotFoundException: Exception
    {
        public NotFoundException(string name, object key)
           : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
