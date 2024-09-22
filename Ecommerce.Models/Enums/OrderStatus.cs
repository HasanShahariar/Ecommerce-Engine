﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bms.Models.Enums
{
    public enum OrderStatus
    {
        Pending=1,
        ReadyToPickup,
        Shipped,
        Delivered,
        Canceled,
        Returned
    }
}
