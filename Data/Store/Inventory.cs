using Data.Interfaces;
using Data.Values;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Data.Store
{
    public class Inventory : IDisplay
    {
        [Key]
        private int _id;
        private List<Equipment>? _equipments;
        private int _nbItemsMax;

        public Inventory(int nbItemsMax)
        {
            _equipments = new List<Equipment>();
            _nbItemsMax = nbItemsMax;
        }

        public int Id { get => _id; set => _id = value; }
        public virtual List<Equipment>? Equipments { get => _equipments; set => _equipments = value; }
        public int NbItemsMax { get => _nbItemsMax; set => _nbItemsMax = value; }

        public void DisplaySelf()
        {
            Console.WriteLine($"Number of equipments it can hold : {_nbItemsMax}");
            if (_equipments.Count > 0)
            {
                Console.WriteLine("Equipments :");
                foreach (Equipment equipment in _equipments)
                {
                    Console.WriteLine(equipment.Name);
                }
            }
        }

        public void AddToInventory(Equipment equipment)
        {
            if (Equipments.Count < NbItemsMax)
            {
                Equipments.Add(equipment);
            }
            else
            {
                Console.WriteLine($"Can't add {equipment.Name}. Your inventory is full !");
            }
        }

        public void RemoveFromInventory(Equipment equipment)
        {
            Equipments.Remove(equipment);
        }
    }
}
