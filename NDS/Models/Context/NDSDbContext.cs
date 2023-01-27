using Microsoft.EntityFrameworkCore;
using NDS.Models.Domain;

namespace NDS.Models.Context
{
    public class NDSDbContext : DbContext
    {
        public NDSDbContext(DbContextOptions<NDSDbContext> option) : base(option)
        {

        }


        #region DB Tables

        public DbSet<Customer> Tbl_Customer { get; set; }

        public DbSet<City> Tbl_City { get; set; }

        public DbSet<Province> Tbl_Province { get; set; }

        public DbSet<Supplier> Tbl_Supplier { get; set; }

        public DbSet<Staff> Tbl_Staff { get; set; }

        public DbSet<StaffSessionReady> Tbl_StaffSessionReady { get; set; }

        public DbSet<Product> Tbl_Product { get; set; }

        public DbSet<Order> Tbl_Order { get; set; }

        public DbSet<OrderDetails> Tbl_OrderDetails { get; set; }

        public DbSet<PublishPlan> Tbl_PublishPlan { get; set; }

        public DbSet<AdminUser> Tbl_AdminUser { get; set; }

        public DbSet<Payment> Tbl_Payment { get; set; }

        public DbSet<Transaction> Tbl_Transaction { get; set; }


        #endregion
    }
}
