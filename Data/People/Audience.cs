using Data.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.People
{
    public class Audience
    {
        private int _id;
        private string _name;
        private string _description;
        private int _level;
        private int _quantity;
        private int _prize;
        private List<Skill> _skills;

        public Audience() { }

        public Audience(string name, string description, int level, int quantity, int prize)
        {
            _name = name;
            _description = description;
            _level = level;
            _quantity = quantity;
            _prize = prize;
            _skills = new List<Skill>();
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int Level { get => _level; set => _level = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public int Prize { get => _prize; set => _prize = value; }
        public virtual List<Skill> Skills { get => _skills; set => _skills = value; }
    }
}
