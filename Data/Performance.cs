using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.People;
using Data.Store;

namespace Data
{
    public class Performance
    {
        private int _id;
        private string _name;
        private string _description;
        private int _duration;
        private int _nbPlayers;
        private int _prize;
        private List<Equipment> _equipmentNeeded;

        public Performance() { }

        public Performance(string name, string description, int duration, int nbPlayers, int prize, List<Equipment> equipmentNeeded)
        {
            _name = name;
            _description = description;
            _duration = duration;
            _nbPlayers = nbPlayers;
            _prize = prize;
            _equipmentNeeded = equipmentNeeded;
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int Duration { get => _duration; set => _duration = value; }
        public int NbPlayers { get => _nbPlayers; set => _nbPlayers = value; }
        public int Prize { get => _prize; set => _prize = value; }
        public virtual List<Equipment> EquipmentNeeded { get => _equipmentNeeded; set => _equipmentNeeded = value; }
    }
}
