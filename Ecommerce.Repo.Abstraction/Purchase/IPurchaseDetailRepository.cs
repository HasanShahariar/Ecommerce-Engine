using Ecommerce.Models.Entities.Purchase;
using Ecommerce.Repo.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repo.Abstraction.Purchase
{
    public interface IPurchaseDetailRepository: IRepository<PurchaseDetails>
    {
    }
}
