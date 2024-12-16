﻿using Castle.DynamicProxy;
using Data.People;
using Data.Store;
using Data.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.Methods
{
    public class RemoveItems
    {
        public static void Audiences()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                Dictionary<int, Audience> audiences = new Dictionary<int, Audience>();
                int cpt = 0;
                int userInput = -1;
                string message = "Which audience would you like to remove ?";

                foreach (Audience audience in context.audiences)
                { // Retrieve the audiences
                    audiences.Add(cpt, audience);
                    Console.WriteLine($"{cpt} - {audience.Name}");
                    cpt++;
                }
                Console.WriteLine($"{context.audiences.Count()} - Go back to remove items menu");

                do
                { // Ask which audience to remove
                    userInput = Data.Methods.General.AskForUserInputInt(message, 0, context.audiences.Count());
                } while (!audiences.ContainsKey(userInput) && userInput != context.audiences.Count());

                if (userInput != context.audiences.Count())
                {
                    Audience audienceToRemove = audiences[userInput];

                    // Remove audience
                    context.audiences.Remove(audienceToRemove);
                    context.SaveChanges();
                }
                else
                {
                    General.RemoveItemsMenu();
                }
            }
        }

        public static void Teams()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                Dictionary<int, Team> teams = new Dictionary<int, Team>();
                int cpt = 0;
                int userInput = -1;
                string message = "Which team would you like to remove ?";

                foreach (Team team in context.teams)
                { // Retrieve the teams
                    teams.Add(cpt, team);
                    Console.WriteLine($"{cpt} - {team.Name}");
                    cpt++;
                }
                Console.WriteLine($"{context.teams.Count()} - Go back to remove items menu");

                do
                { // Ask which team to remove
                    userInput = Data.Methods.General.AskForUserInputInt(message, 0, context.teams.Count());
                } while (!teams.ContainsKey(userInput));

                if (userInput != context.audiences.Count())
                {
                    Team teamToRemove = teams[userInput];

                    // Remove team
                    context.teams.Remove(teamToRemove);
                    context.SaveChanges();
                }
                else
                {
                    General.RemoveItemsMenu();
                }
            }
        }

        public static void Players()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                Dictionary<int, Player> players = new Dictionary<int, Player>();
                int cpt = 0;
                int userInput = -1;
                string message = "Which player would you like to remove ?";

                foreach (Player player in context.players)
                { // Retrieve the players
                    players.Add(cpt, player);
                    Console.WriteLine($"{cpt} - {player.Name}");
                    cpt++;
                }
                Console.WriteLine($"{context.players.Count()} - Go back to remove items menu");

                do
                { // Ask which player to remove
                    userInput = Data.Methods.General.AskForUserInputInt(message, 0, context.players.Count());
                } while (!players.ContainsKey(userInput));

                if (userInput != context.audiences.Count())
                {
                    Player playerToRemove = players[userInput];

                    // Remove team
                    context.players.Remove(playerToRemove);
                    context.SaveChanges();
                }
                    else
                {
                    General.RemoveItemsMenu();
                }
            }
        }

        public static void Equipments()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                Dictionary<int, Equipment> equipments = new Dictionary<int, Equipment>();
                int cpt = 0;
                int userInput = -1;
                string message = "Which equipment would you like to remove ?";

                foreach (Equipment equipment in context.equipments)
                { // Retrieve the equipments
                    equipments.Add(cpt, equipment);
                    Console.WriteLine($"{cpt} - {equipment.Name}");
                    cpt++;
                }
                Console.WriteLine($"{context.equipments.Count()} - Go back to remove items menu");

                do
                { // Ask which equipment to remove
                    userInput = Data.Methods.General.AskForUserInputInt(message, 0, context.equipments.Count());
                } while (!equipments.ContainsKey(userInput));

                if (userInput != context.audiences.Count())
                {
                    Equipment equipmentToRemove = equipments[userInput];

                    // Remove equipment
                    context.equipments.Remove(equipmentToRemove);
                    context.SaveChanges();
                }
                else
                {
                    General.RemoveItemsMenu();
                }
            }
        }

        public static void Skills()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                Dictionary<int, Skill> skills = new Dictionary<int, Skill>();
                int cpt = 0;
                int userInput = -1;
                string message = "Which skill would you like to remove ?";

                foreach (Skill skill in context.skills)
                { // Retrieve the skills
                    skills.Add(cpt, skill);
                    Console.WriteLine($"{cpt} - {skill.Name}");
                    cpt++;
                }
                Console.WriteLine($"{context.skills.Count()} - Go back to remove items menu");

                do
                { // Ask which skill to remove
                    userInput = Data.Methods.General.AskForUserInputInt(message, 0, context.skills.Count());
                } while (!skills.ContainsKey(userInput));

                if (userInput != context.audiences.Count())
                {
                    Skill skillToRemove = skills[userInput];

                    // Remove skill
                    context.skills.Remove(skillToRemove);
                    context.SaveChanges();
                }
                else
                {
                    General.RemoveItemsMenu();
                }
            }
        }

        public static void Stats()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                Dictionary<int, Stat> stats = new Dictionary<int, Stat>();
                int cpt = 0;
                int userInput = -1;
                string message = "Which stat would you like to remove ?";

                foreach (Stat stat in context.stats)
                { // Retrieve the stats
                    stats.Add(cpt, stat);
                    Console.WriteLine($"{cpt} - {stat.Name}");
                    cpt++;
                }
                Console.WriteLine($"{context.stats.Count()} - Go back to remove items menu");

                do
                { // Ask which stat to remove
                    userInput = Data.Methods.General.AskForUserInputInt(message, 0, context.stats.Count());
                } while (!stats.ContainsKey(userInput));

                if (userInput != context.audiences.Count())
                {
                    Stat statToRemove = stats[userInput];

                    // Remove stat
                    context.stats.Remove(statToRemove);
                    context.SaveChanges();
                }
                else
                {
                    General.RemoveItemsMenu();
                }
            }
        }

        public static void Performances()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                Dictionary<int, Performance> performances = new Dictionary<int, Performance>();
                int cpt = 0;
                int userInput = -1;
                string message = "Which performance would you like to remove ?";

                foreach (Performance performance in context.performances)
                { // Retrieve the performances
                    performances.Add(cpt, performance);
                    Console.WriteLine($"{cpt} - {performance.Name}");
                    cpt++;
                }
                Console.WriteLine($"{context.performances.Count()} - Go back to remove items menu");

                do
                { // Ask which performances to remove
                    userInput = Data.Methods.General.AskForUserInputInt(message, 0, context.performances.Count());
                } while (!performances.ContainsKey(userInput));

                if (userInput != context.audiences.Count())
                {
                    Performance performanceToRemove = performances[userInput];

                    // Remove equipment
                    context.performances.Remove(performanceToRemove);
                    context.SaveChanges();
                }
                else
                {
                    General.RemoveItemsMenu();
                }
            }
        }
    }
}
