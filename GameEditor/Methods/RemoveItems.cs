using Castle.DynamicProxy;
using Data;
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
                int[] possibleAnswer = new int[context.audiences.Count() + 1];
                int userInput = -1;
                string message = "Which audience would you like to remove ?";

                foreach (Audience audience in context.audiences)
                { // Retrieve the audiences
                    audiences.Add(cpt, audience);
                    Console.WriteLine($"{cpt} - {audience.Name}");
                    possibleAnswer[cpt] = cpt;
                    cpt++;
                }
                Console.WriteLine($"{context.audiences.Count()} - Go back to remove items menu");
                possibleAnswer[context.audiences.Count()] = context.audiences.Count();

                do
                { // Ask which audience to remove
                    userInput = Data.Methods.General.AskForUserInputInt(message, possibleAnswer);
                } while (!audiences.ContainsKey(userInput));
                Audience audienceToRemove = audiences[userInput];

                // Remove audience
                context.audiences.Remove(audienceToRemove);
            }
        }

        public static void Teams()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                Dictionary<int, Team> teams = new Dictionary<int, Team>();
                int cpt = 0;
                int[] possibleAnswer = new int[context.teams.Count() + 1];
                int userInput = -1;
                string message = "Which team would you like to remove ?";

                foreach (Team team in context.teams)
                { // Retrieve the teams
                    teams.Add(cpt, team);
                    Console.WriteLine($"{cpt} - {team.Name}");
                    possibleAnswer[cpt] = cpt;
                    cpt++;
                }
                Console.WriteLine($"{context.teams.Count()} - Go back to remove items menu");
                possibleAnswer[context.teams.Count()] = context.teams.Count();

                do
                { // Ask which team to remove
                    userInput = Data.Methods.General.AskForUserInputInt(message, possibleAnswer);
                } while (!teams.ContainsKey(userInput));
                Team teamToRemove = teams[userInput];

                // Remove team
                context.teams.Remove(teamToRemove);
            }
        }

        public static void Players()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                Dictionary<int, Player> players = new Dictionary<int, Player>();
                int cpt = 0;
                int[] possibleAnswer = new int[context.players.Count() + 1];
                int userInput = -1;
                string message = "Which player would you like to remove ?";

                foreach (Player player in context.players)
                { // Retrieve the players
                    players.Add(cpt, player);
                    Console.WriteLine($"{cpt} - {player.Name}");
                    possibleAnswer[cpt] = cpt;
                    cpt++;
                }
                Console.WriteLine($"{context.players.Count()} - Go back to remove items menu");
                possibleAnswer[context.players.Count()] = context.players.Count();

                do
                { // Ask which player to remove
                    userInput = Data.Methods.General.AskForUserInputInt(message, possibleAnswer);
                } while (!players.ContainsKey(userInput));
                Player playerToRemove = players[userInput];

                // Remove team
                context.players.Remove(playerToRemove);
            }
        }

        public static void Equipments()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                Dictionary<int, Equipment> equipments = new Dictionary<int, Equipment>();
                int cpt = 0;
                int[] possibleAnswer = new int[context.equipments.Count() + 1];
                int userInput = -1;
                string message = "Which equipment would you like to remove ?";

                foreach (Equipment equipment in context.equipments)
                { // Retrieve the equipments
                    equipments.Add(cpt, equipment);
                    Console.WriteLine($"{cpt} - {equipment.Name}");
                    possibleAnswer[cpt] = cpt;
                    cpt++;
                }
                Console.WriteLine($"{context.equipments.Count()} - Go back to remove items menu");
                possibleAnswer[context.equipments.Count()] = context.equipments.Count();

                do
                { // Ask which equipment to remove
                    userInput = Data.Methods.General.AskForUserInputInt(message, possibleAnswer);
                } while (!equipments.ContainsKey(userInput));
                Equipment equipmentToRemove = equipments[userInput];

                // Remove equipment
                context.equipments.Remove(equipmentToRemove);
            }
        }

        public static void Skills()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                Dictionary<int, Skill> skills = new Dictionary<int, Skill>();
                int cpt = 0;
                int[] possibleAnswer = new int[context.skills.Count() + 1];
                int userInput = -1;
                string message = "Which skill would you like to remove ?";

                foreach (Skill skill in context.skills)
                { // Retrieve the skills
                    skills.Add(cpt, skill);
                    Console.WriteLine($"{cpt} - {skill.Name}");
                    possibleAnswer[cpt] = cpt;
                    cpt++;
                }
                Console.WriteLine($"{context.skills.Count()} - Go back to remove items menu");
                possibleAnswer[context.skills.Count()] = context.skills.Count();

                do
                { // Ask which skill to remove
                    userInput = Data.Methods.General.AskForUserInputInt(message, possibleAnswer);
                } while (!skills.ContainsKey(userInput));
                Skill skillToRemove = skills[userInput];

                // Remove skill
                context.skills.Remove(skillToRemove);
            }
        }

        public static void Stats()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                Dictionary<int, Stat> stats = new Dictionary<int, Stat>();
                int cpt = 0;
                int[] possibleAnswer = new int[context.stats.Count() + 1];
                int userInput = -1;
                string message = "Which stat would you like to remove ?";

                foreach (Stat stat in context.stats)
                { // Retrieve the stats
                    stats.Add(cpt, stat);
                    Console.WriteLine($"{cpt} - {stat.Name}");
                    possibleAnswer[cpt] = cpt;
                    cpt++;
                }
                Console.WriteLine($"{context.stats.Count()} - Go back to remove items menu");
                possibleAnswer[context.stats.Count()] = context.stats.Count();

                do
                { // Ask which stat to remove
                    userInput = Data.Methods.General.AskForUserInputInt(message, possibleAnswer);
                } while (!stats.ContainsKey(userInput));
                Stat statToRemove = stats[userInput];

                // Remove stat
                context.stats.Remove(statToRemove);
            }
        }

        public static void Performances()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                Dictionary<int, Performance> performances = new Dictionary<int, Performance>();
                int cpt = 0;
                int[] possibleAnswer = new int[context.performances.Count() + 1];
                int userInput = -1;
                string message = "Which performance would you like to remove ?";

                foreach (Performance performance in context.performances)
                { // Retrieve the performances
                    performances.Add(cpt, performance);
                    Console.WriteLine($"{cpt} - {performance.Name}");
                    possibleAnswer[cpt] = cpt;
                    cpt++;
                }
                Console.WriteLine($"{context.performances.Count()} - Go back to remove items menu");
                possibleAnswer[context.performances.Count()] = context.performances.Count();

                do
                { // Ask which performances to remove
                    userInput = Data.Methods.General.AskForUserInputInt(message, possibleAnswer);
                } while (!performances.ContainsKey(userInput));
                Performance performanceToRemove = performances[userInput];

                // Remove equipment
                context.performances.Remove(performanceToRemove);
            }
        }
    }
}
