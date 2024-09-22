using Ecommerce.Database.Database;
using Ecommerce.Models.CriteriaDto.Purchase;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.Entities.Purchase;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Models.Enums;
using Ecommerce.Repo.Abstraction.Common;
using Ecommerce.Repo.Abstraction.Purchase;
using Ecommerce.Repo.Abstraction.Setup;
using Ecommerce.Repo.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repo.Purchase
{
    public class PurchaseRepository : Repository<Purchases>, IPurchaseRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICodeGeneratorRepo _codeGeneratorRepo;

        public PurchaseRepository(ApplicationDbContext context, ICodeGeneratorRepo codeGeneratorRepo) : base(context)
        {
            _context = context;
            _codeGeneratorRepo = codeGeneratorRepo;
        }

        public override async Task<bool> Add(Purchases entity)
        {
            _context.Add(entity);
            entity.Code = await _codeGeneratorRepo.GenerateCode((int)CodeGenerationOperationTypes.Purchase);
            return await _context.SaveChangesAsync() > 0;
        }



        public IQueryable<Purchases> GetByCriteria(PurchaseCriteriaDto criteriaDto)
        {
            var data = _context.Purchase.AsQueryable();



            if (!string.IsNullOrEmpty(criteriaDto.Code))
            {
                data = data.Where(c => c.Code.Contains(criteriaDto.Code.Replace("--", " ").Trim()));
            }
            if (criteriaDto.Code != null)
            {
                data = data.Where(c => c.Code.Contains(criteriaDto.Code.Replace("--", " ").Trim()));
            }


            if (criteriaDto.PageParams != null)
            {
                if (!string.IsNullOrWhiteSpace(criteriaDto.PageParams.SearchKey))
                {
                    string searchKey = criteriaDto.PageParams.SearchKey.Replace("--", " ").Trim().ToLower();
                    data = data.Where(c => c.Code.ToLower().Contains(searchKey)

                    );
                }
            }

            return data.OrderBy(c => c.Id);
        }

        public async Task<Purchases> GetById(int Id)
        {
            var result = await _context.Purchase.Where(c=>c.Id==Id)
                .Include(c => c.PurchaseDetail)
                .ThenInclude(d=>d.Product)
                .ThenInclude(d=>d.PurchaseUnit)
                .FirstOrDefaultAsync();
            return result;
        }
    }
}
