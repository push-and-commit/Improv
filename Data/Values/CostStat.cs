using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Values
{
    public class CostStat
    {
        [Key]
        private int _id;
        private int _statId;
        [ForeignKey("StatId")]
        private Stat _stat;
        private string _objectName;
        private SkillTypeEnum _objectType;
        private int _cost;

        public CostStat() { }

        public CostStat(Stat stat, int cost, string objectName, SkillTypeEnum objectType)
        {
            _stat = stat;
            _objectName = objectName;
            _objectType = objectType;
            _cost = cost;

            _statId = stat.Id;
        }

        public int Id { get => _id; set => _id = value; }
        public int StatId { get => _statId; set => _statId = value; }
        public virtual Stat Stat { get => _stat; set => _stat = value; }
        public string ObjectName { get => _objectName; set => _objectName = value; }
        public SkillTypeEnum ObjectType { get => _objectType; set => _objectType = value; }
        public int Cost { get => _cost; set => _cost = value; }
    }
}
