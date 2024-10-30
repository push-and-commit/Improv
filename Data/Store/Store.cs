using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.People;

namespace Data.Store
{
    public class Store
    {
        private int _id;
        private string _name;
        private string _description;
        private List<Equipment> _equipments;
        private TrainingRoom _trainingRoom;

        public Store(string name, string description, List<Equipment> equipments, TrainingRoom trainingRoom)
        {
            _name = name;
            _description = description;
            _equipments = equipments;
            _trainingRoom = trainingRoom;
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public virtual List<Equipment> Equipments { get => _equipments; set => _equipments = value; }
        public virtual TrainingRoom TrainingRoom { get => _trainingRoom; set => _trainingRoom = value; }
    }
}
