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
    public class General
    {
        public General() { }

        public static void Game()
        {
            int userInput = MainMenu();
            switch (userInput)
            {
                case 0:
                    ListItemsMenu();
                    break;
                case 1:
                    AddItemsMenu();
                    break;
                case 2:
                    RemoveItemsMenu();
                    break;
                case 3:
                    Console.WriteLine("Bye !");
                    break;
            }
        }

        public static int MainMenu()
        {

            string message = "Welcome to Improv, Game editor mode !\n" +
                "Please choose an option in the following :\n" +
                "0 - List items\n" +
                "1 - Add an item\n" +
                "2 - Remove an item\n" +
                "3 - Quit the game";
            int userInput = AskForUserInputInt(message, [0, 1, 2, 3]);

            return userInput;
        }

        public static void ListItemsMenu()
        {
            string message = "Which item would you like to list ?\n" +
                "0 - Audiences\n" +
                "1 - Teams\n" +
                "2 - Players\n" +
                "3 - Equipments\n" +
                "4 - Skills\n" +
                "5 - Stats\n" +
                "6 - Performances\n" +
                "7 - Go back to the main menu";
            int userInput = AskForUserInputInt(message, [0, 1, 2, 3, 4, 5, 6, 7]);

            switch (userInput)
            {
                case 0:
                    ListItems.Audiences();
                    ListItemsMenu();
                    break;
                case 1:
                    ListItems.Teams();
                    ListItemsMenu();
                    break;
                case 2:
                    ListItems.Players();
                    ListItemsMenu();
                    break;
                case 3:
                    ListItems.Equipments();
                    ListItemsMenu();
                    break;
                case 4:
                    ListItems.Skills();
                    ListItemsMenu();
                    break;
                case 5:
                    ListItems.Stats();
                    ListItemsMenu();
                    break;
                case 6:
                    ListItems.Performances();
                    ListItemsMenu();
                    break;
                case 7:
                    Game();
                    break;

            }
        }

        public static int AskForUserInputInt(string message, int[] args)
        {
            int userInput;
            bool isInputValid = false;
            bool isInputANumber = false;
            bool isInputAValidChoice = false;

            Console.WriteLine(message);
            do
            {
                Console.Write("My choice : ");
                isInputANumber = int.TryParse(Console.ReadLine(), out userInput);
                if (Array.IndexOf(args, userInput) != -1) isInputAValidChoice = true;
                if (isInputANumber && isInputAValidChoice) isInputValid = true;
            } while (!isInputValid);

            return userInput;
        }

        public static void AddItemsMenu()
        {
            throw new NotImplementedException();
        }

        public static void RemoveItemsMenu()
        {
            throw new NotImplementedException();
        }

        public static void AddEquipment(Equipment[] equipmentArray)
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database

                foreach (Equipment equip in equipmentArray)
                {
                    context.equipments.Add(equip);
                }
                context.SaveChanges();

            }

        }

        public static void RemoveEquipment(Equipment equipment)
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                context.equipments.Remove(equipment);

                context.SaveChanges();
            }

        }

        public static void ListEquipments()
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                foreach (Equipment equipment in context.equipments)
                {
                    Console.WriteLine($"{equipment.Name} is a {equipment.Type} equipment\n" +
                        $"Its description is the following :\n" +
                        $"{equipment.Description}");
                    Console.Write($"It costs {equipment.Price} golds and gives ");
                    foreach (Stat stat in equipment.Stats)
                    {
                        Console.Write($"{stat.Power} points of {stat.Name}");
                    }
                }
            }
        }

        public static void CreateNewGame(string name, string slogan)
        {
            // New team creation
            List<Equipment> equipments = new List<Equipment>();
            List<Stat> stats = new List<Stat>();
            Inventory inventory = new Inventory();
            Team team = new Team(name, 0, equipments, stats, inventory, slogan, 1500);
            using (ConnectDB context = new ConnectDB())
            { // Add the new team to the database
                context.teams.Add(team);
            }

            // New store creation
            string storeName = team.Name + "'s store";
            string storeDescription = "Welcome, buy everything that you need";
            List<Equipment> storeEquipments = new List<Equipment>();
            using (ConnectDB context = new ConnectDB())
            { // Add all equipments to the store
                foreach(Equipment equipment in context.equipments)
                {
                    storeEquipments.Add(equipment);
                }
            }
            Store store = new Store(storeName, storeDescription, storeEquipments, team.TrainingRoom);

            // Adding basic players to the team
            using (ConnectDB context = new ConnectDB())
            { // Add all starter player to the team
                foreach (Player player in context.players)
                {
                    if (player.Type == "starter")
                    {
                        team.Players.Add(player);
                    }
                }
            }
        }

        public static Dictionary<int, Team> GetGames()
        {
            Dictionary<int, Team> games = new Dictionary<int, Team>();

            using (ConnectDB context = new ConnectDB())
            { // Add all games in the database to the dictionary
                int cpt = 0;
                foreach (Team team in context.teams)
                {
                    games.Add(cpt, team);
                }
            }

            return games;
        }

        public static void LoadGame(Team team)
        {
            using (ConnectDB context = new ConnectDB())
            { // Connect to database

            }
        }
    }
}
