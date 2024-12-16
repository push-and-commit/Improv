using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Values
{
    public class Prize
    {
        private int _id;
        private int _money;
        private int _experience;

        public Prize(int money, int experience)
        {
            _money = money;
            _experience = experience;
        }

        public int Id { get => _id; set => _id = value; }
        public int Money { get => _money; set => _money = value; }
        public int Experience { get => _experience; set => _experience = value; }

        public void DisplayPrize()
        {
            Console.WriteLine($"Money: {_money}");
            Console.WriteLine($"Experience: {_experience}");
        }
    }
}
