using Data;
using Data.People;
using Data.Store;
using Data.Values;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Improv.Methods
{
    public class ImportDependencies
    {
        /*******************
            context.object.Include().ThenIclude() didn't work
            Workaround is to inlude dependencies one by one
        *******************/

        /****************
            Audience
         ****************/

        public static void ImportAudienceDependencies(Audience audience, ConnectDB context)
        {
            ImportAudienceSkillDependencies(audience, context);
            ImportAudiencePrizeDependencies(audience, context);
        }
        public static void ImportAudienceSkillDependencies(Audience audience, ConnectDB context)
        {
            context.audiences
                .Include(dbAudience => dbAudience.Skills)
                .FirstOrDefault(dbAudience => dbAudience.Id == audience.Id);
            foreach (Skill skill in audience.Skills)
            {
                context.skills
                    .Include(dbSkills => dbSkills.Cost)
                    .FirstOrDefault(dbCost => dbCost.Id == skill.Id); // Link cost to skill
                foreach (CostStat cost in skill.Cost)
                {
                    context.costStats
                        .Include(dbCostStat => dbCostStat.Stat)
                        .FirstOrDefault(dbCost => dbCost.StatId == cost.StatId); // Link stat to cost
                }
            }
        }

        public static void ImportAudiencePrizeDependencies(Audience audience, ConnectDB context)
        {
            context.audiences
                .Include(dbAudience => dbAudience.Prize)
                .FirstOrDefault(dbAudience => dbAudience.PrizeId == audience.PrizeId);
        }

        /****************
            End Audience
         ****************/

        /****************
            Team
         ****************/

        public static void ImportTeamDependencies(Team team, ConnectDB context)
        {
            ImportTeamInventoryDependencies(team, context);
            ImportTeamEquipmentsDependencies(team, context);
            ImportTeamStatDependencies(team, context);
            ImportTeamTrainingRoomDependencies(team, context);
        }

        private static void ImportTeamInventoryDependencies(Team team, ConnectDB context)
        {
            // Import inventory and dependencies
            context.teams
                .Include(dbTeam => dbTeam.Inventory)
                .FirstOrDefault(dbTeam => dbTeam.InventoryId == team.InventoryId); // Link inventory to team
            Inventory inventory = context.inventories
                .Include(dbInventory => dbInventory.Equipments)
                .FirstOrDefault(dbInventory => dbInventory.Id == team.InventoryId); // Link equipments to Inventory
            if (inventory.Equipments.Count > 0)
            {
                foreach (Equipment equipment in inventory.Equipments)
                {
                    context.equipments
                        .Include(dbEquipment => dbEquipment.Stats)
                        .FirstOrDefault(dbEquipment => dbEquipment.Id == equipment.Id); // Link stat to equipment. Impossible to do without foreach if a list is used
                }
            }
        }

        private static void ImportTeamEquipmentsDependencies(Team team, ConnectDB context)
        {
            /*// Import equipments and dependencies
            context.teams
                .Include(dbTeam => dbTeam.Equipments)
                .FirstOrDefault(dbTeam => dbTeam.Id == team.Id); // Link equipments to team*/
            context.Entry(team).Collection(t => t.Equipments).Load();
            foreach (Equipment equipment in team.Equipments)
            {
                ImportEquipmentStatsDependencies(equipment, context); // Link stat to equipment. Impossible to do without foreach if a list is used
            }
        }

        public static void ImportTeamStatDependencies(Team team, ConnectDB context)
        {
            // Import stats
            context.teams
                .Include(dbTeam => dbTeam.Stats)
                .FirstOrDefault(dbTeam => dbTeam.Id == team.Id); // Link stats to team
        }

        public static void ImportTeamTrainingRoomDependencies(Team team, ConnectDB context)
        {
            // Import training room and dependencies
            context.teams
                .Include(dbTeam => dbTeam.TrainingRoom)
                .FirstOrDefault(dbTeam => dbTeam.TrainingRoomId == team.TrainingRoomId); // Link Training room to team
            TrainingRoom vTrainingRoom = context.trainingRooms
                .Include(dbTrainingRoom => dbTrainingRoom.Stats)
                .FirstOrDefault(dbTrainingRoom => dbTrainingRoom.Id == team.TrainingRoomId); // Link stats to training room
            foreach(PowerStat powserStat in vTrainingRoom.Stats)
            {
                context.powerStats
                    .Include(dbPowerStat => dbPowerStat.Stat)
                    .FirstOrDefault(dbPowerStat => dbPowerStat.StatId == powserStat.StatId);
            }
            context.Entry(vTrainingRoom).Reference(tr => tr.Shop).Load();
            /*context.shops
                .Include(dbShop => dbShop.Equipments)
                .FirstOrDefault(dbShop => dbShop.Id == team.TrainingRoom.ShopId);*/ // Link shop to training room
        }

        /****************
            End Team
         ****************/

        /****************
            Player
         ****************/

        public static void ImportPlayerDependencies(Player player, ConnectDB context)
        {
            ImportPlayerInventoryDependencies(player, context);
            ImportPlayerEquipmentsDependencies(player, context);
            ImportPlayerStatsDependencies(player, context);
            ImportPlayerSkillsDependencies(player, context);
            ImportPlayerTeamDependencies(player, context);
        }

        private static void ImportPlayerInventoryDependencies(Player player, ConnectDB context)
        {
            // Import inventory and dependencies
            context.players
                .Include(dbPlayer => dbPlayer.Inventory)
                .FirstOrDefault(dbPlayer => dbPlayer.InventoryId == player.InventoryId); // Link inventory to player
            context.inventories
                .Include(dbInventory => dbInventory.Equipments)
                .FirstOrDefault(dbInventory => dbInventory.Id == player.InventoryId); // Link equipments to inventory
            foreach (Equipment equipment in player.Inventory.Equipments)
            {
                ImportEquipmentStatsDependencies(equipment, context); // Link stat to equipment. Impossible to do without foreach if a list is used
            }
        }

        private static void ImportPlayerEquipmentsDependencies(Player player, ConnectDB context)
        {
            // Import equipments and dependencies
            context.players
                .Include(dbPlayer => dbPlayer.Equipments)
                .FirstOrDefault(dbPlayer => dbPlayer.Id == player.Id); // Link equipments to player
            foreach (Equipment equipment in player.Equipments)
            {
                ImportEquipmentStatsDependencies(equipment, context); // Link stat to equipment. Impossible to do without foreach if a list is used
            }
        }

        private static void ImportPlayerStatsDependencies(Player player, ConnectDB context)
        {
            // Import stats
            context.players
                .Include(dbPlayer => dbPlayer.Stats)
                .FirstOrDefault(dbPlayer => dbPlayer.Id == player.Id); // Link stats to player
        }

        private static void ImportPlayerSkillsDependencies(Player player, ConnectDB context)
        {
            // Import skills and dependencies
            context.players
                .Include(dbPlayer => dbPlayer.Skills)
                .FirstOrDefault(dbPlayer => dbPlayer.Id == player.Id); // Link skills to player
            foreach (Skill skill in player.Skills)
            {
                context.skills
                    .Include(dbSkills => dbSkills.Cost)
                    .FirstOrDefault(dbCost => dbCost.Id == skill.Id); // Link cost to skill
                foreach (CostStat cost in skill.Cost)
                {
                    context.costStats
                        .Include(dbCostStat => dbCostStat.Stat)
                        .FirstOrDefault(dbCost => dbCost.StatId == cost.StatId); // Link stat to cost
                }
            }
        }

        private static void ImportPlayerTeamDependencies(Player player, ConnectDB context)
        {
            // Import team and dependencies
            context.players
                .Include(dbPlayer => dbPlayer.Team)
                .FirstOrDefault(dbPlayer => dbPlayer.TeamId == player.TeamId); // Link team to player
            ImportTeamDependencies(player.Team, context);
        }

        /****************
            End Player
         ****************/

        /****************
            Performance
         ****************/

        public static void ImportPerformanceDependencies(Performance performance, ConnectDB context)
        {
            ImportPerformancePrizeDependencies(performance, context);
            if (performance.EquipmentNeeded.Count > 0)
            {
                ImportPerformanceEquipmentNeededDependencies(performance, context);
            }
        }

        private static void ImportPerformancePrizeDependencies(Performance performance, ConnectDB context)
        {
            context.performances
                .Include(dbPerformance => dbPerformance.Prize)
                .FirstOrDefault(dbPerformance => dbPerformance.PrizeId == performance.PrizeId);
        }

        private static void ImportPerformanceEquipmentNeededDependencies(Performance performance, ConnectDB context)
        {
            // Import equipments and dependencies
            context.performances
                .Include(dbPerformance => dbPerformance.EquipmentNeeded)
                .FirstOrDefault(dbPerformance => dbPerformance.Id == performance.Id); // Link equipments to player
            foreach (Equipment equipment in performance.EquipmentNeeded)
            {
                ImportEquipmentStatsDependencies(equipment, context);  // Link stat to equipment. Impossible to do without foreach if a list is used
            }
        }

        /****************
            End Performance
         ****************/

        public static void ImportEquipmentStatsDependencies(Equipment equipment, ConnectDB context)
        {
            context.equipments
                .Include(dbEquipments => dbEquipments.Stats)
                .FirstOrDefault(dbEquipment => dbEquipment.Id == equipment.Id);
        }
    }
}
