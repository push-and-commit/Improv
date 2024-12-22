using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.People;
using Data.Store;
using Data.Values;
using Data.Enums;

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
            .UseLazyLoadingProxies()
            .EnableSensitiveDataLogging();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { // Ensure that Primary and Foreign keys are well created and well respected. This override was made with the help of ChatGPT
            // Relation Player - Team
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation Team - TrainingRoom
            modelBuilder.Entity<Team>()
                .HasOne(t => t.TrainingRoom)
                .WithOne()
                .HasForeignKey<Team>(t => t.TrainingRoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation TrainingRoom - Shop
            modelBuilder.Entity<TrainingRoom>()
                .HasOne(t => t.Shop)
                .WithOne()
                .HasForeignKey<TrainingRoom>(t => t.ShopId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation Shop - Equipments
            modelBuilder.Entity<Shop>()
                .HasMany(s => s.Equipments)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            // Relation Inventory - Equipments
            modelBuilder.Entity<Inventory>()
                .HasMany(i => i.Equipments)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            // Relation Impro - Inventory
            modelBuilder.Entity<Impro>()
                .HasOne(i => i.Inventory)
                .WithOne()
                .HasForeignKey<Impro>(i => i.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation Performance - Prize
            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Prize)
                .WithOne()
                .HasForeignKey<Performance>(p => p.PrizeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation Performance - Equipments Needed
            modelBuilder.Entity<Performance>()
                .HasMany(p => p.EquipmentNeeded)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Relation Audience - Prize
            modelBuilder.Entity<Audience>()
                .HasOne(a => a.Prize)
                .WithOne()
                .HasForeignKey<Audience>(a => a.PrizeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation Audience - Skills
            modelBuilder.Entity<Audience>()
                .HasMany(a => a.Skills)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            // Relation Skill - CostStat
            modelBuilder.Entity<Skill>()
                .HasMany(s => s.Cost)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Relation PowerStat - Stat
            modelBuilder.Entity<PowerStat>()
                .HasOne(ps => ps.Stat)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            // Relation CostStat - Stat
            modelBuilder.Entity<CostStat>()
                .HasOne(cs => cs.Stat)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            // Relation Player - Skill
            modelBuilder.Entity<Player>()
                .HasMany(p => p.Skills)
                .WithMany(s => s.Players)
                .UsingEntity<Dictionary<string, object>>(
                    "PlayerSkill", // Name of the join table
                    j => j.HasOne<Skill>()
                          .WithMany()
                          .HasForeignKey("SkillId") // Foreign Key to Skill
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Player>()
                          .WithMany()
                          .HasForeignKey("PlayerId") // Foreign Key to Player
                          .OnDelete(DeleteBehavior.Cascade));
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
