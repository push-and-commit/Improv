using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Store;
using Data.People;
using Data.Values;
using Data.Methods;
using Castle.DynamicProxy;
using static System.Collections.Specialized.BitVector32;
using GameEditor;
using GameEditor.Methods;
using System.Diagnostics;
using System.Numerics;
using Data.Enums;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Improv.Methods
{
    public class Game
    {
        public static void StartGame(ConnectDB context)
        {
            int userInput = MainMenu(context);
            switch (userInput)
            {
                case 0:
                    Console.WriteLine("Bye !");
                    break;
                case 1:
                    InitializeNewGame(context);
                    break;
                case 2:
                    Dictionary<int, Team> games = GameEditor.Methods.General.GetPlayerGames(context);
                    if (games.Count > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("0 - Back to main menu");
                        foreach (KeyValuePair<int, Team> game in games)
                        {
                            Console.WriteLine($"{game.Key} - {game.Value.Name}");
                        }

                        userInput = Data.Methods.General.AskForUserInputInt("Choose an existing game", 0, games.Count);

                        if (userInput == 0)
                        {
                            StartGame(context);
                        }
                        else
                        {
                            Team gameToPlay = GameEditor.Methods.GetItems.TeamByName(games[userInput].Name);

                            TrainingRoomMenu(gameToPlay, context);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nThere are no existing game\n");
                        StartGame(context);
                    }
                    break;
                default:
                    Console.WriteLine("Unsupported scenario");
                    break;
            }
        }

        public static int MainMenu(ConnectDB context)
        {
            int userInput;

            string message = "Welcome to Improv !\n" +
                "Do you wish to start a new game or to continue an existing one ?\n" +
                "0 - Quit the game\n" +
                "1 - New game\n" +
                "2 - Load a game";
            userInput = Data.Methods.General.AskForUserInputInt(message, 0, 2);

            return userInput;
        }

        public static void InitializeNewGame(ConnectDB context)
        {
            string name;
            bool isValidatedName = false;
            string slogan;
            bool isValidatedSlogan = false;
            int userInput;
            string message;

            do
            { // Define team's name
                message = "What is the name of your team ?\n" +
                    "My choice";
                name = Data.Methods.General.AskForUserInput(message);
                if (GameEditor.Methods.CheckItems.TeamNameAlreadyExists(name, context))
                {
                    Console.WriteLine("This name is already taken. Please choose another one");
                }
                else
                {
                    message = $"Your name is {name} ?";
                    isValidatedName = Data.Methods.General.AskYesNo(message);
                }
            } while (!isValidatedName);

            do
            { // Define team's moto
                message = "What is the moto of your team ?\n" +
                    "My choice";
                slogan = Data.Methods.General.AskForUserInput(message);
                message = $"Your moto is {slogan}";
                isValidatedSlogan = Data.Methods.General.AskYesNo(message);
            } while (!isValidatedSlogan);

            // Create new game in database
            Team team = GameEditor.Methods.General.CreateNewGame(name, slogan, context);

            /*// Welcome speech
            Console.WriteLine($"Welcome {team.Name} ! I see that you are a new improvisation team.\n" +
                $"First of all, I would like to congratulate you on creating it !\n" +
                $"I love it when people support local theaters, but it takes a lot to participate in it and so much more to develop a new structure !\n" +
                $"So basically, thanks !\n" +
                $"Don't worry, I will be here to guide you in your first steps as a manager of an improvisation team !");

            // Start training room tutorial
            TrainingRoomTutorial(team, context);*/

            // Launch home screen
            TrainingRoomMenu(team, context);
        }

        public static void TrainingRoomMenu(Team team, ConnectDB context)
        {
            string message;
            int userInput = -1;

            Console.WriteLine();
            message = $"Hello {team.Name} ! What would you like to do ?\n" +
                $"0 - Quit the game\n" +
                $"1 - Go to the shop\n" +
                $"2 - Go on a performance\n" +
                $"3 - Go back home";
            userInput = Data.Methods.General.AskForUserInputInt(message, 0, 3);

            switch (userInput)
            {
                case 0:
                    Console.WriteLine($"Your journey with {team.Name} stops for now. Hope to see you soon !");
                    StartGame(context);
                    break;
                case 1:
                    Console.WriteLine();
                    ShopMenu(team, context);
                    TrainingRoomMenu(team, context);
                    break;
                case 2:
                    Console.WriteLine();
                    PerformanceMenu(team, context);
                    TrainingRoomMenu(team, context);
                    break;
                case 3:
                    Console.WriteLine();
                    GoBackHome(team, context);
                    TrainingRoomMenu(team, context);
                    break;
                default:
                    Console.WriteLine("Unsupported scenario");
                    break;
            }
        }

        /*///////////////////////////
         * 
         * Start Shop menus
         * 
         /////////////////////////*/

        public static void ShopMenu(Team team, ConnectDB context)
        {
            // Reload team to get the training room
            team = Data.Methods.GetItems.LoadTeam(team, context);
            Shop shop = team.TrainingRoom.Shop;

            string message;
            int userInput = -1;

            Console.WriteLine($"{shop.Name} !\n" +
                $"{shop.Description}");

            message = "\nWhat would you like to do ?\n" +
                "0 - Back to Training room\n" +
                "1 - Buy an equipment\n" +
                "2 - Sell an equipment\n" +
                "3 - Recruit a player\n" +
                "4 - Sell a player\n" +
                "5 - Upgrade your training room";
            userInput = Data.Methods.General.AskForUserInputInt(message, 0, 5);
            
            switch (userInput)
            {
                case 0:
                    Console.WriteLine("Thanks for stopping by ! Hope to see you soon !");
                    TrainingRoomMenu(team, context);
                    break;
                case 1:
                    Console.WriteLine();
                    BuyEquipmentMenu(team, context);
                    ShopMenu(team, context);
                    break;
                case 2:
                    Console.WriteLine();
                    SellEquipmentMenu(team, context);
                    ShopMenu(team, context);
                    break;
                case 3:
                    Console.WriteLine();
                    RecruitPlayerMenu(team, context);
                    ShopMenu(team, context);
                    break;
                case 4:
                    Console.WriteLine();
                    SellPlayerMenu(team, context);
                    ShopMenu(team, context);
                    break;
                case 5:
                    Console.WriteLine();
                    UpgradeTrainingRoomMenu(team, context);
                    ShopMenu(team, context);
                    break;
                default:
                    Console.WriteLine("Unsupported scenario");
                    break;
            }
        }
        
        public static void BuyEquipmentMenu(Team team, ConnectDB context)
        {
            string message = "What would you like to buy ?\n" +
                "0 - Back\n" +
                "1 - Player equipment\n" +
                "2 - Team equipment\n" +
                "3 - Consumable";
            int userInput = -1;
            string equipmentType = "";

            do
            {
                userInput = Data.Methods.General.AskForUserInputInt(message, 0, 3);
                switch (userInput)
                {
                    case 0:
                        ShopMenu(team, context);
                        break;
                    case 1:
                        equipmentType = "Player";
                        break;
                    case 2:
                        equipmentType = "Team";
                        break;
                    case 3:
                        equipmentType = "Consumable";
                        break;
                    default:
                        Console.WriteLine("Unsupported scenario");
                        break;
                }

                BuyEquipments(team, equipmentType, context);
            } while (userInput != 0);
        }

        public static void BuyEquipments(Team team, string equipmentType, ConnectDB context)
        {
            string message;
            int userInput = -1;
            bool isSure = false;

            do
            {
                Console.WriteLine();
                Console.WriteLine("0 - Back to the shop");
                Dictionary<int, Equipment> equipmentList = GameEditor.Methods.ListItems.ListEquipmentsType(equipmentType, context);
                Console.WriteLine();

                message = $"My Improv Coins : {team.Money}\n" +
                    $"Choose an item to list their stats";
                userInput = Data.Methods.General.AskForUserInputInt(message , 0, equipmentList.Count);
                if (userInput != 0)
                {
                    Equipment equipment = context.equipments.Include(dbEquipment => dbEquipment.Stats).FirstOrDefault(dbEquipment => dbEquipment.Id == equipmentList[userInput].Id);
                    Console.WriteLine();
                    equipment.DisplaySelf();
                    Console.WriteLine();
                    message = $"Would you like to buy {equipment.Name} for {equipment.Price} Improv Coins ?";
                    isSure = Data.Methods.General.AskYesNo(message);
                    if (isSure && team.Money >= equipment.Price)
                    {
                        team = context.teams.Include(dbTeam => dbTeam.Inventory).FirstOrDefault(dbTeam => dbTeam.InventoryId == team.InventoryId);
                        if (team.Inventory.Equipments.Count < team.Inventory.NbItemsMax)
                        {
                            Equipment newEquipment = new Equipment(equipment, false);
                            context.equipments.Add(newEquipment);
                            team.BuyEquipment(newEquipment);
                            context.Update(team);
                            Console.WriteLine($"Congrats ! You just bought {equipment.Name} !");
                            context.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine("Your inventory is full !");
                        }
                    }
                    else if(isSure && team.Money < equipment.Price)
                    {
                        Console.WriteLine($"You don't have enough Improv Coins to buy {equipment.Name}. {equipment.Price} Improv Coins needed !");
                    }
                }
                else
                {
                    Console.WriteLine("Hope to see you soon !");
                }
            } while (userInput != 0);

        }

        public static void SellEquipmentMenu(Team team, ConnectDB context)
        {
            team = Data.Methods.GetItems.LoadTeam(team, context);
            string message;
            int userInput = -1;
            bool isSure = false;
            do
            {
                int cpt = 1;
                Dictionary<int, Equipment> equipments = new Dictionary<int, Equipment>();
                context.teams.Entry(team).Collection(dbTeam => dbTeam.Equipments).Load();
                foreach (Equipment equipment in team.Equipments)
                {
                    equipments.Add(cpt, equipment);
                    cpt++;
                }
                context.teams.Entry(team).Reference(dbTeam => dbTeam.Inventory).Load();
                foreach (Equipment equipment in team.Inventory.Equipments)
                {
                    equipments.Add(cpt, equipment);
                    cpt++;
                }

                if (equipments.Count > 0)
                {
                    double sellPrice = -1;
                    Console.WriteLine();
                    Console.WriteLine("0 - Back to the shop");
                    foreach (KeyValuePair<int, Equipment> equipment in equipments)
                    {
                        sellPrice = equipment.Value.Price * 0.8;
                        Console.WriteLine($"{equipment.Key} - {equipment.Value.Name} | {sellPrice} Improv Coins");
                    }
                    Console.WriteLine();

                    message = $"My Improv Coins : {team.Money}\n" +
                    $"Which item would you like to sell ?";
                    userInput = Data.Methods.General.AskForUserInputInt(message, 0, equipments.Count);
                    if (userInput != 0)
                    {
                        sellPrice = Math.Ceiling(equipments[userInput].Price * 0.8);
                        message = $"You are about to sell {equipments[userInput].Name} for {sellPrice} Improv Coins.\n" +
                            $"Are you sure ?";
                        isSure = Data.Methods.General.AskYesNo(message);
                        if (isSure)
                        {
                            team.SellEquipment(equipments[userInput]);
                            team.Inventory.Equipments.Remove(equipments[userInput]);
                            context.Update(team);
                            context.SaveChanges();
                            Console.WriteLine($"You earned {sellPrice} Improv Coins");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("There are no items to sell. Feel free to buy some in the shop !");
                    userInput = 0;
                }
            } while(userInput != 0);
        }

        public static void RecruitPlayerMenu(Team team, ConnectDB context)
        {
            string message;
            int userInput = -1;
            int cpt;
            int price = 0;
            bool isSure = false;

            do
            {
                Dictionary<int, Player> playerDictionary = new Dictionary<int, Player>();
                List<Player> playerList = GameEditor.Methods.GetItems.GetDefaultPlayers(context);
                List<Player> playersNotInTeamList = GameEditor.Methods.GetItems.GetDefaultPlayers(context);
                context.teams.Entry(team).Collection(dbTeam => dbTeam.Players).Load();

                if (playerList.Count > 0)
                {
                    foreach (Player player in playerList)
                    {
                        if (team.Players.Contains(player))
                        {
                            playersNotInTeamList.Remove(player);
                        }
                    }

                    if (playersNotInTeamList.Count > 0)
                    {
                        Console.WriteLine("0 - Back to shop");
                        cpt = 1;
                        foreach (Player player in playersNotInTeamList)
                        {
                            switch (player.Type)
                            {
                                case Data.Enums.PlayerTypeEnum.Starter:
                                    price = 50;
                                    break;
                                case Data.Enums.PlayerTypeEnum.Common:
                                    price = 300;
                                    break;
                                case Data.Enums.PlayerTypeEnum.Rare:
                                    price = 1000;
                                    break;
                                case Data.Enums.PlayerTypeEnum.Epic:
                                    price = 15000;
                                    break;
                                default:
                                    Console.WriteLine("Unsupported scenario");
                                    break;
                            }
                            playerDictionary.Add(cpt, player);
                            Console.WriteLine($"{cpt} - {player.Name} | {price} Improv Coins");
                            cpt++;
                        }

                        message = "Choose a player to see their stats";
                        userInput = Data.Methods.General.AskForUserInputInt(message, 0, playerList.Count);

                        if (userInput != 0)
                        {
                            Player player = playerDictionary[userInput];
                            player.DisplaySelf();

                            message = "Would you like to recruit them ?";
                            isSure = Data.Methods.General.AskYesNo(message);
                            if (isSure && team.Money >= price)
                            {
                                Team defaultTeam = context.teams.FirstOrDefault(dbTeam => dbTeam.Name == "Default");
                                player.leaveTeam(defaultTeam);
                                player.joinTeam(team);
                                team.Money -= price;
                                Console.WriteLine($"Congrats ! You just recruited {player.Name} for {price} Improv Coins !");
                            }
                            else if (team.Money < price)
                            {
                                Console.WriteLine($"You don't have enough Improv Coins to recruit {player.Name}");
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Every improvisator in this game are in your team. Well done !");
                        Console.WriteLine();
                        userInput = 0;
                    }
                }
            } while (userInput != 0);
        }

        public static void SellPlayerMenu(Team team, ConnectDB context)
        {
            string message;
            int userInput = -1;
            int cpt = -1;
            int price = 0;
            bool isSure = false;

            do
            {
                cpt = 1;
                Dictionary<int, Player> playerDictionary = new Dictionary<int, Player>();

                Console.WriteLine("0 - Back to shop");
                foreach (Player player in team.Players)
                {
                    playerDictionary.Add(cpt, player);
                    Console.WriteLine($"{cpt} - {player.Name}");
                    cpt++;
                }

                message = "Which player would you like to sell ?";
                userInput = Data.Methods.General.AskForUserInputInt(message, 0, team.Players.Count);

                if(userInput != 0)
                {
                    Dictionary<int, Player> player = new Dictionary<int, Player>();
                    player.Add(0, playerDictionary[userInput]);
                    switch (player.Values.ElementAt(0).Type)
                    {
                        case Data.Enums.PlayerTypeEnum.Starter:
                            price = 30;
                            break;
                        case Data.Enums.PlayerTypeEnum.Common:
                            price = 200;
                            break;
                        case Data.Enums.PlayerTypeEnum.Rare:
                            price = 800;
                            break;
                        case Data.Enums.PlayerTypeEnum.Epic:
                            price = 12000;
                            break;
                        default:
                            Console.WriteLine("Unsupported scenario");
                            break;
                    }

                    message = $"You are about to sell {player.Values.ElementAt(0).Name} for {price} Improv Coins\n" +
                        $"Are you sure ?";
                    isSure = Data.Methods.General.AskYesNo(message);
                    if(isSure)
                    {
                        player.Values.ElementAt(0).leaveTeam(team);
                        team.Money += price;
                        Console.WriteLine($"With regrets, {player.Values.ElementAt(0).Name} has left your team...\n" +
                            $"You earned {price} Improv Coins.");
                    }
                }
            } while (userInput != 0);
        }

        public static void UpgradeTrainingRoomMenu(Team team, ConnectDB context)
        {
            TrainingRoom trainingRoom = team.TrainingRoom;
            context.trainingRooms.Entry(trainingRoom).Collection(dbTrainingRoom => dbTrainingRoom.Stats).Load();
            string message;
            bool isSure = false;
            int price = 5000 * team.TrainingRoom.Level;

            do
            {
                Console.WriteLine();
                Console.WriteLine("Next level stats :");
                team.TrainingRoom.DisplayNextLevelStats();
                Console.WriteLine();

                message = $"Your Improv Coins : {team.Money}" +
                    $"\nWould you like to upgrade your Training room for {price} ?";
                isSure = Data.Methods.General.AskYesNo(message);

                if (isSure && team.Money >= price)
                {
                    team.TrainingRoom.LevelUp();
                    context.Update(team.TrainingRoom);
                    context.SaveChanges();
                    Console.WriteLine("Wow ! Your training room is now looking way better. Every player in your team thank you");
                }
                else if (team.Money < price)
                {
                    Console.WriteLine("You don't have enough Improv Coins to upgrade your Training room");
                }
            } while (isSure);
        }

        /*///////////////////////////
         * 
         * End Shop menus
         * 
         /////////////////////////*/

        /*///////////////////////////
         * 
         * Start Performance menus
         * 
         /////////////////////////*/
         
        public static void PerformanceMenu(Team team, ConnectDB context)
        {
            string message;
            int userInputTypePerformance = -1;
            int userInputMatch = -1;
            int userInputAudience = -1;
            int userInputPerformance = -1;
            int cpt = 1;
            Dictionary<int, string> performanceTypes = new Dictionary<int, string>();

            do
            {
                Console.WriteLine("0 - Back");
                foreach(string name in Enum.GetNames(typeof(PerformanceTypeEnum))) // Each name in PerfoamanceTypeEnum
                {
                    performanceTypes.Add(cpt, name);
                    Console.WriteLine($"{cpt} - {name}");
                    cpt++;
                }
                message = "Which type of performance would you like to do ?";
                userInputTypePerformance = Data.Methods.General.AskForUserInputInt(message, 0, Enum.GetNames(typeof(PerformanceTypeEnum)).Count());

                if (userInputTypePerformance != 0)
                {
                    string performanceName = performanceTypes[userInputTypePerformance];

                    do
                    {
                        Dictionary<int, Audience> audiences = new Dictionary<int, Audience>();
                        cpt = 1;

                        Console.WriteLine();
                        foreach (Audience audience in context.audiences)
                        {
                            context.audiences.Entry(audience).Reference(dbAudience => dbAudience.Prize);
                            audiences.Add(cpt, audience);
                            Console.WriteLine($"{cpt} - {audience.Name} | Prize : {audience.Prize.Money} Improv Coins and {audience.Prize.Experience} experience");
                            cpt++;
                        }
                        message = $"Which audience would you like to perform a {performanceName} for ?";
                        userInputAudience = Data.Methods.General.AskForUserInputInt(message, 0, audiences.Count());

                        if (userInputAudience != 0)
                        {
                            Audience audience = audiences[userInputAudience];
                            do
                            {
                                Dictionary<int, Performance> performances = new Dictionary<int, Performance>();
                                cpt = 1;

                                Console.WriteLine();
                                foreach (Performance performance in context.performances)
                                {
                                    if (performance.Type.ToString() == performanceName)
                                    {
                                        context.performances.Entry(performance).Reference(dbPerformance => dbPerformance.Prize);
                                        performances.Add(cpt, performance);
                                        Console.WriteLine($"{cpt} - {performance.Name} | Prize : {performance.Prize.Money} Improv Coins and {performance.Prize.Experience} experience");
                                        cpt++;
                                    }
                                }
                                message = $"Which performance would you like to do ?";
                                userInputPerformance = Data.Methods.General.AskForUserInputInt(message, 0, performances.Count());

                                if (userInputPerformance != 0)
                                {
                                    Performance performance = performances[userInputPerformance];
                                    Console.WriteLine();
                                    if (performance.Type != PerformanceTypeEnum.Match)
                                    {
                                        message = $"You are about to perform {performance.Name} in front of {audience.Name} for a grand total of {performance.Prize.Money + audience.Prize.Money} Improv Coins and {performance.Prize.Experience + audience.Prize.Experience} experience";
                                        if (Data.Methods.General.AskYesNo(message))
                                        {
                                            doPerformance(team, performance, audience, context);
                                            // Go back to training room
                                            userInputTypePerformance = 0;
                                            userInputAudience = 0;
                                            userInputPerformance = 0;
                                        }
                                    }
                                    else
                                    {
                                        Dictionary<int, Team> matchTeamsDictionary = new Dictionary<int, Team>();
                                        List<Team> matchTeamsList = Data.Methods.GetItems.GetComputerTeams(context);
                                        cpt = 1;
                                        foreach (Team matchTeam in matchTeamsList)
                                        {
                                            matchTeamsDictionary.Add(cpt, matchTeam);
                                            Console.WriteLine($"{cpt} - {matchTeam.Name}");
                                            cpt++;
                                        }

                                        message = $"Against which team would you like to perform ?";
                                        userInputMatch = Data.Methods.General.AskForUserInputInt(message, 1, matchTeamsDictionary.Count);
                                        Team computerTeam = matchTeamsDictionary[userInputMatch];
                                        
                                        message = $"You are about to perform {performance.Name} against {computerTeam.Name} in front of {audience.Name} for a grand total of {performance.Prize.Money + audience.Prize.Money} Improv Coins and {performance.Prize.Experience + audience.Prize.Experience} experience";
                                        if (Data.Methods.General.AskYesNo(message))
                                        {
                                            doMatch(team, computerTeam, performance, audience, context);
                                            // Go back to training room
                                            userInputTypePerformance = 0;
                                            userInputAudience = 0;
                                            userInputPerformance = 0;
                                        }
                                    }
                                }
                            } while (userInputPerformance != 0);
                        }
                    } while (userInputAudience != 0);
                }
                else
                {
                    Console.WriteLine("Hope to see you again soon !");
                    TrainingRoomMenu(team, context);
                }
            } while(userInputTypePerformance != 0);
        }

        public static void doPerformance(Team team, Performance performance, Audience audience, ConnectDB context)
        {
            // General variables
            int userInput = -1;
            string message;
            int playerTurn = 0;
            // Performance variables
            int cptTurn = 0;
            int nbTurn = performance.Duration;
            bool isPerformanceSucceed = false;
            bool isPerformanceQuit = false;
            // Audience variables
            int audienceNumber = audience.Quantity;
            int audienceConvinced = 0;
            int audienceNotConvinced = audience.Quantity;
            int audienceAngry = 0;
            int restToConvince = Convert.ToInt32(Math.Floor(audienceNumber * 0.8)) - audienceConvinced;
            context.audiences.Entry(audience).Reference(dbAudience => dbAudience.Prize).Load();

            Console.WriteLine();
            Console.WriteLine($"Welcome to this new performance of {team.Name} !\n" +
                $"Today, they will perform a {performance.Name}\n" +
                $"Will they be able to conquer the heart of {audience.Quantity} people ?");
            do
            {
                if (playerTurn == 0)
                { // User turn
                    Console.WriteLine();
                    Console.WriteLine($"There are {nbTurn - cptTurn} improvisations left and {audienceNumber - audienceConvinced} people left to convince");
                    message = "0 - Quit the performance\n" +
                        "1 - Do the next improv\n" +
                        "2 - Use a consumable\n" +
                        "What do you wish to do ?";
                    userInput = Data.Methods.General.AskForUserInputInt(message, 0, 2);

                    switch (userInput)
                    {
                        case 0:
                            Console.WriteLine();
                            if (team.Money >= audience.Prize.Money)
                            {
                                isPerformanceQuit = QuitPerformance(team, audience.Prize.Money);
                                if (isPerformanceQuit) break;
                                else continue;
                            }
                            else
                            {
                                Console.WriteLine("You don't have enough money to quit");
                                break;
                            }
                        case 1:
                            Player playerToPlay = ChoosePlayer(team, context);
                            Skill skill = SkillToUse(playerToPlay, context);

                            int angryThreshold = Convert.ToInt32(Math.Floor(0.05 * audienceNotConvinced));
                            if (skill.Power < angryThreshold)
                            { // Was the skill power not enough ?
                                audienceAngry += angryThreshold;
                                Console.WriteLine($"{skill.Name} from {playerToPlay.Name} was not powerful enough...\n" +
                                    $"There are now {audienceAngry} angry people in the audience !");
                            }
                            else
                            {
                                audienceConvinced += skill.Power;
                                audienceNotConvinced -= skill.Power;
                                audienceAngry -= Convert.ToInt32(Math.Ceiling(skill.Power * 0.1));
                                if(audienceAngry < 0) audienceAngry = 0;

                                if (audienceConvinced >= audience.Quantity)
                                { // Has the team won ?
                                    isPerformanceSucceed = true;
                                }
                                else
                                {
                                    restToConvince = Convert.ToInt32(Math.Floor(audienceNumber * 0.8)) - audienceConvinced;

                                    Console.WriteLine($"{playerToPlay.Name} convinces {skill.Power} people in the audience !");
                                }
                            }
                            break;
                        case 2:
                            List<Equipment> consumables = Data.Methods.GetItems.GetConsumables(team, context);
                            if (consumables.Count > 0)
                            {
                                Console.WriteLine();
                                message = "Using a consumable takes a whole improvisation.\n" +
                                    "Are you sure ?";
                                if (Data.Methods.General.AskYesNo(message))
                                {
                                    UseConsumable(team, context);
                                    cptTurn++; // Force cptTurn to iterate as the continue will overpass the later iteration
                                    continue;
                                }
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Your team has no consumables !");
                                break;
                            }
                        default:
                            Console.WriteLine("Unsupported scenario");
                            break;
                    }
                }
                else
                { // Audience turn
                    double threeQuarterAudience = Math.Floor(audienceNumber * 0.6);
                    double halfAudience = Math.Floor(audienceNumber * 0.4);
                    double quarterAudience = Math.Floor(audienceNumber * 0.2);
                    context.audiences.Entry(audience).Collection(dbAudience => dbAudience.Skills).Load();
                    if(audienceConvinced > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"There are {audienceConvinced} convinced people in the audience. They applause, causing your players to gain confidence");
                        Skill applause = context.skills.FirstOrDefault(dbSkill => dbSkill.Name == "Applause");
                        int statBuff = -1;
                        if (audienceConvinced > Convert.ToInt32(threeQuarterAudience))
                        {
                            statBuff = 4;
                        }
                        else if (audienceConvinced > Convert.ToInt32(halfAudience))
                        {
                            statBuff = 3;
                        }
                        else if (audienceConvinced > Convert.ToInt32(quarterAudience))
                        {
                            statBuff = 2;
                        }
                        else
                        {
                            statBuff = 1;
                        }
                        foreach (Player player in team.Players)
                        {
                            context.players.Entry(player).Collection(dbStats => dbStats.Stats).Load();
                            PowerStat confidence = player.Stats.FirstOrDefault(dbStat => dbStat.Stat.Name == "Confidence");
                            Console.WriteLine($"{player.Name} : {confidence.Power} -> {confidence.Power + statBuff}");
                            confidence.Power += statBuff;
                        }
                    }
                    else if (audienceAngry > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"There are {audienceAngry} angry people in the audience. They boo, causing your players to lose confidence");
                        int statNerf = -1;
                        Skill boo = context.skills.FirstOrDefault(dbSkill => dbSkill.Name == "Boo");
                        if (audienceAngry > Convert.ToInt32(threeQuarterAudience))
                        {
                            statNerf = 4;
                        }
                        else if (audienceAngry > Convert.ToInt32(halfAudience))
                        {
                            statNerf = 3;
                        }
                        else if (audienceAngry > Convert.ToInt32(quarterAudience))
                        {
                            statNerf = 2;
                        }
                        else
                        {
                            statNerf = 1;
                        }
                        foreach (Player player in team.Players)
                        {
                            context.players.Entry(player).Collection(dbStats => dbStats.Stats).Load();
                            PowerStat confidence = player.Stats.FirstOrDefault(dbStat => dbStat.Stat.Name == "Confidence");
                            Console.WriteLine($"{player.Name} : {confidence.Power} -> {confidence.Power - statNerf}");
                            confidence.Power -= statNerf;
                        }
                    }
                    Skill nothing = context.skills.FirstOrDefault(dbSkill => dbSkill.Name == "Nothing");
                    Console.WriteLine();
                    Console.WriteLine("The audience does not react and awaits the next improvisation");
                }

                // Change playerTurn
                playerTurn = playerTurn == 0 ? 1 : 0;
                if (playerTurn == 1)
                {
                    cptTurn++;
                }
            } while (cptTurn < nbTurn && !isPerformanceSucceed && !isPerformanceQuit);

            if (isPerformanceSucceed)
            {
                int prizeMoney = performance.Prize.Money + audience.Prize.Money;
                int prizeExperience = performance.Prize.Experience + audience.Prize.Experience;
                Console.WriteLine();
                Console.WriteLine($"What a {performance.Type.ToString()} ! You convinced a tough crowd, congrats !\n" +
                    $"Here are your well deserved {performance.Prize.Money + audience.Prize.Money} Improv Coins and {performance.Prize.Experience + audience.Prize.Experience} experience !\n" +
                    $"See you !");
                Console.WriteLine();
                Console.WriteLine($"Improv coins : {team.Money} -> {team.Money + prizeMoney}");
                team.Money += prizeMoney;
                Console.WriteLine($"Experience : {team.Experience} -> {team.Experience + prizeExperience}");
                team.Experience += prizeExperience;
                if (team.Experience > team.ExperienceToLevelUp)
                {
                    Console.WriteLine();
                    team.LevelUp();
                    context.Update(team);
                    context.SaveChanges();
                }
                double playerExperience = performance.Prize.Experience + audience.Prize.Experience / team.Players.Count;
                Console.WriteLine($"Each player that took a part in this brilliant success gain {Convert.ToInt32(Math.Ceiling(playerExperience))} experience");
                foreach (Player player in team.Players)
                {
                    Player currPlayer = context.players.FirstOrDefault(dbPlayer => dbPlayer.Name == player.Name);
                    currPlayer.Experience += Convert.ToInt32(Math.Ceiling(playerExperience));
                    if (player.Experience > player.ExperienceToLevelUp)
                    {
                        Console.WriteLine();
                        player.LevelUp();
                        context.Update(player);
                        context.SaveChanges();
                    }
                }
            }
            else
            {
                context.teams.Entry(team).Collection(dbTeam => dbTeam.Stats).Load();
                Console.WriteLine($"You did not succeed to convince the audience...\n" +
                    $"The audience paid, you get {audience.Prize.Money}\n" +
                    $"The audience left unsatisfied, your reputation is impacted !");
                int power = team.Stats.FirstOrDefault(dbStat => dbStat.Stat.Name == "Reputation").Power;
                int resPower = power - 3;
                if (resPower < 0) resPower = 0;
                else resPower = power;
                Console.WriteLine($"{team.Name}'s reputation {power} -> {resPower}");
                team.Stats.FirstOrDefault(dbStat => dbStat.Stat.Name == "Reputation").Power -= resPower;
            }
        }

        private static void doMatch(Team team, Team computerTeam, Performance performance, Audience audience, ConnectDB context)
        {
            // General variables
            int userInput = -1;
            string message;
            int playerTurn = 0;
            int cpt = 0;
            // Performance variables
            int cptTurn = 0;
            int nbTurn = performance.Duration;
            bool isPerformanceSucceed = false;
            bool isPerformanceQuit = false;
            // Audience variables
            int audienceNumber = audience.Quantity;
            int audienceConvinced = 0;
            int audienceNotConvinced = audience.Quantity;
            int audienceAngry = 0;
            int restToConvince = Convert.ToInt32(Math.Floor(audienceNumber * 0.8)) - audienceConvinced;
            // Compare teams variables
            int audienceConvincedTeam = 0;
            int audienceConvincedComputer = 0;

            context.audiences.Entry(audience).Reference(dbAudience => dbAudience.Prize).Load();

            Console.WriteLine();
            Console.WriteLine($"Welcome to this new improvised match !\n" +
                $"Today, {team.Name} will play against {computerTeam.Name}\n" +
                $"Who will be able to adapt and play with the other team the best way ?\n" +
                $"Who will be able to conquer the heart of {audience.Quantity} people ?");
            do
            {
                if (playerTurn == 0)
                { // User turn
                    Console.WriteLine();
                    Console.WriteLine($"There are {nbTurn - cptTurn} improvisations left and {audienceNumber - audienceConvinced} people left to convince");
                    Console.WriteLine();
                    Console.WriteLine($"{team.Name}'s turn");
                    message = "0 - Quit the performance\n" +
                        "1 - Do the next improv\n" +
                        "2 - Use a consumable\n" +
                        "What do you wish to do ?";
                    userInput = Data.Methods.General.AskForUserInputInt(message, 0, 2);

                    switch (userInput)
                    {
                        case 0:
                            Console.WriteLine();
                            if (team.Money >= audience.Prize.Money)
                            {
                                isPerformanceQuit = QuitPerformance(team, audience.Prize.Money);
                                if (isPerformanceQuit) break;
                                else continue;
                            }
                            else
                            {
                                Console.WriteLine("You don't have enough money to quit");
                                break;
                            }
                        case 1:
                            Player playerToPlay = ChoosePlayer(team, context);
                            Skill skill = SkillToUse(playerToPlay, context);

                            int angryThreshold = Convert.ToInt32(Math.Floor(0.05 * audienceNotConvinced));
                            if (skill.Power < angryThreshold)
                            { // Was the skill power not enough ?
                                audienceAngry += angryThreshold;
                                Console.WriteLine($"{skill.Name} from {playerToPlay.Name} was not powerful enough...\n" +
                                    $"There are now {audienceAngry} angry people in the audience !");
                            }
                            else
                            {
                                // Convert the convinced and angry audience
                                audienceConvinced += skill.Power;
                                if (audienceConvinced > audienceNumber)
                                {
                                    audienceConvincedTeam = audienceConvinced - audienceNumber;
                                    audienceConvinced = audienceNumber;
                                }
                                else
                                {
                                    audienceConvincedTeam += skill.Power;
                                }
                                audienceNotConvinced -= skill.Power;
                                if(audienceNotConvinced < 0)
                                {
                                    audienceNotConvinced = 0;
                                }
                                audienceAngry -= Convert.ToInt32(Math.Ceiling(skill.Power * 0.1));
                                if (audienceAngry < 0) audienceAngry = 0;

                                if (audienceConvinced >= audienceNumber)
                                { // Has the team won ?
                                    isPerformanceSucceed = true;
                                }
                                else
                                {
                                    restToConvince = Convert.ToInt32(Math.Floor(audienceNumber * 0.8)) - audienceConvinced;

                                    Console.WriteLine($"{playerToPlay.Name} convinces {skill.Power} people in the audience !");
                                }
                            }
                            break;
                        case 2:
                            List<Equipment> consumables = Data.Methods.GetItems.GetConsumables(team, context);
                            if (consumables.Count > 0)
                            {
                                Console.WriteLine();
                                message = "Using a consumable takes a whole improvisation.\n" +
                                    "Are you sure ?";
                                if (Data.Methods.General.AskYesNo(message))
                                {
                                    UseConsumable(team, context);
                                    cptTurn++; // Force cptTurn to iterate as the continue will overpass the later iteration
                                    continue;
                                }
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Your team has no consumables !");
                                break;
                            }
                        default:
                            Console.WriteLine("Unsupported scenario");
                            break;
                    }
                }
                else if (playerTurn == 1)
                { // Computer turn
                    Console.WriteLine();
                    Console.WriteLine($"{computerTeam.Name}'s turn");
                    context.teams.Entry(computerTeam).Collection(dbTeam => dbTeam.Players).Load();
                    Dictionary<int, Player> computerPlayers = new Dictionary<int, Player>();
                    cpt = 0;
                    foreach(Player player in computerTeam.Players)
                    {
                        computerPlayers.Add(cpt, player);
                        cpt++;
                    }
                    Random random = new Random();
                    double randomNumberPlayer = random.Next(computerPlayers.Count - 1);
                    Player playerToPlay = computerPlayers[Convert.ToInt32(Math.Floor(randomNumberPlayer))];

                    context.players.Entry(playerToPlay).Collection(dbPlayer => dbPlayer.Skills).Load();
                    Dictionary<int, Skill> playerSkills = new Dictionary<int, Skill>();
                    cpt = 0;
                    foreach (Skill playerSkill in playerToPlay.Skills)
                    {
                        playerSkills.Add(cpt, playerSkill);
                        cpt++;
                    }
                    double randomNumberSkill = random.Next(playerSkills.Count - 1);
                    Skill skillToUse = playerSkills[Convert.ToInt32(Math.Floor(randomNumberSkill))];

                    int angryThreshold = Convert.ToInt32(Math.Floor(0.05 * audienceNotConvinced));
                    if (skillToUse.Power < angryThreshold)
                    { // Was the skill power not enough ?
                        audienceAngry += angryThreshold;
                        Console.WriteLine($"{skillToUse.Name} from {playerToPlay.Name} was not powerful enough...\n" +
                            $"There are now {audienceAngry} angry people in the audience !");
                    }
                    else
                    {
                        // Convert the convinced and angry audience
                        audienceConvinced += skillToUse.Power;
                        if (audienceConvinced > audienceNumber)
                        {
                            audienceConvincedComputer = audienceConvinced - audienceNumber;
                            audienceConvinced = audienceNumber;
                        }
                        else
                        {
                            audienceConvincedComputer += skillToUse.Power;
                        }
                        audienceNotConvinced -= skillToUse.Power;
                        if (audienceNotConvinced < 0)
                        {
                            audienceNotConvinced = 0;
                        }
                        audienceAngry -= Convert.ToInt32(Math.Ceiling(skillToUse.Power * 0.1));
                        if (audienceAngry < 0) audienceAngry = 0;

                        Console.WriteLine($"{playerToPlay.Name} convinces {skillToUse.Power} people in the audience !");

                        if (audienceConvinced >= audience.Quantity)
                        { // Has the computer won ?
                            isPerformanceSucceed = true;
                        }
                        else
                        {
                            restToConvince = Convert.ToInt32(Math.Floor(audienceNumber * 0.8)) - audienceConvinced;
                        }
                    }
                }
                else
                { // Audience turn
                    double threeQuarterAudience = Math.Floor(audienceNumber * 0.6);
                    double halfAudience = Math.Floor(audienceNumber * 0.4);
                    double quarterAudience = Math.Floor(audienceNumber * 0.2);
                    context.audiences.Entry(audience).Collection(dbAudience => dbAudience.Skills).Load();
                    if (audienceConvinced > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"There are {audienceConvinced} convinced people in the audience. They applause, causing your players to gain confidence");
                        Skill applause = context.skills.FirstOrDefault(dbSkill => dbSkill.Name == "Applause");
                        int statBuff = -1;
                        if (audienceConvinced > Convert.ToInt32(threeQuarterAudience))
                        {
                            statBuff = 4;
                        }
                        else if (audienceConvinced > Convert.ToInt32(halfAudience))
                        {
                            statBuff = 3;
                        }
                        else if (audienceConvinced > Convert.ToInt32(quarterAudience))
                        {
                            statBuff = 2;
                        }
                        else
                        {
                            statBuff = 1;
                        }
                        Console.WriteLine();
                        Console.WriteLine($"{team.Name} :");
                        foreach (Player player in team.Players)
                        {
                            context.players.Entry(player).Collection(dbStats => dbStats.Stats).Load();
                            PowerStat confidence = player.Stats.FirstOrDefault(dbStat => dbStat.Stat.Name == "Confidence");
                            Console.WriteLine($"{player.Name} : {confidence.Power} -> {confidence.Power + statBuff}");
                            confidence.Power += statBuff;
                        }
                        Console.WriteLine();
                        Console.WriteLine($"{computerTeam.Name} :");
                        foreach (Player player in computerTeam.Players)
                        {
                            context.players.Entry(player).Collection(dbStats => dbStats.Stats).Load();
                            PowerStat confidence = player.Stats.FirstOrDefault(dbStat => dbStat.Stat.Name == "Confidence");
                            Console.WriteLine($"{player.Name} : {confidence.Power} -> {confidence.Power + statBuff}");
                            confidence.Power += statBuff;
                        }
                    }
                    else if (audienceAngry > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"There are {audienceAngry} angry people in the audience. They boo, causing your players to lose confidence");
                        int statNerf = -1;
                        Skill boo = context.skills.FirstOrDefault(dbSkill => dbSkill.Name == "Boo");
                        if (audienceAngry > Convert.ToInt32(threeQuarterAudience))
                        {
                            statNerf = 4;
                        }
                        else if (audienceAngry > Convert.ToInt32(halfAudience))
                        {
                            statNerf = 3;
                        }
                        else if (audienceAngry > Convert.ToInt32(quarterAudience))
                        {
                            statNerf = 2;
                        }
                        else
                        {
                            statNerf = 1;
                        }
                        Console.WriteLine($"{team.Name} :");
                        foreach (Player player in team.Players)
                        {
                            context.players.Entry(player).Collection(dbStats => dbStats.Stats).Load();
                            PowerStat confidence = player.Stats.FirstOrDefault(dbStat => dbStat.Stat.Name == "Confidence");
                            Console.WriteLine($"{player.Name} : {confidence.Power} -> {confidence.Power - statNerf}");
                            confidence.Power -= statNerf;
                        }
                        Console.WriteLine($"{computerTeam.Name} :");
                        foreach (Player player in computerTeam.Players)
                        {
                            context.players.Entry(player).Collection(dbStats => dbStats.Stats).Load();
                            PowerStat confidence = player.Stats.FirstOrDefault(dbStat => dbStat.Stat.Name == "Confidence");
                            Console.WriteLine($"{player.Name} : {confidence.Power} -> {confidence.Power - statNerf}");
                            confidence.Power -= statNerf;
                        }
                    }
                    Skill nothing = context.skills.FirstOrDefault(dbSkill => dbSkill.Name == "Nothing");
                    Console.WriteLine();
                    Console.WriteLine("The audience does not react and awaits the next improvisation");
                }

                // Change playerTurn
                playerTurn = (playerTurn + 1) % 3;
                if (playerTurn == 2)
                {
                    cptTurn++;
                }
            } while (cptTurn < nbTurn && !isPerformanceSucceed && !isPerformanceQuit);

            if (isPerformanceSucceed)
            {
                int prizeMoney = performance.Prize.Money + audience.Prize.Money;
                int prizeExperience = performance.Prize.Experience + audience.Prize.Experience;

                // Determine who wins
                if (audienceConvincedTeam > audienceConvincedComputer)
                { // Player wins
                    Console.WriteLine();
                    Console.WriteLine($"What a {performance.Type.ToString()} ! You convinced a tough crowd, congrats !\n" +
                        $"Here are your well deserved {performance.Prize.Money + audience.Prize.Money} Improv Coins and {performance.Prize.Experience + audience.Prize.Experience} experience !\n" +
                        $"See you !");
                    Console.WriteLine();
                    Console.WriteLine($"Improv coins : {team.Money} -> {team.Money + prizeMoney}");
                    team.Money += prizeMoney;
                    Console.WriteLine($"Experience : {team.Experience} -> {team.Experience + prizeExperience}");
                    team.Experience += prizeExperience;
                    if (team.Experience > team.ExperienceToLevelUp)
                    {
                        Console.WriteLine();
                        team.LevelUp();
                        context.Update(team);
                    }
                    double playerExperience = performance.Prize.Experience + audience.Prize.Experience / team.Players.Count;
                    Console.WriteLine($"Each player that took a part in this brilliant success gain {Convert.ToInt32(Math.Ceiling(playerExperience))} experience");
                    foreach (Player player in team.Players)
                    {
                        Player currPlayer = context.players.FirstOrDefault(dbPlayer => dbPlayer.Name == player.Name);
                        currPlayer.Experience += Convert.ToInt32(Math.Ceiling(playerExperience));
                        if (player.Experience > player.ExperienceToLevelUp)
                        {
                            Console.WriteLine();
                            player.LevelUp();
                            context.Update(player);
                            context.SaveChanges();
                        }
                    }
                }
                else if (audienceConvincedComputer > audienceConvincedTeam)
                { // Computer wins
                    Console.WriteLine();
                    Console.WriteLine($"Even if {team.Name} gave eveything they got, {computerTeam.Name} were more appreciated by the audience\n" +
                        $"Better luck next time !");

                    double consolationPrize = prizeExperience * 0.1;
                    double lostPrize = prizeMoney * 0.5;
                    Console.WriteLine();
                    Console.WriteLine($"As a consolation prize, you earned {Convert.ToInt32(Math.Ceiling(consolationPrize))}");
                    team.Experience += Convert.ToInt32(Math.Ceiling(consolationPrize));
                    if (team.Experience > team.ExperienceToLevelUp)
                    {
                        Console.WriteLine();
                        team.LevelUp();
                        context.Update(team);
                        context.SaveChanges();
                    }
                    Console.WriteLine($"However, you lost {Convert.ToInt32(Math.Ceiling(lostPrize))}");
                    team.Money -= Convert.ToInt32(Math.Ceiling(lostPrize));
                    if( team.Money < 0 ) team.Money = 0;
                    context.Update(team);
                    context.SaveChanges();
                }
                else
                { // Draw
                    int halfPrizeExperience = Convert.ToInt32(Math.Ceiling(prizeExperience * 0.5));
                    int halfPrizeMoney = Convert.ToInt32(Math.Ceiling(prizeMoney * 0.5));
                    Console.WriteLine();
                    Console.WriteLine($"Both {team.Name} and {computerTeam.Name} did an amazing job tonight !\n" +
                        $"No winners, no losers, both teams win half the prize !");
                    Console.WriteLine();
                    Console.WriteLine($"You earned {halfPrizeMoney} Improv Coins and {halfPrizeExperience} experience !");
                    team.Experience += halfPrizeExperience;
                    Console.WriteLine();
                    Console.WriteLine($"Improv coins : {team.Money} -> {team.Money + halfPrizeMoney}");
                    team.Money += prizeMoney;
                    Console.WriteLine($"Experience : {team.Experience} -> {team.Experience + halfPrizeExperience}");
                    if (team.Experience > team.ExperienceToLevelUp)
                    {
                        Console.WriteLine();
                        team.LevelUp();
                        context.Update(team);
                        context.SaveChanges();
                    }
                    team.Money += halfPrizeMoney;
                    context.Update(team);
                    context.SaveChanges();
                }
            }
            else
            {
                Console.WriteLine($"Neither {team.Name} nor {computerTeam.Name} were good enough to convice this extremely tough corwd !\n" +
                    $"A little more training needs to be done before facing this audience again !");
            }
        }

        public static bool QuitPerformance(Team team, int priceToQuit)
        {
            bool isQuitted = Data.Methods.General.AskYesNo($"You will lose {priceToQuit} Improv Coins if you quit");
            if (isQuitted)
            {
                isQuitted = true;
                Console.WriteLine($"You lost {priceToQuit} Improv Coins by quitting the performance");
            }
            return isQuitted;
        }

        public static Player ChoosePlayer(Team team, ConnectDB context)
        {
            int userInput = -1;
            Dictionary<int, Player> players = new Dictionary<int, Player>();
            // Choose a player to do the current improv
            int cpt = 1;
            context.teams.Entry(team).Collection(dbTeam => dbTeam.Players).Load();
            Console.WriteLine();
            foreach (Player player in team.Players)
            {
                players.Add(cpt, player);
                Console.WriteLine($"{cpt} - {player.Name}");
                cpt++;
            }
            string message = "Choose a player to do the next improv";
            userInput = Data.Methods.General.AskForUserInputInt(message, 1, team.Players.Count);
            return players[userInput];
        }

        public static Skill SkillToUse(Player player, ConnectDB context)
        {
            int userInput;
            string message;
            int cpt = 1;
            Dictionary<int, Skill> skills = new Dictionary<int, Skill>();
            context.players.Entry(player).Collection(dbPlayer => dbPlayer.Skills).Load();
            Console.WriteLine();
            foreach (Skill skill in player.Skills)
            {
                skills.Add(cpt, skill);
                Console.WriteLine($"{cpt} - {skill.Name}");
                cpt++;
            }
            message = "Use a skill";
            userInput = Data.Methods.General.AskForUserInputInt(message, 1, player.Skills.Count);
            return skills[userInput];
        }

        public static void UseConsumable(Team team, ConnectDB context)
        {
            context.teams.Entry(team).Collection(dbTeam => dbTeam.Players).Load();
            int userInput = -1;
            string message;
            int cpt = 1;
            bool isSure = false;
            do
            {
                Console.WriteLine();
                Dictionary<int, Player> players = new Dictionary<int, Player>();
                foreach (Player player in team.Players)
                {
                    players.Add(cpt, player);
                    Console.WriteLine($"{cpt} - {player.Name}");
                    cpt++;
                }
                message = "On which player would you like to use a consumable";
                userInput = Data.Methods.General.AskForUserInputInt(message, 1, players.Count);
                Player playerToRegenerate = players[userInput];

                Dictionary<int, Equipment> consumableDictionary = new Dictionary<int, Equipment>();
                List<Equipment> consumableList = Data.Methods.GetItems.GetConsumables(team, context);
                cpt = 1;
                Console.WriteLine();
                foreach (Equipment consumable in consumableList)
                {
                    consumableDictionary.Add(cpt, consumable);
                    Console.WriteLine($"{cpt} - {consumable.Name}");
                    cpt++;
                }
                message = $"Which consumable would you like to use on {playerToRegenerate.Name} ?\n" +
                    $"Choose a consumable to see their stats";
                userInput = Data.Methods.General.AskForUserInputInt(message, 1, consumableDictionary.Count);
                Equipment consumableToUse = consumableDictionary[userInput];
                context.equipments.Entry(consumableToUse).Collection(dbEquipment => dbEquipment.Stats).Load();
                consumableToUse.DisplaySelf();

                message = $"You are about to use {consumableToUse.Name} on {playerToRegenerate.Name}\n" +
                    $"Are you sure ?";
                isSure = Data.Methods.General.AskYesNo(message);
                if (isSure)
                {
                    playerToRegenerate.UseConsumable(consumableToUse);
                }
            } while (!isSure);

        }

        /*///////////////////////////
         * 
         * End Performance menus
         * 
         /////////////////////////*/

        /*///////////////////////////
         * 
         * Start Go back home menus
         * 
         /////////////////////////*/

        public static void GoBackHome(Team team, ConnectDB context)
        {
            foreach (Player player in team.Players)
            {
                player.Stats.FirstOrDefault(powerStat => powerStat.Stat.Name == "Fatigue").Power = 100;
            }
            context.Update(team.Players);
            context.SaveChanges();

            Console.WriteLine("Your players are now well rested !");
        }

        /*///////////////////////////
         * 
         * End Go back home menus
         * 
         /////////////////////////*/

        /*///////////////////////////
         * 
         * Start Tutorials
         * 
         /////////////////////////*/

        private static void TrainingRoomTutorial(Team team, ConnectDB context)
        {
            Console.WriteLine("This is your training room ! This will be your place and your safe space :)\n" +
                "Here, you will be able to do many things.\n" +
                "Number one : Use the shop. Recruit new players, buy new equipments, sell them if needed, upgrade your training room.\n" +
                "One thing's for sure, you need Improv Coins to do that ! As you are new in this economy and it seems to me that you are the future of improvisation, I will make a small gift to help you start your journey here");
            Console.WriteLine("You earned 1500 Improv Coins");
            Console.WriteLine("Hey ! I just thought about this, but you don't have any players yet, right ? I have just the right people for you !\n" +
                "A group of friends started improvisation recently and are looking to join a team !");

            // Start shop tutorial
            ShopTutorial(team, context);

            Console.WriteLine("Here, you can also go on a performance with your team !");
            // Start performance tutorial
            PerformanceTutorial(team, context);

            Console.WriteLine("After such a long day and great perforamnces, both your player and yourself need a well deserved rest, you can go back home !");
            // Start go back home tutorial
            GoBackHomeTutorial(team, context);
        }

        private static void ShopTutorial(Team team, ConnectDB context)
        {
            string message = "";
            int userInput = Data.Methods.General.AskForUserInputInt(message, 1, 1);
        }

        private static void PerformanceTutorial(Team team, ConnectDB context)
        {
            string message = "";
            int userInput = Data.Methods.General.AskForUserInputInt(message, 2, 2);
        }

        private static void GoBackHomeTutorial(Team team, ConnectDB context)
        {
            string message = "";
            int userInput = Data.Methods.General.AskForUserInputInt(message, 3, 3);
        }

        /*///////////////////////////
         * 
         * End Tutorials
         * 
         /////////////////////////*/
    }
}
