using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Values;

namespace Data.Store
{
    public class Equipment
    {
        private int _id;
        private string _name;
        private string _description;
        private int _price;
        private string _type;
        private List<Stat> _stats;
        private int _minLevel;

        public Equipment() { }

        public Equipment(string name, string description, int price, string type, List<Stat> stats, int minLevel)
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
        public string Type { get => _type; set => _type = value; }
        public virtual List<Stat> Stats { get => _stats; set => _stats = value; }
        public int MinLevel { get => _minLevel; set => _minLevel = value; }
    }
}
