using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.People;
using Data.Store;
using Data.Values;

namespace Data
{
    public class ConnectDB : DbContext
    {
        public string dbPath;

        public ConnectDB()
        {
            var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", ".."));
            var databaseFolder = Path.Combine(projectRoot, "Data\\bin\\Debug\\net8.0");
            Directory.CreateDirectory(databaseFolder);
            dbPath = Path.Combine(databaseFolder, "improv.db");
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source ={dbPath}")
            .UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { // Ensure that Primary and Foreign keys are well created and well respected
            // Relation Player - Team
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);

            // Relation Team - TrainingRoom
            modelBuilder.Entity<Team>()
                .HasOne(t => t.TrainingRoom)
                .WithOne()
                .HasForeignKey<Team>(t => t.TrainingRoomId);

            // Relation TrainingRoom - Shop
            modelBuilder.Entity<TrainingRoom>()
                .HasOne(t => t.Shop)
                .WithOne()
                .HasForeignKey<TrainingRoom>(t => t.ShopId);

            // Relation Shop - Equipments
            modelBuilder.Entity<Shop>()
                .HasMany(s => s.Equipments)
                .WithOne();

            // Relation Inventory - Equipments
            modelBuilder.Entity<Inventory>()
                .HasMany(i => i.Equipments)
                .WithOne();

            // Relation Impro - Inventory
            modelBuilder.Entity<Impro>()
                .HasOne(i => i.Inventory)
                .WithOne()
                .HasForeignKey<Impro>(i => i.InventoryId);

            // Relation Performance - Prize
            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Prize)
                .WithOne()
                .HasForeignKey<Performance>(p => p.PrizeId);

            // Relation Performance - Equipments Needed
            modelBuilder.Entity<Performance>()
                .HasMany(p => p.EquipmentNeeded)
                .WithOne();

            // Relation Audience - Prize
            modelBuilder.Entity<Audience>()
                .HasOne(a => a.Prize)
                .WithOne()
                .HasForeignKey<Audience>(a => a.PrizeId);

            // Relation Audience - Skills
            modelBuilder.Entity<Audience>()
                .HasMany(a => a.Skills)
                .WithOne();

            // Relation Skill - CostStat
            modelBuilder.Entity<Skill>()
                .HasMany(s => s.Cost)
                .WithOne();

            // Relation PowerStat - Stat
            modelBuilder.Entity<PowerStat>()
                .HasOne(ps => ps.Stat)
                .WithMany();

            // Relation CostStat - Stat
            modelBuilder.Entity<CostStat>()
                .HasOne(cs => cs.Stat)
                .WithMany();
        }

        public DbSet<Equipment> equipments { get; set; }
        public DbSet<Skill> skills { get; set; }
        public DbSet<Stat> stats { get; set; }
        public DbSet<Player> players { get; set; }
        public DbSet<Team> teams { get; set; }
        public DbSet<Audience> audiences { get; set; }
        public DbSet<Performance> performances { get; set; }
        public DbSet<Shop> shops { get; set; }
        public DbSet<Inventory> inventories { get; set; }
        public DbSet<TrainingRoom> trainingRooms { get; set; }
        public DbSet<CostStat> costStats { get; set; }
        public DbSet<Prize> prizes { get; set; }
        public DbSet<PowerStat> powerStats { get; set; }
    }
}
