using Ecommerce.Database.Database;
using Ecommerce.Models.Entities.Purchase;
using Ecommerce.Repo.Abstraction.Purchase;
using Ecommerce.Repo.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repo.Purchase
{
    public class PurchaseDetailRepository:Repository<PurchaseDetails>, IPurchaseDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public PurchaseDetailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
