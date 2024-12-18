﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Data.Enums;
using Data.Interfaces;
using Data.Store;
using Data.Values;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.People
{
    public class Team : Impro, IDisplay
    {
        [Key]
        private int _id;
        private string _slogan;
        private int _trainingRoomId;
        [ForeignKey("TrainingRoomId")]
        private TrainingRoom _trainingRoom;
        private int _money;
        private List<Player> _players;
        private TeamTypeEnum _type;

        public Team() { }

        public Team(string name, int level, List<Equipment> equipments, List<PowerStat> stats, Inventory inventory, string slogan, int money, TrainingRoom trainingRoom, TeamTypeEnum type) : base(name, level, equipments, stats, inventory)
        {
            _slogan = slogan;
            _money = money;
            _trainingRoom = trainingRoom;
            _players = new List<Player>();
            _type = type;
            Inventory.NbItemsMax = 15;

            _trainingRoomId = trainingRoom.Id;
        }

        public int Id { get => _id; set => _id = value; }
        public string Slogan { get => _slogan; set => _slogan = value; }
        public int TrainingRoomId { get => _trainingRoomId; set => _trainingRoomId = value; }
        public virtual TrainingRoom TrainingRoom { get => _trainingRoom; set => _trainingRoom = value; }
        public int Money { get => _money; set => _money = value; }
        public virtual List<Player> Players { get => _players; set => _players = value; }
        public virtual TeamTypeEnum Type { get => _type; set => _type = value; }

        public void DisplaySelf()
        {
            Console.WriteLine($"Name : {this.Name}");
            Console.WriteLine($"Level : {this.Level}");
            if (this.Equipments.Count > 0)
            {
                Console.WriteLine("Equipments :");
                foreach (Equipment equipment in this.Equipments)
                {
                    Console.WriteLine(equipment.Name);
                }
            }
            else
            {
                Console.WriteLine("No Equipment in this Team");
            }
            Console.WriteLine("Stats :");
            foreach (PowerStat stat in this.Stats)
            {
                Console.WriteLine($"{stat.Power} points of {stat.Stat.Name} ");
            }
            Console.WriteLine($"Inventory :");
            this.Inventory.DisplaySelf();
            Console.WriteLine($"Moto : {this.Slogan}");
            Console.WriteLine($"Improv Coins : {this.Money}");
            Console.WriteLine($"Training Room :");
            this.TrainingRoom.DisplaySelf();

            string type = _type == TeamTypeEnum.Player ? "a human" : "the computer";
            Console.WriteLine($"This team is played by {type}");
        }

        public void RecruitPlayer(Player player)
        {
            Players.Add(player);
        }

        public void SellPlayer(Player player)
        {
            Players.Remove(player);
        }

        public void BuyEquipment(Equipment equipment)
        {
            if (Money >= equipment.Price)
            {
                Money -= equipment.Price;
                Inventory.AddToInventory(equipment);
            }
        }

        public void SellEquipment(Equipment equipment)
        {
            Money += equipment.Price;
            Inventory.RemoveFromInventory(equipment);
        }
    }
}
