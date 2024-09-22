﻿using Ecommerce.Database.Database;
using Ecommerce.Models.Common;
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
    public class ItemTypeRepository : Repository<ItemType>, IItemTypeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICodeGeneratorRepo _codeGeneratorRepo;

        public ItemTypeRepository(ApplicationDbContext context, ICodeGeneratorRepo codeGeneratorRepo) : base(context)
        {
            _context = context;
            _codeGeneratorRepo = codeGeneratorRepo;
        }
        public override async Task<bool> Add(ItemType entity)
        {
            _context.Add(entity);
            entity.Code = await _codeGeneratorRepo.GenerateCode((int)CodeGenerationOperationTypes.ItemType);
            return await _context.SaveChangesAsync() > 0;
        }


        public IQueryable<ItemType> GetByCriteria(ItemTypeCriteriaDto criteriaDto)
        {
            var data = _context.ItemTypes.AsQueryable();



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
