using Microsoft.EntityFrameworkCore;
using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Data.Models.LookupEntities;
using RSMS.Data.Models.SecurityEntities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RSMS.Data
{
    public class RsmsDbContext : DbContext
    {
        public RsmsDbContext(DbContextOptions<RsmsDbContext> options) : base(options)
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
        public DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; } = default!;
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

            // 🔹 Composite Keys
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            // 🔹 Relationships
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.CategoryId);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.RSHostel)
                .WithMany(h => h.Students)
                .HasForeignKey(s => s.RSHostel);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Grade)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GradeId);

            modelBuilder.Entity<Staff>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Staff)
                .HasForeignKey(s => s.DepartmentId);

            modelBuilder.Entity<Staff>()
                .HasOne(s => s.Designation)
                .WithMany(d => d.Staff)
                .HasForeignKey(s => s.DesignationId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Staff)
                .WithMany(s => s.Users)
                .HasForeignKey(u => u.StaffId);

            modelBuilder.Entity<PurchaseInvoice>()
                .HasOne(pi => pi.Supplier)
                .WithMany(s => s.PurchaseInvoices)
                .HasForeignKey(pi => pi.SupplierId);

            modelBuilder.Entity<PurchaseInvoiceItem>()
                .HasOne(pii => pii.PurchaseInvoice)
                .WithMany(pi => pi.Items)
                .HasForeignKey(pii => pii.PurchaseId);

            modelBuilder.Entity<PurchaseInvoiceItem>()
                .HasOne(pii => pii.Item)
                .WithMany(i => i.PurchaseInvoiceItems)
                .HasForeignKey(pii => pii.ItemId);

            modelBuilder.Entity<StockLedger>()
                .HasOne(sl => sl.Item)
                .WithMany(i => i.StockLedgers)
                .HasForeignKey(sl => sl.ItemId);

            modelBuilder.Entity<AssetIssue>()
                .HasOne(ai => ai.Student)
                .WithMany(s => s.AssetIssues)
                .HasForeignKey(ai => ai.StudentId);

            modelBuilder.Entity<AssetIssue>()
                .HasOne(ai => ai.Item)
                .WithMany(i => i.AssetIssues)
                .HasForeignKey(ai => ai.ItemId);
        }
    }
}
