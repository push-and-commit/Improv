using Data.People;
using Data.Enums;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Values
{
    public class Skill : IDisplay
    {
        [Key]
        private int _id;
        private string _name;
        private string _description;
        private SkillTypeEnum _type;
        private int _power;
        private List<CostStat> _cost;
        private List<Player> _players;

        public Skill() { }

        public Skill(string name, string description, SkillTypeEnum type, int power, List<CostStat> cost)
        {
            _name = name;
            _description = description;
            _type = type;
            _power = power;
            _cost = cost;
            _players = new List<Player>();
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public SkillTypeEnum Type { get => _type; set => _type = value; }
        public int Power { get => _power; set => _power = value; }
        public virtual List<CostStat> Cost { get => _cost; set => _cost = value; }
        public virtual List<Player> Players { get => _players; set => _players = value; }

        public void DisplaySelf()
        {
            Console.WriteLine($"Name : {_name}");
            Console.WriteLine($"Description : {_description}");
            Console.WriteLine($"Type : {_type}");
            Console.WriteLine($"Power : {_power}");
            Console.WriteLine("Stats required to use the skill :");
            foreach (CostStat costStat in _cost)
            {
                Console.WriteLine($"{costStat.Cost} points of {costStat.Stat.Name} ");
            }
        }
    }
}
