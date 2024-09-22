using Ecommerce.BLL.Abstraction.Sale;
using Ecommerce.BLL.Abstraction.Sale;
using Ecommerce.BLL.Base;
using Ecommerce.Models.Common.Paging;
using Ecommerce.Models.Common;
using Ecommerce.Models.CriteriaDto.Sale;
using Ecommerce.Models.Entities.Sale;
using Ecommerce.Models.Entities.Sale;
using Ecommerce.Repo.Abstraction.Inventory;
using Ecommerce.Repo.Abstraction.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ecommerce.BLL.Sale
{
    public class SaleManager: Manager<Sales>, ISaleManager
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleDetailRepository _saleDetailRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public SaleManager(
            ISaleRepository saleRepository,
            ISaleDetailRepository saleDetailRepository,
            IInventoryRepository inventoryRepository
            ) : base(saleRepository)
        {
            _saleRepository = saleRepository;
            _saleDetailRepository = saleDetailRepository;
            _inventoryRepository = inventoryRepository;
        }


        public async Task<PagedList<Sales>> GetByCriteria(SaleCriteriaDto criteriaDto)
        {
            var data = _saleRepository.GetByCriteria(criteriaDto);

            if (criteriaDto != null)
            {
                var result = await PagedList<Sales>.CreateAsync(data, criteriaDto.PageParams.PageNumber, criteriaDto.PageParams.PageSize);
                return result;
            }
            else
            {
                var totalDataCount = data.Count();

                return new PagedList<Sales>(data.ToList(), data.Count(), 1, totalDataCount);
            }
        }


        public async Task<Sales> GetById(int Id)
        {
            return await _saleRepository.GetById(Id);
        }

        public override async Task<bool> AddAsync(Sales sales)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var result = await _saleRepository.Add(sales);
                    if (!result)
                    {
                        throw new Exception("Failed to add sales");
                    }

                    bool allUpdatesSuccessful = false;

                    allUpdatesSuccessful = await updateProductInventory(sales.SaleDetails.ToList(), true);

                    if (allUpdatesSuccessful)
                    {
                        scope.Complete();
                        return true;
                    }
                    else
                    {
                        throw new Exception("Failed to update inventory");
                    }
                }
                catch (Exception ex)
                {
                    // Log exception if necessary
                    throw new InvalidOperationException("Transaction failed", ex);
                }
            }
        }


        public async Task<bool> updateProductInventory(List<SaleDetail> saleDetailList, bool IsToIncrease)
        {
            bool allUpdatesSuccessful = true;
            foreach (var saleDetail in saleDetailList)
            {
                var productInventory = await _inventoryRepository.GetByProductId(saleDetail.ProductId);
                if (IsToIncrease)
                {
                    productInventory.Quantity += saleDetail.Quantity;
                }
                if (!IsToIncrease)
                {
                    productInventory.Quantity -= saleDetail.Quantity;
                }


                var inventoryUpdateResult = await _inventoryRepository.Update(productInventory);

                if (!inventoryUpdateResult)
                {
                    allUpdatesSuccessful = false;
                    break;
                }
            }
            return allUpdatesSuccessful;
        }

        public override async Task<Result> Update(Sales sale)
        {
            if (sale.SaleDetails != null && sale.SaleDetails.Any())
            {
                var items = sale.SaleDetails;

                List<SaleDetail> addebleitemDetails = new List<SaleDetail>();
                List<SaleDetail> updateableDetails = new List<SaleDetail>();
                List<long> deleteableDetailsIds = new List<long>();

                addebleitemDetails = items.Where(c => c.Id == 0).ToList();

                items = items.Where(c => c.Id > 0).ToList();

                if (items != null && items.Any())
                {
                    var currentIds = items.Select(c => c.Id);
                    var existingDetails = _saleDetailRepository.Get(c => c.SalesId == sale.Id);
                    var existingIds = existingDetails.Select(c => c.Id);

                    if (existingIds.Any())
                    {
                        updateableDetails = items.Where(c => existingIds.Contains(c.Id)).ToList();

                        deleteableDetailsIds = existingIds.Where(c => !currentIds.Contains(c)).ToList();
                    }
                }

                var isUpdate = await _saleRepository.Update(sale);

                if (isUpdate)
                {

                    bool isDetailsAdded = false;
                    bool isDetailsUpdated = false;
                    bool isDetailDeleted = false;
                    if (addebleitemDetails.Any())
                    {
                        isDetailsAdded = await _saleDetailRepository.AddRangeAsync(addebleitemDetails);
                        await updateProductInventory(addebleitemDetails, true);

                    }

                    if (updateableDetails.Any())
                    {

                        foreach (var detail in updateableDetails)
                        {
                            var data = _saleDetailRepository.GetFirstOrDefaultAsNoTracking(c => c.Id == detail.Id);
                            if (detail.Quantity > data.Quantity)
                            {
                                var productInventory = await _inventoryRepository.GetByProductId(detail.ProductId);
                                productInventory.Quantity = (productInventory.Quantity - data.Quantity) + detail.Quantity;
                                var inventoryUpdateResult = await _inventoryRepository.Update(productInventory);
                            }
                            if (detail.Quantity < data.Quantity)
                            {
                                var productInventory = await _inventoryRepository.GetByProductId(detail.ProductId);
                                productInventory.Quantity = (productInventory.Quantity - data.Quantity) + detail.Quantity;
                                var inventoryUpdateResult = await _inventoryRepository.Update(productInventory);
                            }

                        }

                        isDetailsUpdated = await _saleDetailRepository.UpdateRangeAsync(updateableDetails);


                    }
                    if (deleteableDetailsIds.Any())
                    {
                        var deleteableItems = _saleDetailRepository.Get(c => deleteableDetailsIds.Contains(c.Id)).ToList();
                        isDetailDeleted = await _saleDetailRepository.RemoveRangeAsync(deleteableItems);
                        await updateProductInventory(addebleitemDetails, false);

                    }
                    if (isDetailsAdded || isDetailsUpdated || isDetailDeleted)
                    {
                        return Result.Success();
                    }
                }
            }
            return Result.Failure(new[] { "Failed to Update" });
        }
    }
}
