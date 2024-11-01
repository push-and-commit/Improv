﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Values
{
    public class Skill
    {
        private int _id;
        private string _name;
        private string _description;
        private string _type;
        private int _power;
        private List<CostStat> _cost;

        public Skill() { }

        public Skill(string name, string description, string type, int power, List<CostStat> cost)
        {
            _name = name;
            _description = description;
            _type = type;
            _power = power;
            _cost = cost;
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public string Type { get => _type; set => _type = value; }
        public int Power { get => _power; set => _power = value; }
        public virtual List<CostStat> Cost { get => _cost; set => _cost = value; }
    }
}
