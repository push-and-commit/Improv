using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Values
{
    public class PowerStat
    {
        
        [Key]
        private int _id;

        private int _statId;
        [ForeignKey("StatId")]
        private Stat _stat;
        private string _objectName;
        private string _objectType;
        private int _power;

        public PowerStat() { }

        public PowerStat(PowerStat powerStat)
        {
            _stat = powerStat.Stat;
            _objectName = powerStat.ObjectName;
            _objectType = powerStat.ObjectType;
            _power = powerStat.Power;

            _statId = powerStat.Id;
        }

        public PowerStat(Stat stat, string objectName, string objectType, int power)
        {
            _stat = stat;
            _objectName = objectName;
            _objectType = objectType;
            _power = power;

            _statId = stat.Id;
        }

        public int Id { get => _id; set => _id = value; }
        public int StatId { get => _statId; set => _statId = value; }
        public virtual Stat Stat { get => _stat; set => _stat = value; }
        public string ObjectName { get => _objectName; set => _objectName = value; }
        public string ObjectType { get => _objectType; set => _objectType = value; }
        public int Power { get => _power; set => _power = value; }
    }
}
