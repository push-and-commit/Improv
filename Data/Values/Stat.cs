using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Values
{
    public class Stat
    {
        private int _id;
        private string _name;
        private string _description;
        private string _type;
        private int _power;

        public Stat() { }

        public Stat(string name, string description, string type, int power)
        {
            _name = name;
            _description = description;
            _type = type;
            Power = power;
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public string Type { get => _type; set => _type = value; }
        public int Power { get => _power; set => _power = value; }
    }
}
