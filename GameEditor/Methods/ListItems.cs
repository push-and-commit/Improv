using Data.People;
using Data.Store;
using Data.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEditor.Methods
{
    public class ListItems
    {

        public static void Audiences()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                if (context.audiences.Count() > 0)
                {
                    foreach (Audience audience in context.audiences)
                    {
                        Console.WriteLine("----------------");
                        Console.WriteLine($"Name : {audience.Name}\n" +
                            $"Description : {audience.Description}\n" +
                            $"Number of people : {audience.Quantity}\n" +
                            $"Prize : {audience.Prize.Money} Improv Coins and {audience.Prize.Experience} points of experience");
                        if(audience.Skills.Count > 0)
                        {
                            Console.WriteLine("Skills : ");
                            foreach(Skill skill in audience.Skills)
                            {
                                Console.WriteLine(skill.Name);
                            }
                        }
                    }
                    Console.WriteLine(); // Separate each audience with a carriage return
                }
                else
                {
                    Console.WriteLine("There are no audiences in the database");
                }
            }
        }

        public static Dictionary<int, Audience> ListAudiences(ConnectDB context)
        {
            Dictionary<int, Audience> audiences = new Dictionary<int, Audience>();
            int cpt = 1;

            foreach(Audience audience in context.audiences)
            {
                audiences.Add(cpt, audience);
                Console.WriteLine($"{cpt} - {audience.Name}");
                cpt++;
            }

            return audiences;
        }

        public static void Teams()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                if (context.teams.Count() > 0)
                {
                    foreach (Team team in context.teams)
                    {
                        Console.WriteLine("----------------");
                        Console.WriteLine($"Name : {team.Name}\n" +
                            $"Slogan : {team.Slogan}\n" +
                            $"Level : {team.Level.ToString()}");
                        Console.WriteLine($"\nStats :");
                        foreach (PowerStat stat in team.Stats)
                        {
                            Console.WriteLine($"{stat.Power.ToString()} : {stat.Stat.Name}");
                        }
                        Console.WriteLine($"\nEquipments equipped : ");
                        foreach (Equipment equipment in team.Equipments)
                        {
                            Console.WriteLine($"{equipment.Name}");
                        }
                        Console.WriteLine("\nEquipments in inventory : ");
                        foreach (Equipment equipment in team.Inventory.Equipments)
                        {
                            Console.WriteLine($"{equipment.Name}");
                        }
                        Console.WriteLine($"\nMoney : {team.Money.ToString()}");
                        Console.WriteLine($"\nPlayers : ");
                        foreach (Player player in team.Players)
                        {
                            Console.WriteLine($"Name : {player.Name}\n" +
                                $"Level : {player.Level.ToString()}");
                            if (player.Equipments.Count > 0)
                            {
                                Console.WriteLine("Equipments equipped :");
                                foreach (Equipment equipment in player.Equipments)
                                {
                                    Console.WriteLine($"{equipment.Name}");
                                }
                            }
                            if (player.Inventory.Equipments.Count > 0)
                            {
                                Console.WriteLine("Equipments in inventory :");
                                foreach (Equipment equipment in player.Inventory.Equipments)
                                {
                                    Console.WriteLine($"{equipment.Name}");
                                }
                            }
                            Console.WriteLine("Stats :\n");
                            foreach (PowerStat stat in player.Stats)
                            {
                                Console.WriteLine($"Name : {stat.Stat.Name}\n" +
                                    $"Power : {stat.Power.ToString()}");
                            }
                            Console.WriteLine($"Age : {player.Age.ToString()}");
                            Console.WriteLine("Skills :");
                            foreach(Skill skill in player.Skills)
                            {
                                Console.WriteLine($"Name : {skill.Name}");
                            }
                        }
                        Console.WriteLine($"\nTraining room :");
                        Console.WriteLine($"Name : {team.TrainingRoom.Name}\n" +
                            $"Description : {team.TrainingRoom.Description}\n" +
                            $"Level : {team.TrainingRoom.Level.ToString()}");
                        Console.WriteLine("Stats :");
                        foreach (PowerStat stat in team.TrainingRoom.Stats)
                        {
                            Console.WriteLine($"{stat.Power.ToString()} points of {stat.Stat.Name}");
                        }
                        Console.WriteLine(); // Separate each team with a carriage return
                    }
                }
                else
                {
                    Console.WriteLine("There are no teams in the database");
                }
            }
        }

        public static Dictionary<int, Team> ListTeams(ConnectDB context)
        {
            Dictionary<int, Team> teams = new Dictionary<int, Team>();
            int cpt = 1;

            foreach (Team team in context.teams)
            {
                teams.Add(cpt, team);
                Console.WriteLine($"{cpt} - {team.Name}");
                cpt++;
            }

            return teams;
        }

        public static void Players()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                if (context.players.Count() > 0)
                {
                    foreach (Player player in context.players)
                    {
                        Console.WriteLine("----------------");
                        Console.WriteLine($"Name : {player.Name}\n" +
                            $"Level : {player.Level.ToString()}\n");
                        if (player.Equipments.Count > 0)
                        {
                            Console.WriteLine("Equipments :");
                            foreach (Equipment equipment in player.Equipments)
                            {
                                Console.WriteLine($"{equipment.Name}");
                            }
                        }
                        Console.WriteLine("Stats :\n");
                        foreach (PowerStat stat in player.Stats)
                        {
                            Console.WriteLine($"Name : {stat.Stat.Name}\n" +
                                $"Power : {stat.Power.ToString()}");
                        }
                        Console.WriteLine($"Age : {player.Age.ToString()}");
                        Console.WriteLine("Skills :");
                        foreach (Skill skill in player.Skills)
                        {
                            Console.WriteLine($"Name : {skill.Name}");
                        }
                        Console.WriteLine(); // Separate each player with a carriage return
                    }
                }
                else
                {
                    Console.WriteLine("There are no players in the database");
                }
            }
        }

        public static Dictionary<int, Player> ListPlayers(ConnectDB context)
        {
            Dictionary<int, Player> players = new Dictionary<int, Player>();
            int cpt = 1;

            foreach (Player player in context.players)
            {
                players.Add(cpt, player);
                Console.WriteLine($"{cpt} - {player.Name}");
                cpt++;
            }

            return players;
        }

        public static void Equipments()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                if (context.equipments.Count() > 0)
                {
                    Console.WriteLine("----------------");
                    foreach (Equipment equipment in context.equipments)
                    {
                        Console.WriteLine($"Name : {equipment.Name}\n" +
                            $"Description : {equipment.Description}\n" +
                            $"Price : {equipment.Price.ToString()}\n" +
                            $"Type : {equipment.Type}\n");
                        Console.WriteLine("Stats :");
                        foreach (PowerStat stat in equipment.Stats)
                        {
                            Console.WriteLine($"Name : {stat.Stat.Name}\n" +
                                $"Power : {stat.Power}");
                        }
                        if (equipment.MinLevel > 0)
                        {
                            Console.WriteLine($"Level required to equip : {equipment.MinLevel}");
                        }
                        Console.WriteLine(); // Separate each equipment with a carriage return
                    }
                }
                else
                {
                    Console.WriteLine("There are no equipments in the database");
                }
            }
        }

        public static Dictionary<int, Equipment> ListEquipments(ConnectDB context)
        {
            Dictionary<int, Equipment> equipments = new Dictionary<int, Equipment>();
            int cpt = 1;

            foreach (Equipment equipment in context.equipments)
            {
                equipments.Add(cpt, equipment);
                Console.WriteLine($"{cpt} - {equipment.Name}");
                cpt++;
            }

            return equipments;
        }

        public static void Skills()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                if (context.skills.Count() > 0)
                {
                    foreach (Skill skill in context.skills)
                    {
                        Console.WriteLine("----------------");
                        Console.WriteLine($"Name : {skill.Name}\n" +
                            $"Description : {skill.Description}\n" +
                            $"Type : {skill.Type}\n" +
                            $"Power : {skill.Power}\n");
                        if (skill.Cost.Count > 0)
                        {
                            Console.WriteLine("Stats needed to use :");
                            foreach (CostStat cost in skill.Cost)
                            {
                                Console.WriteLine($"{cost.Cost} points of {cost.Stat.Name}");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("There are no skills in the database");
                }
            }
        }

        public static Dictionary<int, Skill> ListSkills(ConnectDB context)
        {
            Dictionary<int, Skill> skills = new Dictionary<int, Skill>();
            int cpt = 1;

            foreach (Skill skill in context.skills)
            {
                skills.Add(cpt, skill);
                Console.WriteLine($"{cpt} - {skill.Name}");
                cpt++;
            }

            return skills;
        }

        public static void Stats()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                if (context.stats.Count() > 0)
                {
                    foreach (Stat stat in context.stats)
                    {
                        Console.WriteLine("----------------");
                        Console.WriteLine($"Name : {stat.Name}\n" +
                            $"Description : {stat.Description}\n" +
                            $"Type : {stat.Type}");
                        Console.WriteLine(); // Separate each stat with a carriage return
                    }
                }
                else
                {
                    Console.WriteLine("There are no stats in the database");
                }
            }
        }

        public static Dictionary<int, Stat> ListStats(ConnectDB context)
        {
            Dictionary<int, Stat> stats = new Dictionary<int, Stat>();
            int cpt = 1;

            foreach (Stat stat in context.stats)
            {
                stats.Add(cpt, stat);
                Console.WriteLine($"{cpt} - {stat.Name}");
                cpt++;
            }

            return stats;
        }

        public static void Performances()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                if (context.performances.Count() > 0)
                {
                    foreach (Performance performance in context.performances)
                    {
                        Console.WriteLine("----------------");
                        Console.WriteLine($"Name : {performance.Name}\n" +
                            $"Description : {performance.Description}\n" +
                            $"Duration : {performance.Duration}\n" +
                            $"Duration : {performance.NbPlayers}\n" +
                            $"Prize : {performance.Prize.Money} Improv Coins and {performance.Prize.Experience} points of experience\n");
                        if(performance.EquipmentNeeded.Count > 0) {
                            Console.WriteLine($"Equipments needed");
                            foreach (Equipment equipment in performance.EquipmentNeeded)
                            {
                                Console.WriteLine($"Name : {equipment.Name}");
                            }
                        }
                        Console.WriteLine(); // Separate each performance with a carriage return
                    }
                }
                else
                {
                    Console.WriteLine("There are no performances in the database");
                }
            }
        }

        public static Dictionary<int, Performance> ListPerformances(ConnectDB context)
        {
            Dictionary<int, Performance> performances = new Dictionary<int, Performance>();
            int cpt = 1;

            foreach (Performance performance in context.performances)
            {
                performances.Add(cpt, performance);
                Console.WriteLine($"{cpt} - {performance.Name}");
                cpt++;
            }

            return performances;
        }
    }
}
