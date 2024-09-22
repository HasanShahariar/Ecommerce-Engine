using Ecommerce.BLL.Abstraction.Setup;
using Ecommerce.BLL.Base;
using Ecommerce.Models.Common.Paging;
using Ecommerce.Models.Common;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Repo.Abstraction.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Repo.Setup;

namespace Ecommerce.BLL.Setup
{
    public class ItemTypeManager : Manager<ItemType>, IItemTypeManager
    {
        private readonly IItemTypeRepository _itemTypeRepository;

        public ItemTypeManager(IItemTypeRepository itemTypeRepository) : base(itemTypeRepository)
        {
            _itemTypeRepository = itemTypeRepository;
        }


        public async Task<PagedList<ItemType>> GetByCriteria(ItemTypeCriteriaDto criteriaDto)
        {
            var data = _itemTypeRepository.GetByCriteria(criteriaDto);

            if (criteriaDto != null)
            {
                var result = await PagedList<ItemType>.CreateAsync(data, criteriaDto.PageParams.PageNumber, criteriaDto.PageParams.PageSize);
                return result;
            }
            else
            {
                var totalDataCount = data.Count();

                return new PagedList<ItemType>(data.ToList(), data.Count(), 1, totalDataCount);
            }
        }
    }
}
