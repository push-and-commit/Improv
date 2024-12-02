using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Store;
using Data.People;
using Data.Values;
using Castle.DynamicProxy;

namespace Improv.Methods
{
    public class Game
    {
        public static void StartGame()
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
                            int.TryParse(Console.ReadLine(), out userInput);
                        } while (!games.ContainsKey(userInput));

                        Team gameToPlay = games.ElementAt(userInput).Value;
                        GameEditor.Methods.General.LoadGame(gameToPlay.Name);


                    }
                    else
                    {
                        Console.WriteLine("There are no existing game\n");
                        StartGame();
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
                isInputANumber = int.TryParse(Console.ReadLine(), out userInput);
                if (userInput is 0 or 1 or 2) isInputAValidChoice = true;
                if (isInputANumber && isInputAValidChoice) isInputValid = true;
            } while (!isInputValid);

            return userInput;
        }

        public static void InitializeNewGame()
        {
            string name;
            bool isValidatedName = false;
            string slogan;
            bool isValidatedSlogan = false;
            int userInput;

            do
            { // Define team's name
                Console.WriteLine("What is the name of your team :");
                Console.Write("My choice : ");
                name = Console.ReadLine();
                userInput = Data.Methods.General.AskForUserInputInt("Your name is " + name + "\nAre you sure ?" +
                    "\n0 - Yes" +
                    "\n1 - No", [0, 1]);
                isValidatedName = userInput == 0 ? true : false;
            } while (!isValidatedName);

            do
            { // Define team's moto
                Console.Write("What is the moto of your team :");
                Console.Write("My choice : ");
                slogan = Console.ReadLine();
                userInput = Data.Methods.General.AskForUserInputInt("Your moto is " + slogan + "\nAre you sure ?" +
                    "\n0 - Yes" +
                    "\n1 - No", [0, 1]);
                isValidatedSlogan = userInput == 0 ? true : false;
            } while (!isValidatedSlogan);

            // Create new game in database
            GameEditor.Methods.General.CreateNewGame(name, slogan);
            // Retrieve the team from the database
            Team team = GameEditor.Methods.General.LoadGame(name);

            // Welcome speech
            Console.WriteLine($"Welcome {team.Name} ! I see that you are a new improvisation team.\n" +
                $"First of all, I would like to congratulate you on creating it !\n" +
                $"I love it when people support local theaters, but it takes a lot to participate in it and so much more to develop a new structure !\n" +
                $"So basically, thanks !\n" +
                $"Don't worry, I will be here to guide you in your first steps as a manager of an improvisation team !");

            // Start training room tutorial
            TrainingRoomTutorial(team);

            // 
        }

        private static void TrainingRoomTutorial(Team team)
        {
            Console.WriteLine("This is your training room ! This will be your place and your safe space\n" +
                "Here, you will be able to do many things.\n" +
                "Number one : Use the store. Recruit new players, buy new equipments, sell them if needed, upgrade your training room.\n" +
                "One thing's for sure, you need Improv Coins to do that ! As you are new in this economy and it seems to me that you are a good person, I will give you 1500 Improv Coins");
            Console.WriteLine("Hey ! I just thought about this, but you don't have any players yet, right ? I have just the right people for you !\n" +
                "A group of friends started improvisation recently and are looking to join a team !");

            // Start store tutorial
            StoreTutorial(team);

            Console.WriteLine("Here, you can also go on a performance with your team !");
            // Start performance tutorial
            PerformanceTutorial(team);

            Console.WriteLine("After such a long day and great perforamnces, both your player and yourself need a well deserved rest, you can go back home !");
            // Start back to home tutorial
            GoingBackHomeTutorial(team);
        }

        private static void StoreTutorial(Team team)
        {
            string message = "";
            int userInput = Data.Methods.General.AskForUserInputInt(message, [0]);
        }

        private static void PerformanceTutorial(Team team)
        {
            string message = "";
            int userInput = Data.Methods.General.AskForUserInputInt(message, [1]);
        }

        private static void GoingBackHomeTutorial(Team team)
        {
            string message = "";
            int userInput = Data.Methods.General.AskForUserInputInt(message, [2]);
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
            bool isPerformanceQuit = false;
            // Audience variables
            int audienceConverted = 0;
            int audienceNotConverted = audience.Quantity;
            int audienceAngry = 0;

            Console.WriteLine($"Welcome to this new performance of {team.Name} !\n" +
                $"Today, they will perform a {performance.Name}\n" +
                $"Will they be able to conquer the heart of {audience.Quantity} people ?");
            do
            {
                if (playerTurn == 1)
                { // User turn
                    userInput = Data.Methods.General.AskForUserInputInt("What do you wish to do ?\n" +
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
                            do
                            { // while not quit or improv done
                                userInput = Data.Methods.General.AskForUserInputInt("What do you wish to do ?\n" +
                                    "0 - Do the next improv\n" +
                                    "1 - Quit the performance", [0, 1]);
                                switch (userInput)
                                {
                                    case 0:
                                        DoImprov(team, audience, audienceConverted, audienceNotConverted, audienceAngry);
                                        break;
                                    case 1:
                                        isPerformanceQuit = QuitPerformance(team, audience.Prize);
                                        if (!isPerformanceQuit)
                                        {
                                            userInput = Data.Methods.General.AskForUserInputInt("What do you wish to do ?\n" +
                                                "0 - Do the next improv\n" +
                                                "1 - Quit the performance", [0, 1]);
                                        }
                                        break;
                                }
                            } while (1 != 1);
                            break;
                        case 2:
                            QuitPerformance(team, audience.Prize);
                            isPerformanceQuit = true;
                            break;
                    }
                }
                else
                { // Audience turn
                    Player playerToPlay;
                }

                // Change playerTurn
                playerTurn = playerTurn == 1 ? 2 : 1;
                cptTurn++;
            } while (cptTurn < nbTurn && audienceConverted < audience.Quantity && !isPerformanceSucceed && !isPerformanceQuit);

            if (isPerformanceSucceed)
            {
                Console.WriteLine("Congratulations ! You won !");
            }
            else
            {
                Console.WriteLine("You lost, looser");
            }

        }

        public static bool QuitPerformance(Team team, int priceToQuit)
        {
            bool isQuitted = false;
            int userInput = Data.Methods.General.AskForUserInputInt("You will lose " + priceToQuit + " Improv Coins if you quit. Are you sure ?\n" +
                "0 - Yes\n" +
                "1 - No", [0, 1]);
            if (userInput == 0)
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
            userInput = Data.Methods.General.AskForUserInputInt(message, nbPlayers);
            Player playerToPlay = team.Players.ElementAt(userInput);

            Array returnValues = null/*{audienceConverted, audienceNotConverted, audienceAngry, isPerformanceSucceed, playerToPlay}*/;

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

        public static Skill SkillToUse(Player player)
        {
            int userInput;
            string message = "Use a skill";
            int cpt = 0;
            int[] nbSkills = new int[player.Skills.Count];
            foreach (Skill skill in player.Skills)
            {
                message += $"\n{cpt} - {skill.Name}";
                nbSkills[cpt] = cpt;
                cpt++;
            }
            userInput = Data.Methods.General.AskForUserInputInt(message, nbSkills);
            return player.Skills.ElementAt(userInput);
        }
    }
}
