﻿using Ecommerce.Database.Database;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Models.Enums;
using Ecommerce.Repo.Abstraction.Common;
using Ecommerce.Repo.Abstraction.Setup;
using Ecommerce.Repo.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repo.Setup
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICodeGeneratorRepo _codeGeneratorRepo;

        public SupplierRepository(ApplicationDbContext context, ICodeGeneratorRepo codeGeneratorRepo) : base(context)
        {
            _context = context;
            _codeGeneratorRepo = codeGeneratorRepo;
        }

        public override async Task<bool> Add(Supplier entity)
        {
            _context.Add(entity);
            entity.Code = await _codeGeneratorRepo.GenerateCode((int)CodeGenerationOperationTypes.Supplier);
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<Supplier> GetByCriteria(SupplierCriteriaDto criteriaDto)
        {
            var data = _context.Suppliers.AsQueryable();



            if (!string.IsNullOrEmpty(criteriaDto.Name))
            {
                data = data.Where(c => c.Name.Contains(criteriaDto.Name.Replace("--", " ").Trim()));
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
                    data = data.Where(c => c.Name.ToLower().Contains(searchKey)

                    );
                }
            }

            return data.OrderBy(c => c.Id);
        }
    }
}
