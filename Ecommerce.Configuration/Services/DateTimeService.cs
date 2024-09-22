using Ecommerce.Models.Common.Identity;
using System;


namespace Ecommerce.Configuration.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
