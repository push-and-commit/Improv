﻿using Data.Interfaces;
using Data.Store;
using Data.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.People
{
    public class TrainingRoom : IDisplay
    {
        private int _id;
        private string _name;
        private string _description;
        private int _level;
        private List<PowerStat> _stats;

        public TrainingRoom() { }

        public TrainingRoom(string name, string description, int level, List<PowerStat> stats)
        {
            _name = name;
            _description = description;
            _level = level;
            _stats = stats;
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int Level { get => _level; set => _level = value; }
        public virtual List<PowerStat> Stats { get => _stats; set => _stats = value; }

        public void DisplaySelf()
        {
            Console.WriteLine($"Name : {_name}");
            Console.WriteLine($"Description : {_description}");
            Console.WriteLine($"Level : {_level}");
            Console.WriteLine("Stats :");
            foreach (PowerStat stat in _stats)
            {
                Console.WriteLine($"{stat.Power} points of {stat.Stat.Name} ");
            }
        }
    }
}