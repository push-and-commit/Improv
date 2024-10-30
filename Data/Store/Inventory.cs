using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Store
{
    public class Inventory
    {
        private int _id;
        private List<Equipment> _equipments;
        private int _nbItemsMax;

        public Inventory()
        {
            _equipments = new List<Equipment>();
            _nbItemsMax = 0;
        }

        public int Id { get => _id; set => _id = value; }
        public virtual List<Equipment> Equipments { get => _equipments; set => _equipments = value; }
        public int NbItemsMax { get => _nbItemsMax; set => _nbItemsMax = value; }

        public void AddToInventory(Equipment equipment)
        {
            if (Equipments.Count < NbItemsMax)
            {
                Equipments.Add(equipment);
            }
        }

        public void RemoveFromInventory(Equipment equipment)
        {
            Equipments.Remove(equipment);
        }
    }
}
