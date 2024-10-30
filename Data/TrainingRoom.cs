using Data;
using Data.Store;
using Data.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class TrainingRoom
    {
        private int _id;
        private string _name;
        private string _description;
        private int _level;
        private List<Stat> _stats;

        public TrainingRoom() { }

        public TrainingRoom(string name, string description, int level)
        {
            _name = name;
            _description = description;
            _level = level;
            _stats = new List<Stat>();
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int Level { get => _level; set => _level = value; }
        public virtual List<Stat> Stats { get => _stats; set => _stats = value; }
    }
}
