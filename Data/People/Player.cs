using System;
using System.Collections.Generic;
using System.Linq;
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
    public class Player : Impro, IDisplay, ILevel
    {
        [Key]
        private int _id;
        private int _age;
        private List<Skill> _skills;
        private int? _teamId;
        [ForeignKey("TeamId")]
        private Team _team;
        private PlayerTypeEnum _type;
        private bool _isDefault;

        public Player() { }

        public Player(Impro impro, int age, Team team, PlayerTypeEnum type, bool isDefault) : base(impro.Name, impro.Level, impro.Equipments, impro.Stats, impro.Inventory)
        {
            _age = age;
            _skills = new List<Skill>();
            _team = team;
            _type = type;
            _isDefault = isDefault;

            if(team != null) _teamId = team.Id;
        }

        public Player(string name, int level, List<Equipment> equipments, List<PowerStat> stats, Inventory inventory, int age, Team team, PlayerTypeEnum type, bool isDefault) : base(name, level, equipments, stats, inventory)
        {
            _age = age;
            _skills = new List<Skill>();
            _team = team;
            _type = type;
            _isDefault = isDefault;

            if(team != null) _teamId = team.Id;
        }

        public int Id { get => _id; set => _id = value; }
        public int Age { get => _age; set => _age = value; }
        public virtual List<Skill> Skills { get => _skills; set => _skills = value; }
        public int? TeamId { get => _teamId; set => _teamId = value; }
        public virtual Team Team { get => _team; set => _team = value; }
        public PlayerTypeEnum Type { get => _type; set => _type = value; }
        public bool IsDefault { get => _isDefault; set => _isDefault = value; }

        public void DisplaySelf()
        {
            Console.WriteLine($"Name : {this.Name}");
            Console.WriteLine($"Level : {this.Level}");
            Console.WriteLine("Stats :");
            foreach (PowerStat stat in this.Stats)
            {
                Console.WriteLine($"{stat.Power} points of {stat.Stat.Name} ");
            }
            Console.WriteLine("Skills :");
            foreach(Skill skill in this.Skills)
            {
                Console.WriteLine($"Name : {skill.Name}");
                if (skill.Cost.Count > 0)
                {
                    Console.WriteLine("Stats required to use :");
                    foreach (CostStat cost in skill.Cost)
                    {
                        Console.WriteLine($"{cost.Cost} points of {cost.Stat.Name}");
                    }
                }
                Console.WriteLine();
            }
            if (this.Equipments.Count > 0)
            {
                Console.WriteLine($"Equipments : ");
                foreach (Equipment equipment in this.Equipments)
                {
                    Console.WriteLine(equipment.Name);
                }
            }
            Console.WriteLine($"Inventory :");
            this.Inventory.DisplaySelf();
            Console.WriteLine($"Age : {this.Age}");
            Console.WriteLine($"Type : {this.Type}");
            if (this.Team != null)
            {
                Console.WriteLine($"Team : {this.Team.Name}");
            }
        }

        public void LevelUp()
        {
            Console.WriteLine($"{this.Name} levels up !");
            Level++;
            Experience -= ExperienceToLevelUp;
            ExperienceToLevelUp = ExperienceToLevelUp + Convert.ToInt32(Math.Floor(ExperienceToLevelUp * 0.75));
            foreach (PowerStat stat in this.Stats)
            {
                Console.WriteLine($"{stat.Stat.Name} : {stat.Power} -> {stat.Power + 5}");
                stat.Power += 5;
            }
            Inventory.NbItemsMax++;
        }

        public void joinTeam(Team team)
        {
            _team = team;
            team.RecruitPlayer(this);
        }

        public void leaveTeam(Team team)
        {
            _team = null;
            team.Players.Remove(this);
        }

        public void UseConsumable(Equipment equipment)
        {
            foreach (PowerStat stat in equipment.Stats)
            {
                this.Stats.FirstOrDefault(playerStat => playerStat.Stat.Name == stat.Stat.Name).Power += stat.Power;
            }
        }
    }
}
