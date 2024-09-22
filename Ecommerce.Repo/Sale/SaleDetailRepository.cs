using Ecommerce.Database.Database;
using Ecommerce.Models.Entities.Sale;
using Ecommerce.Repo.Abstraction.Sale;
using Ecommerce.Repo.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repo.Sale
{
    public class SaleDetailRepository:Repository<SaleDetail>,ISaleDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public SaleDetailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
