using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ecommerce.Configuration.Services;
using Ecommerce.Database.Database;
using Ecommerce.Models.Common;
using Ecommerce.Models.Common.Identity;
using Ecommerce.Repo.Abstraction.Identity;
using Ecommerce.Repo.Identity;
using Ecommerce.Repo.Setup;
using Ecommerce.BLL.Setup;
using Ecommerce.BLL.Abstraction.Setup;
using Ecommerce.Repo.Abstraction.Setup;
using Ecommerce.BLL.Abstraction.Identity;
using Ecommerce.BLL.Identity;
using Ecommerce.BLL.Abstraction.Menu;
using Ecommerce.BLL.Menu;
using Ecommerce.Repo.Abstraction.Menu;
using Ecommerce.Repo.Menu;
using Ecommerce.BLL.Purchase;
using Ecommerce.BLL.Abstraction.Purchase;
using Ecommerce.Repo.Abstraction.Purchase;
using Ecommerce.Repo.Purchase;
using Ecommerce.BLL.Abstraction.Common;
using Ecommerce.BLL.Common;
using Ecommerce.Repo.Common;
using Ecommerce.Repo.Abstraction.Common;
using Ecommerce.Repo.Abstraction.Inventory;
using Ecommerce.Repo.Inventory;
using Ecommerce.BLL.Abstraction.Inventory;
using Ecommerce.BLL.Inventory;
using Ecommerce.BLL.Abstraction.Sale;
using Ecommerce.BLL.Sale;
using Ecommerce.Repo.Sale;
using Ecommerce.Repo.Abstraction.Sale;

namespace Ecommerce.Configuration
{
    public static class ConfigureServices
    {
        public static void Configure(IServiceCollection services, IConfiguration configruation)
        {
            //DbConnection
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configruation.GetConnectionString("DefaultConnection")));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUser, CurrentUserService>();
            services.AddScoped<IDateTime, DateTimeService>();
            
            //Service
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductManager, ProductManager>();

            //Unit
            services.AddTransient<IUnitRepository, UnitRepository>();
            services.AddTransient<IUnitManager, UnitManager>();

            //ItemType
            services.AddTransient<IItemTypeRepository, ItemTypeRepository>();
            services.AddTransient<IItemTypeManager, ItemTypeManager>();

            //Branch
            services.AddTransient<IBranchRepository, BranchRepository>();
            services.AddTransient<IBranchManager, BranchManager>();

            //Bank
            services.AddTransient<IBankRepository, BankRepository>();
            services.AddTransient<IBankManager, BankManager>();

            //Supplier
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<ISupplierManager, SupplierManager>();

            //Customer
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerManager, CustomerManager>();

            //User
            services.AddTransient<IUserRoleManager, UserRoleManager>();
            services.AddTransient<IUserRoleRepo, UserRoleRepo>();

            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IUserRepository, UserRepository>();

            
            //Role
            services.AddTransient<IRoleMenuManager, RoleMenuManager>();
            services.AddTransient<IRoleMenuRepo, RoleMenuRepo>();

            //Menu
            services.AddTransient<IMenuManager, MenuManager>();
            services.AddTransient<IMenuRepository, MenuRepository>();

            //Purchase
            services.AddTransient<IPurchaseManager, PurchaseManager>();
            services.AddTransient<IPurchaseRepository, PurchaseRepository>();

            services.AddTransient<IPurchaseDetailRepository, PurchaseDetailRepository>();

            //Sale
            services.AddTransient<ISaleManager, SaleManager>();
            services.AddTransient<ISaleRepository, SaleRepository>();

            services.AddTransient<ISaleDetailRepository, SaleDetailRepository>();

            //CodeGeneration
            services.AddTransient<ICodeGenerationService, CodeGenerationService>();
            services.AddTransient<ICodeGeneratorRepo, CodeGeneratorRepo>();

            //Product Inventory
            services.AddTransient<IInventoryManager, InventoryManager>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();
        }
    }
}
