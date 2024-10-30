using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Store;
using Data.Values;

namespace Data.People
{
    public class Player : Impro
    {
        int _age;
        List<Skill> _skills;
        Team _team;
        string _type; // starter - common - average - rare

        public Player() { }

        public Player(int id, string name, int level, List<Equipment> equipments, List<Stat> stats, Inventory inventory, int age) : base(name, level, equipments, stats, inventory)
        {
            _age = age;
            _skills = new List<Skill>();
            _team = null;
            inventory.NbItemsMax = 10;
        }

        public int Age { get => _age; set => _age = value; }
        public virtual List<Skill> Skills { get => _skills; set => _skills = value; }
        public virtual Team Team { get => _team; set => _team = value; }
        public string Type { get => _type; set => _type = value; }

        public void joinTeam(Team team)
        {
            _team = team;
            team.RecruitPlayer(this);
        }

        public void leaveTeam(Team team)
        {
            _team = null;
            team.Players.Remove(this);
        }

        public void UseSkill(Skill skill)
        {

        }
    }
}
