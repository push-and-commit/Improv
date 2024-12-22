using Data;
using Data.Enums;
using Data.People;
using Data.Store;
using Data.Values;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Methods
{
    public class GetItems
    {

        public static Team LoadTeam(Team team, ConnectDB context)
        {
            return context.teams.FirstOrDefault(dbTeam => dbTeam.Name == team.Name);
        }

        public static List<Equipment> GetConsumables(Team team, ConnectDB context)
        {
            context.teams.Entry(team).Collection(dbTeam => dbTeam.Equipments).Load();
            List<Equipment> consumables = new List<Equipment>();
            foreach (Equipment equipment in team.Equipments)
            {
                if (equipment.Type == EquipmentTypeEnum.Consumable)
                {
                    consumables.Add(equipment);
                }
            }
            return consumables;
        }
        public static List<Team> GetComputerTeams(ConnectDB context)
        {
            List<Team> teams = new List<Team>();
            foreach (Team team in context.teams)
            {
                if(team.Type == TeamTypeEnum.Computer)
                {
                    teams.Add(team);
                }
            }
            return teams;
        }
    }
}
