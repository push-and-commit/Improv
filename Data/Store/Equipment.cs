using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Data.Enums;
using Data.Interfaces;
using Data.Values;

namespace Data.Store
{
    public class Equipment : IDisplay
    {
        [Key]
        private int _id;
        private string _name;
        private string _description;
        private int _price;
        private EquipmentTypeEnum _type;
        private List<PowerStat> _stats;
        private int _minLevel;

        public Equipment() { }

        public Equipment(string name, string description, int price, EquipmentTypeEnum type, List<PowerStat> stats, int minLevel)
        {
            _name = name;
            _description = description;
            _price = price;
            _type = type;
            _stats = stats;
            _minLevel = minLevel;
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int Price { get => _price; set => _price = value; }
        public EquipmentTypeEnum Type { get => _type; set => _type = value; }
        public virtual List<PowerStat> Stats { get => _stats; set => _stats = value; }
        public int MinLevel { get => _minLevel; set => _minLevel = value; }

        public void DisplaySelf()
        {
            Console.WriteLine($"Name : {_name}");
            Console.WriteLine($"Description : {_description}");
            Console.WriteLine($"Price : {_price}");
            Console.WriteLine($"Type : {_type}");
            Console.WriteLine($"Level required to hold the equipment : {_minLevel}");
            Console.WriteLine("Stats :");
            foreach (PowerStat stat in _stats)
            {
                Console.WriteLine($"{stat.Power} points of {stat.Stat.Name} ");
            }
        }
    }
}
