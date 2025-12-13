using Microsoft.EntityFrameworkCore;
using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Data.Models.LookupEntities;
using RSMS.Data.Models.Others;
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
        public DbSet<UserHostel> UserHostels { get; set; } = default!;

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
        public DbSet<Inventory> Inventory { get; set; } = default!;

        //Others
        public DbSet<SchoolActivity> SchoolActivities { get; set; } = default!; 
        public DbSet<SchoolHoliday> SchoolHolidays { get; set; } = default!;
        public DbSet<AttendanceType> AttendanceTypes { get; set; } = default!;
        public DbSet<ConsumptionConfig> ConsumptionConfig { get; set; } = default!;
        public DbSet<Notification> Notification { get; set; } = default!;
        public DbSet<NotificationAudit> NotificationAudit { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RolePermission>()
                  .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<UserHostel>(entity =>
            {
                entity.HasKey(uh => uh.Id);

                entity.HasOne(uh => uh.User)
                      .WithMany(u => u.UserHostels)
                      .HasForeignKey(uh => uh.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(uh => uh.Role)
                      .WithMany(r => r.UserRoles)
                      .HasForeignKey(uh => uh.RoleId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(uh => uh.RSHostel)
                      .WithMany()
                      .HasForeignKey(uh => uh.RSHostelId)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.CategoryId);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Grade)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GradeId);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.RSHostel)
                .WithMany(r => r.Students)
                .HasForeignKey(s => s.RSHostelId);
            modelBuilder.Entity<Inventory>()
                .Property(i => i.QuantityInHand)
                .HasComputedColumnSql("[OpeningBalance] + [QuantityReceived] - [QuantityIssued]");

        }
    }
}
