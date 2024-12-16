using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Enums;
using Data.Interfaces;
using Data.Store;
using Data.Values;

namespace Data.People
{
    public class Performance : IDisplay
    {
        private int _id;
        private string _name;
        private string _description;
        private int _duration;
        private int _nbPlayers;
        private PerformanceTypeEnum _type;
        private Prize _prize;
        private List<Equipment> _equipmentNeeded;

        public Performance() { }

        public Performance(string name, string description, int duration, int nbPlayers, PerformanceTypeEnum type, Prize prize, List<Equipment> equipmentNeeded)
        {
            _name = name;
            _description = description;
            _duration = duration;
            _nbPlayers = nbPlayers;
            _type = type;
            _prize = prize;
            _equipmentNeeded = equipmentNeeded;
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int Duration { get => _duration; set => _duration = value; }
        public int NbPlayers { get => _nbPlayers; set => _nbPlayers = value; }
        public virtual PerformanceTypeEnum Type { get => _type; set => _type = value; }
        public virtual Prize Prize { get => _prize; set => _prize = value; }
        public virtual List<Equipment> EquipmentNeeded { get => _equipmentNeeded; set => _equipmentNeeded = value; }

        public void DisplaySelf()
        {
            Console.WriteLine($"Name : {_name}");
            Console.WriteLine($"Description : {_description}");
            Console.WriteLine($"Duration (number of turns) : {_duration}");
            Console.WriteLine($"Number of players required to perform : {_nbPlayers}");
            Console.WriteLine($"Prize : {_prize.Money} Improv Coins and {_prize.Experience} points of experience");
            Console.WriteLine("Equipments needed to perform :");
            if (_equipmentNeeded.Count > 0)
            {
                foreach (Equipment equipment in _equipmentNeeded)
                {
                    Console.WriteLine(equipment.Name);
                }
            }
        }
    }
}
