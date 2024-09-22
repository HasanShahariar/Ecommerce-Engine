using Ecommerce.BLL.Abstraction.Base;
using Ecommerce.BLL.Abstraction.Purchase;
using Ecommerce.BLL.Base;
using Ecommerce.Models.Common;
using Ecommerce.Models.Common.Paging;
using Ecommerce.Models.CriteriaDto.Purchase;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.Entities.Inventory;
using Ecommerce.Models.Entities.Purchase;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Repo.Abstraction.Inventory;
using Ecommerce.Repo.Abstraction.Purchase;
using Ecommerce.Repo.Abstraction.Setup;
using Ecommerce.Repo.Inventory;
using Ecommerce.Repo.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ecommerce.BLL.Purchase
{
    public class PurchaseManager : Manager<Purchases>, IPurchaseManager
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IPurchaseDetailRepository _purchaseDetailRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public PurchaseManager(
            IPurchaseRepository purchaseRepository,
            IPurchaseDetailRepository purchaseDetailRepository,
            IInventoryRepository inventoryRepository
            ) : base(purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
            _purchaseDetailRepository = purchaseDetailRepository;
            _inventoryRepository = inventoryRepository;
        }


        public async Task<PagedList<Purchases>> GetByCriteria(PurchaseCriteriaDto criteriaDto)
        {
            var data = _purchaseRepository.GetByCriteria(criteriaDto);

            if (criteriaDto != null)
            {
                var result = await PagedList<Purchases>.CreateAsync(data, criteriaDto.PageParams.PageNumber, criteriaDto.PageParams.PageSize);
                return result;
            }
            else
            {
                var totalDataCount = data.Count();

                return new PagedList<Purchases>(data.ToList(), data.Count(), 1, totalDataCount);
            }
        }


        public async Task<Purchases> GetById(int Id)
        {
            return await _purchaseRepository.GetById(Id);
        }

        public override async Task<bool> AddAsync(Purchases purchases)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var result = await _purchaseRepository.AddAsync(purchases);
                    if (!result)
                    {
                        throw new Exception("Failed to add purchases");
                    }

                    bool allUpdatesSuccessful = false;

                    allUpdatesSuccessful = await updateProductInventory(purchases.PurchaseDetail.ToList(),true);

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


        public async Task<bool> updateProductInventory(List<PurchaseDetails> purchaseDetailList,bool IsToIncrease)
        {
            bool allUpdatesSuccessful = true;
            foreach (var purchaseDetail in purchaseDetailList)
            {
                var productInventory = await _inventoryRepository.GetByProductId(purchaseDetail.ProductId);
                if (IsToIncrease)
                {
                    productInventory.Quantity += purchaseDetail.Quantity;
                }
                if(!IsToIncrease)
                {
                    productInventory.Quantity -= purchaseDetail.Quantity;
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

        public override async Task<Result> Update(Purchases purchase)
        {
            if (purchase.PurchaseDetail != null && purchase.PurchaseDetail.Any())
            {
                var items = purchase.PurchaseDetail;

                List<PurchaseDetails> addebleitemDetails = new List<PurchaseDetails>();
                List<PurchaseDetails> updateableDetails = new List<PurchaseDetails>();
                List<long> deleteableDetailsIds = new List<long>();

                addebleitemDetails = items.Where(c => c.Id == 0).ToList();

                items = items.Where(c => c.Id > 0).ToList();

                if (items != null && items.Any())
                {
                    var currentIds = items.Select(c => c.Id);
                    var existingDetails = _purchaseDetailRepository.Get(c => c.PurchaseId == purchase.Id);
                    var existingIds = existingDetails.Select(c => c.Id);

                    if (existingIds.Any())
                    {
                        updateableDetails = items.Where(c => existingIds.Contains(c.Id)).ToList();

                        deleteableDetailsIds = existingIds.Where(c => !currentIds.Contains(c)).ToList();
                    }
                }

                var isUpdate = await _purchaseRepository.Update(purchase);

                if (isUpdate)
                {
                   
                    bool isDetailsAdded = false;
                    bool isDetailsUpdated = false;
                    bool isDetailDeleted = false;
                    if (addebleitemDetails.Any())
                    {
                        isDetailsAdded = await _purchaseDetailRepository.AddRangeAsync(addebleitemDetails);
                        await updateProductInventory(addebleitemDetails, true);

                    }

                    if (updateableDetails.Any())
                    {
                        
                        foreach (var detail in updateableDetails)
                        {
                            var data = _purchaseDetailRepository.GetFirstOrDefaultAsNoTracking(c => c.Id == detail.Id);
                            if (detail.Quantity > data.Quantity)
                            {
                                var productInventory = await _inventoryRepository.GetByProductId(detail.ProductId);
                                productInventory.Quantity =  (productInventory.Quantity-data.Quantity) + detail.Quantity;
                                var inventoryUpdateResult = await _inventoryRepository.Update(productInventory);
                            }
                            if (detail.Quantity < data.Quantity)
                            {
                                var productInventory = await _inventoryRepository.GetByProductId(detail.ProductId);
                                productInventory.Quantity =  (productInventory.Quantity-data.Quantity) + detail.Quantity;
                                var inventoryUpdateResult = await _inventoryRepository.Update(productInventory);
                            }

                        }

                        isDetailsUpdated = await _purchaseDetailRepository.UpdateRangeAsync(updateableDetails);


                    }
                    if (deleteableDetailsIds.Any())
                    {
                        var deleteableItems = _purchaseDetailRepository.Get(c => deleteableDetailsIds.Contains(c.Id)).ToList();
                        isDetailDeleted = await _purchaseDetailRepository.RemoveRangeAsync(deleteableItems);
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
