using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.Common
{
    public interface ICurrentUser
    {
        string UserId { get; }
        string UserName { get; }
        string Email { get; }
        string Mobile { get; }
        string ClientId { get; }
    }
}
