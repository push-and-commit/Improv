using Data;
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
                        Console.WriteLine($"Name : {audience.Name}\n" +
                            $"Description : {audience.Description}\n" +
                            $"Number of people : {audience.Quantity}\n" +
                            $"Prize : {audience.Prize}\n" +
                            $"Prize if won : {audience.WinningPrize}");
                    }
                    Console.WriteLine(); // Separate each team with a carriage return
                }
                else
                {
                    Console.WriteLine("There are no audiences in the database");
                }
            }
        }

        public static void Teams()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                if (context.teams.Count() > 0)
                {
                    foreach (Team team in context.teams)
                    {
                        Console.WriteLine($"Name : {team.Name}\n" +
                            $"Slogan : {team.Slogan}\n" +
                            $"Level : {team.Level.ToString()}\n");
                        Console.WriteLine($"Stats : {team.Stats}");
                        foreach (Stat stat in team.Stats)
                        {
                            Console.WriteLine($"Name : {stat.Name}\n" +
                                $"Power : {stat.Power.ToString()}");
                        }
                        Console.WriteLine($"Equipments equipped : ");
                        foreach (Equipment equipment in team.Equipments)
                        {
                            Console.WriteLine($"{equipment.Name}");
                        }
                        Console.WriteLine("Equipments in inventory : ");
                        foreach (Equipment equipment in team.Inventory.Equipments)
                        {
                            Console.WriteLine($"{equipment.Name}");
                        }
                        Console.WriteLine($"Money : {team.Money.ToString()}");
                        Console.WriteLine($"Players : ");
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
                            foreach (Stat stat in player.Stats)
                            {
                                Console.WriteLine($"Name : {stat.Name}\n" +
                                    $"Power : {stat.Power.ToString()}");
                            }
                            Console.WriteLine($"Age : {player.Age.ToString()}");
                            Console.WriteLine("Skills :");
                            foreach(Skill skill in player.Skills)
                            {
                                Console.WriteLine($"Name : {skill.Name}");
                            }
                        }
                        Console.WriteLine($"Training room :");
                        Console.WriteLine($"Name : {team.TrainingRoom.Name}\n" +
                            $"Description : {team.TrainingRoom.Description}\n" +
                            $"Level : {team.TrainingRoom.Level.ToString()}");
                        Console.WriteLine("Stats :");
                        foreach (Stat stat in team.TrainingRoom.Stats)
                        {
                            Console.WriteLine($"Name : {stat.Name}\n" +
                                $"Power : {stat.Power.ToString()}");
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

        public static void Players()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                if (context.players.Count() > 0)
                {
                    foreach (Player player in context.players)
                    {
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
                        foreach (Stat stat in player.Stats)
                        {
                            Console.WriteLine($"Name : {stat.Name}\n" +
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

        public static void Equipments()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                if (context.equipments.Count() > 0)
                {
                    foreach (Equipment equipment in context.equipments)
                    {
                        Console.WriteLine($"Name : {equipment.Name}\n" +
                            $"Description : {equipment.Description}\n" +
                            $"Price : {equipment.Price.ToString()}\n" +
                            $"Type : {equipment.Type}\n");
                        Console.WriteLine("Stats :");
                        foreach (Stat stat in equipment.Stats)
                        {
                            Console.WriteLine($"Name : {stat.Name}\n" +
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

        public static void Skills()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                if (context.skills.Count() > 0)
                {
                    foreach (Skill skill in context.skills)
                    {
                        Console.WriteLine($"Name : {skill.Name}\n" +
                            $"Description : {skill.Description}\n" +
                            $"Type : {skill.Type}\n" +
                            $"Power : {skill.Power}\n" +
                            $"Stat needed to use : {skill.Cost.ElementAt(0)}\n" +
                            $"Power stat needed to use : {skill.Cost.ElementAt(1)}");
                        Console.WriteLine(); // Separate each skill with a carriage return
                    }
                }
                else
                {
                    Console.WriteLine("There are no skills in the database");
                }
            }
        }

        public static void Stats()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                if (context.stats.Count() > 0)
                {
                    foreach (Stat stat in context.stats)
                    {
                        Console.WriteLine($"Name : {stat.Name}\n" +
                            $"Description : {stat.Description}\n" +
                            $"Number of people : {stat.Type}\n" +
                            $"Prize : {stat.Power}");
                        Console.WriteLine(); // Separate each stat with a carriage return
                    }
                }
                else
                {
                    Console.WriteLine("There are no stats in the database");
                }
            }
        }

        public static void Performances()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                if (context.performances.Count() > 0)
                {
                    foreach (Performance performance in context.performances)
                    {
                        Console.WriteLine($"Name : {performance.Name}\n" +
                            $"Description : {performance.Description}\n" +
                            $"Duration : {performance.Duration}\n" +
                            $"Duration : {performance.NbPlayers}\n" +
                            $"Prize : {performance.Prize}\n");
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
    }
}
