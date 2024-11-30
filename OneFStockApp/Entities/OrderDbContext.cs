using Entities;
using Microsoft.EntityFrameworkCore;

namespace OneFStockApp.Entities
{
    public class OrderDbContext : DbContext
    {
      
        public OrderDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BuyOrder> BuyOrders { get; set; }

        public DbSet<SellOrder> SellOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BuyOrder>().ToTable("Buy_Orders");

            modelBuilder.Entity<SellOrder>().ToTable("Sell_Orders");

            //Seed Data'nın eklenmesi
                
            string BuyOrdersJson = System.IO.File.ReadAllText("mockdata.json");

            List<BuyOrder> BuyOrders = System.Text.Json.JsonSerializer.Deserialize<List<BuyOrder>>(BuyOrdersJson);

            foreach (BuyOrder buyOrder in BuyOrders)
            {
                modelBuilder.Entity<BuyOrder>().HasData(buyOrder);
            }
        }
    }
    
}