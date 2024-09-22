using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Models.Common;
using Ecommerce.Models.Common.Identity;
using Ecommerce.Models.Entities.Identity;
using Ecommerce.Models.Entities.Permissions;
using Ecommerce.Models.Entities.Setup;
using System.Reflection;
using Ecommerce.Models.Entities.Menues;
using Ecommerce.Models.Entities.Purchase;
using System.Diagnostics.Metrics;
using Ecommerce.Models.DbModels.Views;
using Ecommerce.Models.Entities.CodeGenerator;
using Ecommerce.Models.Entities.Inventory;
using Ecommerce.Models.Entities.Sale;
using Bms.Models.Entities.Order;
using Ecommerce.Models.Entities.Order;

namespace Ecommerce.Database.Database
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
        UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        private readonly IDateTime _dateTime;
        public ICurrentUser CurrentUserService { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUser cureCurrentUserService, IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
            CurrentUserService = cureCurrentUserService;

        }


        //Setup
        public DbSet<Client> Clients { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Unit> Units { get; set; }

        //UserRole
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        //permission 
        public DbSet<PermissionModule> PermissionModules { get; set; }
        public DbSet<PermissionFeature> PermissionFeatures { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RoleFeaturePermission> RoleFeaturePermissions { get; set; }

        //Menu
        public DbSet<RoleMenu> RoleMenus { get; set; }
        public DbSet<Menu> Menus { get; set; }

        //Purchase
        public DbSet<Purchases> Purchase { get; set; }
        public DbSet<PurchaseDetails> PurchaseDetails { get; set; }

        //Sale
        public DbSet<Sales> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }


        //CodeGeneration
        public DbSet<CodeGenerationOperationType> CodeGenerationOperationTypes { get; set; }
        public DbSet<CodeGenerationNumber> CodeGenerationNumbers { get; set; }

        //Inventory
        public DbSet<ProductInventory> ProductInventorys { get; set; }

        //Order
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItem> OrderItems{ get; set; }


        //VW_Product
        public DbSet<VW_Product> VW_Products { get; set; }

        //VW_Inventory
        public DbSet<VW_Inventory> VW_Inventorys { get; set; }

        

        






        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                       

                        entry.Entity.CreatedById = (string.IsNullOrEmpty(CurrentUserService.UserId) || CurrentUserService.UserId == "0") ? entry.Entity.CreatedById == null ? 0 : entry.Entity.CreatedById : long.Parse(CurrentUserService.UserId);
                        entry.Entity.CreatedOn = _dateTime.Now.AddHours(4);
                        //entry.Entity.UpdatedById = (string.IsNullOrEmpty(CurrentUserService.UserId) || CurrentUserService.UserId == "0") ? entry.Entity.CreatedById == null ? 0 : entry.Entity.CreatedById : long.Parse(CurrentUserService.UserId);
                        //entry.Entity.UpdatedOn = _dateTime.Now.AddHours(4);
                        entry.CurrentValues["IsSoftDelete"] = false;

                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedById = string.IsNullOrEmpty(CurrentUserService.UserId) ? 0 : long.Parse(CurrentUserService.UserId);
                        entry.Entity.UpdatedOn = _dateTime.Now.AddHours(4);
                        entry.Property(e => e.CreatedOn).IsModified = false;
                        entry.Property(e => e.CreatedById).IsModified = false;
                        //entry.CurrentValues["IsSoftDelete"] = false;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsSoftDelete"] = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedById = long.Parse(CurrentUserService.UserId);
                        entry.Entity.CreatedOn = _dateTime.Now.AddHours(4);
                        //entry.Entity.UpdatedById = long.Parse(CurrentUserService.UserId);
                        //entry.Entity.UpdatedOn = _dateTime.Now.AddHours(4);
                        entry.CurrentValues["IsSoftDelete"] = false;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedById = long.Parse(CurrentUserService.UserId);
                        entry.Entity.UpdatedOn = _dateTime.Now.AddHours(4);
                        entry.CurrentValues["IsSoftDelete"] = false;
                        entry.Property(e => e.CreatedOn).IsModified = false;
                        entry.Property(e => e.CreatedById).IsModified = false;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsSoftDelete"] = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return base.SaveChanges();
        }

        public int HardDeleteChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<VW_Product>().HasNoKey().ToView("VW_Product");

            modelBuilder.Entity<RoleMenu>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd(); // Configure identity
            });

            modelBuilder.Entity<VW_Product>(e =>
            {
                e.HasNoKey();
                e.ToView("VW_Product");

            });

            modelBuilder.Entity<VW_Inventory>(e =>
            {
                e.HasNoKey();
                e.ToView("VW_Inventory");

            });

            modelBuilder.Entity<VW_Inventory>().Ignore(v => v.Sl);


            //modelBuilder.Entity<UserRole>(userRole =>
            //{
            //    userRole.HasKey(ur => new { ur.UserId });

            //});



            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var method = SetGlobalQueryFilterMethod.MakeGenericMethod(entityType.ClrType);
                    method.Invoke(this, new object[] { modelBuilder });
                }
            }

            modelBuilder.Entity<Product>()
            .HasOne(p => p.PurchaseUnit)
            .WithMany() // Adjust if there's a navigation property in Unit for Products
            .HasForeignKey(p => p.PurchaseUnitId)
            .OnDelete(DeleteBehavior.Restrict); // Change cascade behavior as needed

            modelBuilder.Entity<Product>()
                .HasOne(p => p.SaleUnit)
                .WithMany() // Adjust if there's a navigation property in Unit for Products
                .HasForeignKey(p => p.SaleUnitId)
                .OnDelete(DeleteBehavior.Restrict); // Change cascade behavior as needed

            base.OnModelCreating(modelBuilder);

        }


        private static readonly MethodInfo SetGlobalQueryFilterMethod = typeof(ApplicationDbContext)
        .GetMethod(nameof(SetGlobalQueryFilter), BindingFlags.NonPublic | BindingFlags.Static);

        private static void SetGlobalQueryFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : AuditableEntity
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(e => !e.IsSoftDelete);
        }


    }
}
