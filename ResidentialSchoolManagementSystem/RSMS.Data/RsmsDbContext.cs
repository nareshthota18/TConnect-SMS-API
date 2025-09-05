using Microsoft.EntityFrameworkCore;
using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Data.Models.LookupEntities;
using RSMS.Data.Models.SecurityEntities;

namespace RSMS.Data
{
    public class RSMSDbContext : DbContext
    {
        public RSMSDbContext(DbContextOptions<RSMSDbContext> options) : base(options)
        {
        }

        // Lookup tables
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Grade> Grades { get; set; } = default!;
        public DbSet<RSHostel> RSHostels { get; set; } = default!;
        public DbSet<Department> Departments { get; set; } = default!;
        public DbSet<Designation> Designations { get; set; } = default!;
        public DbSet<ItemType> ItemTypes { get; set; } = default!;
        public DbSet<Supplier> Suppliers { get; set; } = default!;

        // Security tables
        public DbSet<Role> Roles { get; set; } = default!;
        public DbSet<Permission> Permissions { get; set; } = default!;
        public DbSet<RolePermission> RolePermissions { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<UserRole> UserRoles { get; set; } = default!;

        // Core entities
        public DbSet<Staff> Staff { get; set; } = default!;
        public DbSet<Student> Students { get; set; } = default!;
        public DbSet<StudentAttendance> StudentAttendance { get; set; } = default!;
        public DbSet<StaffAttendance> StaffAttendance { get; set; } = default!;

        // Inventory 
        public DbSet<Item> Items { get; set; } = default!;
        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; } = default!;
        public DbSet<PurchaseItem> PurchaseInvoiceItems { get; set; } = default!;
        public DbSet<StockLedger> StockLedgers { get; set; } = default!;
        public DbSet<AssetIssue> AssetIssues { get; set; } = default!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Concurrency token
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var prop = entity.FindProperty("RowVersion");
                if (prop != null)
                {
                    prop.IsConcurrencyToken = true;
                    prop.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAddOrUpdate;
                }
            }
            
        }
    }
}
