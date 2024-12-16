using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Values
{
    public class PowerStat
    {
        private int _id;
        private Stat _stat;
        private string _objectName;
        private string _objectType;
        private int _power;

        public PowerStat() { }

        public PowerStat(Stat stat, string objectName, string objectType, int power)
        {
            Stat = stat;
            ObjectName = objectName;
            ObjectType = objectType;
            Power = power;
        }

        public int Id { get => _id; set => _id = value; }
        public virtual Stat Stat { get => _stat; set => _stat = value; }
        public string ObjectName { get => _objectName; set => _objectName = value; }
        public string ObjectType { get => _objectType; set => _objectType = value; }
        public int Power { get => _power; set => _power = value; }
    }
}
