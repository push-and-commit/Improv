using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Values
{
    public class CostStat
    {
        private int _id;
        private Stat _stat;
        private int _cost;

        public CostStat() { }

        public CostStat(Stat stat, int cost)
        {
            _stat = stat;
            _cost = cost;
        }

        public int Id { get => _id; set => _id = value; }
        public virtual Stat Stat { get => _stat; set => _stat = value; }
        public int Cost { get => _cost; set => _cost = value; }
    }
}
