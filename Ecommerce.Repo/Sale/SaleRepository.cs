using Ecommerce.Database.Database;
using Ecommerce.Models.CriteriaDto.Purchase;
using Ecommerce.Models.CriteriaDto.Sale;
using Ecommerce.Models.Entities.Purchase;
using Ecommerce.Models.Entities.Sale;
using Ecommerce.Models.Enums;
using Ecommerce.Repo.Abstraction.Common;
using Ecommerce.Repo.Abstraction.Sale;
using Ecommerce.Repo.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repo.Sale
{
    public class SaleRepository:Repository<Sales>,ISaleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICodeGeneratorRepo _codeGeneratorRepo;

        public SaleRepository(ApplicationDbContext context, ICodeGeneratorRepo codeGeneratorRepo) : base(context)
        {
            _context = context;
            _codeGeneratorRepo = codeGeneratorRepo;
        }

        public override async Task<bool> Add(Sales entity)
        {
            _context.Add(entity);
            entity.Code = await _codeGeneratorRepo.GenerateCode((int)CodeGenerationOperationTypes.Sale);
            return await _context.SaveChangesAsync() > 0;
        }



        public IQueryable<Sales> GetByCriteria(SaleCriteriaDto criteriaDto)
        {
            var data = _context.Sales
                .Include(c=>c.Customer)
                .AsQueryable();



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

        public async Task<Sales> GetById(int Id)
        {
            var result = await _context.Sales.Where(c => c.Id == Id)
                .Include(c => c.SaleDetails)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.SaleUnit)
                .FirstOrDefaultAsync();
            return result;
        }
    }
}
