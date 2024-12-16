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

namespace Data.People
{
    public class Player : Impro, IDisplay
    {
        int _age;
        List<Skill> _skills;
        Team _team;
        PlayerTypeEnum _type;

        public Player() { }

        public Player(string name, int level, List<Equipment> equipments, List<PowerStat> stats, Inventory inventory, int age, Team team, PlayerTypeEnum type) : base(name, level, equipments, stats, inventory)
        {
            _age = age;
            _skills = new List<Skill>();
            _team = team;
            _type = type;
        }

        public int Age { get => _age; set => _age = value; }
        public virtual List<Skill> Skills { get => _skills; set => _skills = value; }
        public virtual Team Team { get => _team; set => _team = value; }
        public PlayerTypeEnum Type { get => _type; set => _type = value; }

        public void DisplaySelf()
        {
            Console.WriteLine($"Name : {this.Name}");
            Console.WriteLine($"Level : {this.Level}");
            Console.WriteLine($"Equipments : ");
            foreach (Equipment equipment in this.Equipments)
            {
                Console.WriteLine(equipment.Name);
            }
            Console.WriteLine("Stats :");
            foreach (PowerStat stat in this.Stats)
            {
                Console.WriteLine($"{stat.Power} points of {stat.Stat.Name} ");
            }
            Console.WriteLine($"Inventory :");
            this.Inventory.DisplaySelf();
            Console.WriteLine($"Age : {this.Age}");
            Console.WriteLine($"Type : {this.Type}");
            if (this.Team != null)
            {
                Console.WriteLine($"Team :");
                this.Team.DisplaySelf();
            }
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
    }
}
