using Microsoft.EntityFrameworkCore;
using Ecommerce.Database.Database;
using Ecommerce.Models.Common;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Repo.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ecommerce.Repo.Abstraction.Setup;
using Microsoft.Extensions.Logging;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.DbModels.Views;
using Ecommerce.Models.Enums;
using Ecommerce.Repo.Abstraction.Common;

namespace Ecommerce.Repo.Setup
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICodeGeneratorRepo _codeGeneratorRepo;

        public ProductRepository(ApplicationDbContext context, ICodeGeneratorRepo codeGeneratorRepo) : base(context)
        {
            _context = context;
            _codeGeneratorRepo = codeGeneratorRepo;
        }

        public override async Task<bool> Add(Product product)
        {
            _context.Add(product);
            product.Code = await _codeGeneratorRepo.GenerateCode((int)CodeGenerationOperationTypes.Product);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Result> DeleteProduct(int ProductId)
        {
            var result = await _context.Products
                .FirstOrDefaultAsync(c => c.Id == ProductId);

            _context.Remove(result);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public IQueryable<Product> GetByCriteria(ProductCriteriaDto criteriaDto)
        {
            var data = _context.Products.Include(c=>c.PurchaseUnit).AsQueryable();

            

            if (!string.IsNullOrEmpty(criteriaDto.Name))
            {
                data = data.Where(c => c.Name.Contains(criteriaDto.Name.Replace("--", " ").Trim()));
            }
            if (criteriaDto.Code != null)
            {
                data = data.Where(c => c.Code.Contains(criteriaDto.Code.Replace("--", " ").Trim()));
            }
            if (criteriaDto.IsEnable != null)
            {
                data = data.Where(c => c.IsEnable == criteriaDto.IsEnable);
            }



            if (criteriaDto.PageParams != null)
            {
                if (!string.IsNullOrWhiteSpace(criteriaDto.PageParams.SearchKey))
                {
                    string searchKey = criteriaDto.PageParams.SearchKey.Replace("--", " ").Trim().ToLower();
                    data = data.Where(c => c.Name.ToLower().Contains(searchKey)

                    );
                }
            }

            return data.OrderBy(c => c.Id);
        }

        public IQueryable<VW_Product> GetAllFromVW_Product(ProductCriteriaDto criteriaDto)
        {
            var data = _context.VW_Products.AsQueryable();



            if (!string.IsNullOrEmpty(criteriaDto.Name))
            {
                data = data.Where(c => c.Name.Contains(criteriaDto.Name.Replace("--", " ").Trim()));
            }
            if (criteriaDto.Code != null)
            {
                data = data.Where(c => c.Code.Contains(criteriaDto.Code.Replace("--", " ").Trim()));
            }
            if (criteriaDto.IsEnable != null)
            {
                data = data.Where(c => c.IsEnable == criteriaDto.IsEnable);
            }



            if (criteriaDto.PageParams != null)
            {
                if (!string.IsNullOrWhiteSpace(criteriaDto.PageParams.SearchKey))
                {
                    string searchKey = criteriaDto.PageParams.SearchKey.Replace("--", " ").Trim().ToLower();
                    data = data.Where(c => c.Name.ToLower().Contains(searchKey)

                    );
                }
            }

            return data.OrderBy(c => c.Id);
        }


    }
}
