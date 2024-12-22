using Data;
using Data.Enums;
using Data.People;
using Data.Store;
using Data.Values;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEditor.Methods
{
    public class General
    {
        public General() { }

        public static void Game(ConnectDB context)
        {
            int userInput = MainMenu();
            switch (userInput)
            {
                case 0:
                    Console.WriteLine("Bye !");
                    break;
                case 1:
                    ListItemsMenu(context);
                    break;
                case 2:
                    AddItemsMenu(context);
                    break;
                case 3:
                    RemoveItemsMenu(context);
                    break;
            }
        }

        public static int MainMenu()
        {

            string message = "Welcome to Improv, Game editor mode !\n" +
                "Please choose an option in the following :\n" +
                "0 - Quit the game\n" +
                "1 - List items\n" +
                "2 - Add an item\n" +
                "3 - Remove an item";
            int userInput = Data.Methods.General.AskForUserInputInt(message, 0, 3);

            return userInput;
        }

        public static void ListItemsMenu(ConnectDB context)
        {
            string message = "Which items would you like to list ?\n" +
                "0 - Go back to the main menu\n" +
                "1 - Audiences\n" +
                "2 - Teams\n" +
                "3 - Players\n" +
                "4 - Equipments\n" +
                "5 - Skills\n" +
                "6 - Stats\n" +
                "7 - Performances";
            int userInput = Data.Methods.General.AskForUserInputInt(message, 0, 7);

            switch (userInput)
            {
                case 0:
                    Game(context);
                    break;
                case 1:
                    ListItems.Audiences(context);
                    ListItemsMenu(context);
                    break;
                case 2:
                    ListItems.Teams(context);
                    ListItemsMenu(context);
                    break;
                case 3:
                    ListItems.Players(context);
                    ListItemsMenu(context);
                    break;
                case 4:
                    ListItems.Equipments(context);
                    ListItemsMenu(context);
                    break;
                case 5:
                    ListItems.Skills(context);
                    ListItemsMenu(context);
                    break;
                case 6:
                    ListItems.Stats(context);
                    ListItemsMenu(context);
                    break;
                case 7:
                    ListItems.Performances(context);
                    ListItemsMenu(context);
                    break;

            }
        }

        public static void AddItemsMenu(ConnectDB context)
        {
            string message = "Which item would you like to add ?\n" +
                "0 - Go back to the main menu\n" +
                "1 - Audience\n" +
                "2 - Team\n" +
                "3 - Player\n" +
                "4 - Equipment\n" +
                "5 - Skill\n" +
                "6 - Stat\n" +
                "7 - Performance";
            int userInput = Data.Methods.General.AskForUserInputInt(message, 0, 7);

            switch (userInput)
            {
                case 0:
                    Game(context);
                    break;
                case 1:
                    AddItems.Audiences(context);
                    AddItemsMenu(context);
                    break;
                case 2:
                    AddItems.Teams(context);
                    AddItemsMenu(context);
                    break;
                case 3:
                    AddItems.Players(context);
                    AddItemsMenu(context);
                    break;
                case 4:
                    AddItems.Equipments(context);
                    AddItemsMenu(context);
                    break;
                case 5:
                    AddItems.Skills(context);
                    AddItemsMenu(context);
                    break;
                case 6:
                    AddItems.Stats(context);
                    AddItemsMenu(context);
                    break;
                case 7:
                    AddItems.Performances(context);
                    AddItemsMenu(context);
                    break;
            }
        }

        public static void RemoveItemsMenu(ConnectDB context)
        {
            string message = "Which item would you like to remove ?\n" +
                "0 - Go back to the main menu\n" +
                "1 - Audience\n" +
                "2 - Team\n" +
                "3 - Player\n" +
                "4 - Equipment\n" +
                "5 - Skill\n" +
                "5 - Stat\n" +
                "7 - Performance ";
            int userInput = Data.Methods.General.AskForUserInputInt(message, 0, 7);

            switch (userInput)
            {
                case 0:
                    Game(context);
                    break;
                case 1:
                    RemoveItems.Audiences(context);
                    RemoveItemsMenu(context);
                    break;
                case 2:
                    RemoveItems.Teams(context);
                    RemoveItemsMenu(context);
                    break;
                case 3:
                    RemoveItems.Players(context);
                    RemoveItemsMenu(context);
                    break;
                case 4:
                    RemoveItems.Equipments(context);
                    RemoveItemsMenu(context);
                    break;
                case 5:
                    RemoveItems.Skills(context);
                    RemoveItemsMenu(context);
                    break;
                case 6:
                    RemoveItems.Stats(context);
                    RemoveItemsMenu(context);
                    break;
                case 7:
                    RemoveItems.Performances(context);
                    RemoveItemsMenu(context);
                    break;
            }
        }

        public static Team CreateNewGame(string name, string slogan, ConnectDB context)
        {
            // New training room creation
            TrainingRoom trainingRoom = AddItems.CreateTrainingRoom(name, context);

            // New Inventory creation
            Inventory inventory = AddItems.CreateInventory(context);

            // New team creation
            Team team = AddItems.CreateTeam(name, inventory, slogan, trainingRoom, TeamTypeEnum.Player, context);

            return team;
        }

        public static List<Player> GetStarterPlayers()
        {
            List<Player> playerList = new List<Player>();
            using (ConnectDB context = new ConnectDB())
            {
                foreach (Player player in context.players)
                {
                    if (player.Type == Data.Enums.PlayerTypeEnum.Starter)
                    {
                        playerList.Add(player);
                    }
                }
            }
            return playerList;
        }

        public static Dictionary<int, Team> GetPlayerGames(ConnectDB context)
        {
            Dictionary<int, Team> games = new Dictionary<int, Team>();

            // Add all games in the database to the dictionary
            if (context.teams.Count() > 0)
            {
                int cpt = 1;
                foreach (Team team in context.teams)
                {
                    if (team.Type == TeamTypeEnum.Player && team.Name != "Default")
                    {
                        games.Add(cpt, team);
                        cpt++;
                    }
                }
            }

            return games;
        }

        public static Team LoadGame(string name, ConnectDB context)
        {
            Team userTeam = null;
            foreach (Team team in context.teams)
            {
                if (team.Name == name)
                {
                    userTeam = team;
                }
            }
            return userTeam;
        }

        public static Impro CreateImpro(string name, string type, ConnectDB context)
        {
            string askMessage;
            string atLeastMessage;
            int level = Data.Methods.General.AskForUserInputInt("Level (min value 1)", 1, int.MaxValue);
            List<Equipment> equipments = new List<Equipment>();

            if (context.equipments.Count() > 0)
            {
                Console.WriteLine("Choose the Equipments to add to their inventory");
                askMessage = "Which item would you like to add ? (\"0 - That's enough\" if none)";
                equipments = AskUserForEquipments(askMessage, context);
            }

            Console.WriteLine("Choose a Stat to add");
            askMessage = "Which stat would you like to add ?";
            atLeastMessage = "An Improv needs at least one Stat to be created !";
            List<PowerStat> stats = AskUserForStats(askMessage, atLeastMessage, name, type, context);

            Inventory inventory = AskUserForInventory(context);

            Impro impro = new Impro(name, level, equipments, stats, inventory);

            return impro;
        }

        public static List<PowerStat> AskUserForStats(string askMessage, string atLeastMessage, string objectName, string objectType, ConnectDB context)
        {
            List<PowerStat> statList = new List<PowerStat>();
            int statPower;
            Dictionary<int, Stat> stats = new Dictionary<int, Stat>();
            int userInput = -1;
            do
            {
                Console.WriteLine("0 - That's enough");
                stats = ListItems.ListStats(context, objectType);
                userInput = Data.Methods.General.AskForUserInputInt(askMessage, 0, stats.Count());
                if (userInput != 0)
                {
                    statPower = Data.Methods.General.AskForUserInputInt("Choose the amount of power the Stat gives (min value 1)", 1, int.MaxValue);
                    PowerStat stat = new PowerStat(stats[userInput], objectName, objectType, statPower);
                    statList.Add(stat);
                }
                if (userInput == 0 && statList.Count() == 0)
                {
                    userInput = -1;
                    Console.WriteLine(atLeastMessage);
                }
            } while (statList.Count() <= stats.Count && userInput != 0);

            return statList;
        }

        public static List<Equipment> AskUserForEquipments(string askMessage, ConnectDB context)
        {
            Dictionary<int, Equipment> listEquipments = new Dictionary<int, Equipment>();
            List<Equipment> equipmentList = new List<Equipment>();
            int userInput = -1;
            do
            {
                Console.WriteLine("0 - That's enough");
                listEquipments = ListItems.ListEquipments(context);
                userInput = Data.Methods.General.AskForUserInputInt(askMessage, 0, listEquipments.Count());
                if (userInput != 0)
                {
                    if (!equipmentList.Contains(listEquipments[userInput]))
                    {
                        equipmentList.Add(listEquipments[userInput]);
                    }
                    else
                    {
                        Console.WriteLine("This Equipment has already been added ! Please choose another one");
                    }
                }
            } while (listEquipments.Count() != equipmentList.Count() && userInput != 0);

            return equipmentList;
        }

        public static Inventory AskUserForInventory(ConnectDB context)
        {
            Console.WriteLine("Inventory :");
            int nbItemsMax = Data.Methods.General.AskForUserInputInt("Number of items it can hold", 0, int.MaxValue);
            Inventory inventory = new Inventory(nbItemsMax);
            Dictionary<int, Equipment> equipmentList = new Dictionary<int, Equipment>();
            int userInput = -1;
            do
            {
                Console.WriteLine("0 - That's enough");
                equipmentList = ListItems.ListEquipments(context);
                userInput = Data.Methods.General.AskForUserInputInt("Choose an Equipment :", 0, equipmentList.Count);

                if (userInput != 0)
                {
                    inventory.AddToInventory(equipmentList[userInput]);
                }
            } while (inventory.Equipments.Count() <= inventory.NbItemsMax && userInput != 0);

            return inventory;
        }

        public static List<Skill> AskUserForSkills(string askMessage, string atLeastMessage, ConnectDB context)
        {
            List<Skill> skillList = new List<Skill>();
            int userInput = -1;

            Dictionary<int, Skill> skills = new Dictionary<int, Skill>();
            do
            {
                Console.WriteLine("0 - That's enough");
                skills = ListItems.ListSkills(context);
                userInput = Data.Methods.General.AskForUserInputInt(askMessage, 0, skills.Count());
                if (userInput != 0)
                {
                    if (!skillList.Contains(skills[userInput]))
                    {
                        skillList.Add(skills[userInput]);
                    }
                    else
                    {
                        Console.WriteLine("This Skill has already been added to the list ! Please choose another one");
                    }
                }
                else if (userInput == 0 && skillList.Count == 0)
                {
                    userInput = -1;
                    Console.WriteLine(atLeastMessage);
                }
            } while (skillList.Count != skills.Count && userInput != 0);

            return skillList;
        }
    }
}