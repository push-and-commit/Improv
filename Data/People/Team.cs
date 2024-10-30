using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Data.Store;
using Data.Values;

namespace Data.People
{
    public class Team : Impro
    {
        private string _slogan;
        private TrainingRoom _trainingRoom;
        private int _money;
        private List<Player> _players;

        public Team() { }

        public Team(string name, int level, List<Equipment> equipments, List<Stat> stats, Inventory inventory, string slogan, int money) : base(name, level, equipments, stats, inventory)
        {
            _slogan = slogan;
            _trainingRoom = new TrainingRoom("Training room", "My first training room", 0);
            _money = money;
            _players = new List<Player>();
            Inventory.NbItemsMax = 15;
        }

        public string Slogan { get => _slogan; set => _slogan = value; }
        public virtual TrainingRoom TrainingRoom { get => _trainingRoom; set => _trainingRoom = value; }
        public int Money { get => _money; set => _money = value; }
        public virtual List<Player> Players { get => _players; set => _players = value; }

        public void RecruitPlayer(Player player)
        {
            Players.Add(player);
        }

        public void SellPlayer(Player player)
        {
            Players.Remove(player);
        }

        public void BuyEquipment(Equipment equipment, int quantity)
        {
            if (Money >= quantity * equipment.Price)
            {
                Money -= quantity * equipment.Price;
                for (int i = 0; i < quantity; i++)
                {
                    Inventory.AddToInventory(equipment);
                }
            }
        }

        public void SellEquipment(Equipment equipment, int quantity)
        {
            Money += quantity * equipment.Price;
            for (int i = 0; i < quantity; i++)
            {
                Inventory.RemoveFromInventory(equipment);
            }
        }
    }
}
