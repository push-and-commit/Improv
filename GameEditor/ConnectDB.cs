using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Store;
using Data.Values;
using Data.People;

namespace GameEditor
{
    public class ConnectDB : DbContext
    {
        public string dbPath;

        public ConnectDB()
        {
            dbPath = "improv.db";
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source ={dbPath}")
            .UseLazyLoadingProxies();

        public DbSet<Equipment> equipments { get; set; }
        public DbSet<Skill> skills { get; set; }
        public DbSet<Stat> stats { get; set; }
        public DbSet<Player> players { get; set; }
        public DbSet<Team> teams { get; set; }
        public DbSet<Audience> audiences { get; set; }
        public DbSet<Performance> performances { get; set; }
        public DbSet<Store> stores { get; set; }
        public DbSet<Inventory> inventories { get; set; }
        public DbSet<TrainingRoom> trainingRooms { get; set; }
        public DbSet<CostStat> costStats { get; set; }
        public DbSet<Prize> prizes { get; set; }
        public DbSet<PowerStat> powerStats { get; set; }
    }
}
