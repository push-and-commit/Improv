using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Store;
using Data.People;
using Castle.DynamicProxy;
using Data.Values;

namespace Improv
{
    public class Methods
    {
        public static void Game()
        {
            int userInput = MainMenu();
            switch (userInput)
            {
                case 0:
                    InitializeNewGame();
                    break;
                case 1:
                    Dictionary<int, Team> games = GameEditor.Methods.General.GetGames();
                    if (games.Count > 0)
                    {
                        Console.WriteLine("Choose an existing game :");
                        foreach (KeyValuePair<int, Team> game in games)
                        {
                            Console.WriteLine($"{game.Key} - {game.Value.Name}");
                        }
                        do
                        {
                            Console.Write("My choice : ");
                            Int32.TryParse(Console.ReadLine(), out userInput);
                        } while (!games.ContainsKey(userInput));

                        Team gameToPlay = games.ElementAt(userInput).Value;
                        GameEditor.Methods.General.LoadGame(gameToPlay);


                    } else
                    {
                        Console.WriteLine("There are no existing game\n");
                        Game();
                    }
                    break;
                case 2:
                    Console.WriteLine("Bye !");
                    break;
            }
        }

        public static int MainMenu()
        {
            int userInput;
            bool isInputValid = false;
            bool isInputANumber = false;
            bool isInputAValidChoice = false;

            Console.WriteLine("Welcome to Improv !\n" +
                "Do you wish to start a new game or to continue an existing one ?\n" +
                "0 - New game\n" +
                "1 - Load a game\n" +
                "2 - Quit the game");
            do
            {
                Console.Write("My choice : ");
                isInputANumber = Int32.TryParse(Console.ReadLine(), out userInput);
                if (userInput is 0 or 1 or 2) isInputAValidChoice = true;
                if (isInputANumber && isInputAValidChoice) isInputValid = true;
            } while (!isInputValid);

            return userInput;
        }

        public static void InitializeNewGame()
        {
            string name;
            string slogan;
            Console.WriteLine("What is the name of your team :");
            Console.Write("My choice : ");
            name = Console.ReadLine();
            Console.Write("What is the moto of your team :");
            slogan = Console.ReadLine();
            GameEditor.Methods.General.CreateNewGame(name, slogan);
        }

        public static void InitializeStore(Team team)
        {
            GameEditor.Methods.General.ListEquipments();
        }

        public static void Fight(Team team, Performance performance, Audience audience)
        {
            // General variables
            int userInput;
            int playerTurn = 1;
            // Performance variables
            int cptTurn = 0;
            int nbTurn = performance.Duration;
            bool isPerformanceSucceed = false;
            bool isPerformanceQuitted = false;
            // Audience variables
            int audienceConverted = 0;
            int audienceNotConverted = audience.Quantity;
            int audienceAngry = 0;

            // Message that will be reused

            Console.WriteLine($"Welcome to this new performance of {team.Name} !\n" +
                $"Today, they will perform a {performance.Name}\n" +
                $"Will they be able to conqauer the heart of people {audience.Quantity} ?");
            do
            {
                if (playerTurn == 1)
                { // User turn
                    userInput = GameEditor.Methods.General.AskForUserInputInt("What do you wish to do ?\n" +
                        "0 - Do the next improv\n" +
                        "1 - Use a consumable\n" +
                        "2 - Quit the performance", [0, 1, 2]);

                    switch (userInput)
                    {
                        case 0:
                            DoImprov(team, audience, audienceConverted, audienceNotConverted, audienceAngry);
                            break;
                        case 1:
                            UseConsumable(team);
                            userInput = GameEditor.Methods.General.AskForUserInputInt("What do you wish to do ?\n" +
                                "0 - Do the next improv\n" +
                                "1 - Quit the performance", [0, 1]);
                            switch (userInput)
                            {
                                case 0:
                                    DoImprov(team, audience, audienceConverted, audienceNotConverted, audienceAngry);
                                    break;
                                case 1:
                                    isPerformanceQuitted = QuitPerformance(team, audience.Prize);
                                    if (!isPerformanceQuitted)
                                    {
                                        userInput = GameEditor.Methods.General.AskForUserInputInt("What do you wish to do ?\n" +
                                            "0 - Do the next improv\n" +
                                            "1 - Quit the performance", [0, 1]);
                                    }
                                    break;
                            }
                            break;
                        case 2:
                            QuitPerformance(team, audience.Prize);
                            isPerformanceQuitted = true;
                            break;
                    }
                } else
                { // Audience turn
                    
                }

                // Change playerTurn
                playerTurn = playerTurn == 1 ? 2 : 1;
                cptTurn++;
            } while(cptTurn < nbTurn && audienceConverted < audience.Quantity && !isPerformanceSucceed && !isPerformanceQuitted);

            if (isPerformanceSucceed)
            {
                Console.WriteLine("Congratulations ! You won !");
            } else
            {
                Console.WriteLine("You lost, looser");
            }
            
        }

        public static bool QuitPerformance(Team team, int priceToQuit)
        {
            bool isQuitted = false;
            int userInput = GameEditor.Methods.General.AskForUserInputInt("Are you sure ?\n" +
                "0 - Yes\n" +
                "1 - No", [0, 1]);
            if(userInput == 0)
            {
                if (team.Money < priceToQuit)
                {
                    Console.WriteLine("You don't have enough money to quit");
                }
                else
                {
                    team.Money -= priceToQuit;
                    Console.WriteLine($"You lost {priceToQuit} by quitting the performance");
                    isQuitted = true;
                }
            }
            return isQuitted;
        }

        public static void UseConsumable(Team team)
        {
            throw new NotImplementedException();
        }

        public static Array DoImprov(Team team, Audience audience, int audienceConverted, int audienceNotConverted, int audienceAngry)
        {
            int userInput;
            bool isPerformanceSucceed = false;
            // Choose a player to do the current improv
            string message = "Choose a player to do the next improv";
            int cpt = 0;
            int[] nbPlayers = new int[team.Players.Count];
            foreach (Player player in team.Players)
            {
                message += $"\n{cpt} - {player.Name}";
                nbPlayers[cpt] = cpt;
                cpt++;
            }
            userInput = GameEditor.Methods.General.AskForUserInputInt(message, nbPlayers);
            Player playerToPlay = team.Players.ElementAt(userInput);

            Array returnValues = {audienceConverted, audienceNotConverted, audienceAngry, isPerformanceSucceed};

            Skill skill = SkillToUse(playerToPlay);
            audienceConverted += skill.Power;

            if (audienceConverted >= audience.Quantity)
            { // Has the team won ?
                isPerformanceSucceed = true;
                return returnValues;
            }

            if (skill.Power < Math.Ceiling(0.05 * audienceNotConverted))
            { // Was the skill power not enough ?
                audienceAngry += Convert.ToInt32(Math.Ceiling(0.05 * audienceNotConverted));
            }

            audienceNotConverted -= skill.Power;

            return returnValues;
        }

        public static Skill SkillToUse(Player playerToPlay)
        {
            int userInput;
            string message = "Use a skill";
            int cptSkill = 0;
            int[] nbSkills = new int[playerToPlay.Skills.Count];
            foreach (Skill skill in playerToPlay.Skills)
            {
                message += $"\n{cptSkill} - {skill.Name}";
                nbSkills[cptSkill] = cptSkill;
                cptSkill++;
            }
            userInput = GameEditor.Methods.General.AskForUserInputInt(message, nbSkills);
            return playerToPlay.Skills.ElementAt(userInput);
        }
    }
}
