using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Store;
using Data.Values;
using Data.Interfaces;

namespace Data.People
{
    public class Impro
    {
        [Key]
        private int _id;
        private string _name;
        private int _level;
        private int _experience;
        private int _experienceToLevelUp;
        private List<Equipment> _equipments;
        private List<PowerStat> _stats;
        private int _inventoryId;
        [ForeignKey("InventoryId")]
        private Inventory _inventory;

        public Impro() { }

        public Impro(string name, int level, List<Equipment> equipments, List<PowerStat> stats, Inventory inventory)
        {
            _name = name;
            _level = level;
            _experience = 0;
            _experienceToLevelUp = 100 + (level * 10 + 30);
            _equipments = equipments;
            _stats = stats;
            _inventory = inventory;

            _inventoryId = inventory.Id;
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public int Level { get => _level; set => _level = value; }
        public int Experience { get => _experience; set => _experience = value; }
        public int ExperienceToLevelUp { get => _experienceToLevelUp; set => _experienceToLevelUp = value; }
        public virtual List<Equipment> Equipments { get => _equipments; set => _equipments = value; }
        public virtual List<PowerStat> Stats { get => _stats; set => _stats = value; }
        public virtual int InventoryId { get => _inventoryId; set => _inventoryId = value; }
        public virtual Inventory Inventory { get => _inventory; set => _inventory = value; }

        public void AddEquipment(Equipment equipment)
        {
            Equipments.Add(equipment);
        }

        public void RemoveEquipment(Equipment equipment)
        {
            Equipments.Remove(equipment);
        }
    }
}
