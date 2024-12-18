using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.People;
using Data.Values;

namespace Data.Store
{
    public class Shop : IDisplay
    {
        [Key]
        private int _id;
        private string _name;
        private string _description;
        private List<Equipment> _equipments;

        public Shop() { }

        public Shop(string name, string description, List<Equipment> equipments)
        {
            _name = name;
            _description = description;
            _equipments = equipments;
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public virtual List<Equipment> Equipments { get => _equipments; set => _equipments = value; }

        public void DisplaySelf()
        {
            Console.WriteLine($"Name : {_name}");
            Console.WriteLine($"Description : {_description}");
            if (_equipments.Count > 0)
            {
                Console.WriteLine("Equipments :");
                foreach (Equipment equipment in _equipments)
                {
                    Console.WriteLine(equipment.Name);
                }
            }
        }
    }
}
