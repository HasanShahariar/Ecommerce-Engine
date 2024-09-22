using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Ecommerce.Models.Entities.Identity;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Models.IdentityDto;
using Ecommerce.Models.Request.Setup;
using Ecommerce.Models.ReturnDto.Setup;
using Ecommerce.Models.ReturnDto.Menu;
using Ecommerce.Models.Entities.Menues;
using Ecommerce.Models.Entities.Purchase;
using Ecommerce.Models.ReturnDto.Purchase;
using Ecommerce.Models.Request.Purchase;
using Ecommerce.Models.Entities.Sale;
using Ecommerce.Models.Request.Sale;
using Ecommerce.Models.ReturnDto.Sale;

namespace Ecommerce.Configuration
{
    public class ConfigureAutoMapper : Profile
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ConfigureAutoMapper));
        }

        public ConfigureAutoMapper()
        {
            //user 
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserReturnDto>();

            //Product
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductReturnDto>();

            //ItemType
            CreateMap<ItemTypeCreateDto, ItemType>();
            CreateMap<ItemType, ItemTypeReturnDto>();

            //Branch
            CreateMap<BranchCreateDto, Branch>();
            CreateMap<Branch, BranchReturnDto>();

            //Bank
            CreateMap<BankCreateDto, Bank>();
            CreateMap<Bank, BankReturnDto>();

            //Supplier
            CreateMap<SupplierCreateDto, Supplier>();
            CreateMap<Supplier, SupplierReturnDto>();

            //Customer
            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<Customer, CustomerReturnDto>();

            //Menu
            CreateMap<Menu, MenuReturnDto>()
                        .ForMember(dest => dest.Submenu, opt => opt.MapFrom(src => src.Submenu != null && src.Submenu.Count > 0 ? src.Submenu : null));

            //Unit
            CreateMap<Unit, UnitReturnDto>();

            //Purchase
            CreateMap<PurchaseCreateDto, Purchases>();
            CreateMap<Purchases, PurchaseReturnDto>();

            //PurchaseCreateDto Details
            CreateMap<PurchaseDetailCreateDto, PurchaseDetails>();
            CreateMap<PurchaseDetails, PurchaseDetailReturnDto>();

            //Purchase
            CreateMap<SaleCreateDto, Sales>();
            CreateMap<Sales, SaleReturnDto>();

            //PurchaseCreateDto Details
            CreateMap<SaleDetailCreateDto, SaleDetail>();
            CreateMap<SaleDetail, SaleDetailReturnDto>();

        }

    }
}
