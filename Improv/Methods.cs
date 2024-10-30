using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Store;
using Data.People;
using Castle.DynamicProxy;

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

        public static void Fight(Team team, Audience audience, Performance performance)
        {
            int playerTurn = 1;
            int nbTurn = performance.Duration;
            int audienceConverted = 0;
            int audienceNotConverted = audience.Quantity;
            int cptTurn = 0;
            do
            {
                Console.WriteLine();
                // Change playerTurn
                playerTurn = playerTurn == 1 ? 2 : 1;
                cptTurn++;
            } while(cptTurn < nbTurn && audienceConverted < audience.Quantity);
            
        }
    }
}
