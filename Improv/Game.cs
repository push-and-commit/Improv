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
                            Team gameToPlay = GameEditor.Methods.GetItems.PlayerByName(games[userInput].Name);
                            GameEditor.Methods.General.LoadGame(gameToPlay.Name, context);

                            TrainingRoomMenu(gameToPlay, context);
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no existing game\n");
                        StartGame(context);
                    }
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

            // Welcome speech
            Console.WriteLine($"Welcome {team.Name} ! I see that you are a new improvisation team.\n" +
                $"First of all, I would like to congratulate you on creating it !\n" +
                $"I love it when people support local theaters, but it takes a lot to participate in it and so much more to develop a new structure !\n" +
                $"So basically, thanks !\n" +
                $"Don't worry, I will be here to guide you in your first steps as a manager of an improvisation team !");

            // Start training room tutorial
            TrainingRoomTutorial(team, context);

            // Launch home screen
            TrainingRoomMenu(team, context);
        }

        public static void TrainingRoomMenu(Team team, ConnectDB context)
        {
            string message;
            int userInput = -1;

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
                    ShopMenu(team, context);
                    break;
                case 2:
                    PerformanceMenu(team, context);
                    break;
                case 3:
                    GoBackHome(team, context);
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
            Console.WriteLine(team.Name);
            foreach (Team t in context.teams)
            {
                ImportDependencies.ImportTeamDependencies(team, context);
                t.DisplaySelf();
            }

            // Shop shop = GameEditor.Methods.GetItems.Shop(team);
            string message;
            int userInput = -1;

            /* Console.WriteLine($"{shop.Name} !\n" +
                $"{shop.Description}");*/

            message = "What would you like to do ?\n" +
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
                    BuyEquipmentMenu(team, context);
                    break;
                case 2:
                    SellEquipmentMenu(team, context);
                    break;
                case 3:
                    RecruitPlayerMenu(team, context);
                    break;
                case 4:
                    SellPlayerMenu(team, context);
                    break;
                case 5:
                    UpgradeTrainingRoomMenu(team, context);
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
                Console.WriteLine("0 - Back to the shop");
                Dictionary<int, Equipment> equipmentList = GameEditor.Methods.ListItems.ListEquipmentsType(equipmentType, context);

                message = $"Your Improv Coins : {team.Money}\n" +
                    $"Choose an item to list their stats";
                userInput = Data.Methods.General.AskForUserInputInt(message , 0, equipmentList.Count);
                if (userInput != 0)
                {
                    Equipment equipment = context.equipments.Include(dbEquipment => dbEquipment.Stats).FirstOrDefault(dbEquipment => dbEquipment.Id == equipmentList[userInput].Id);
                    equipment.DisplaySelf();
                    message = $"Would you like to buy {equipment.Name} ?";
                    isSure = Data.Methods.General.AskYesNo(message);
                    if (isSure && team.Money >= equipment.Price)
                    {
                        team = context.teams.Include(dbTeam => dbTeam.Inventory).FirstOrDefault(dbTeam => dbTeam.InventoryId == team.InventoryId);
                        if (team.Inventory.Equipments.Count < team.Inventory.NbItemsMax)
                        {
                            team.BuyEquipment(equipment);
                            context.SaveChanges();
                            Console.WriteLine($"Congrats ! You just bought {equipment.Name} !");
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
            string message;
            int userInput = -1;
            bool isSure = false;

            do
            {
                int cpt = 1;
                Dictionary<int, Equipment> equipments = new Dictionary<int, Equipment>();
                foreach (Equipment equipment in team.Equipments)
                {
                    equipments.Add(cpt, equipment);
                    cpt++;
                }
                foreach (Equipment equipment in team.Inventory.Equipments)
                {
                    equipments.Add(cpt, equipment);
                    cpt++;
                }

                if (equipments.Count > 0)
                {
                    Console.WriteLine("0 - Back to the shop");
                    foreach (KeyValuePair<int, Equipment> equipment in equipments)
                    {
                        Console.WriteLine($"{equipment.Key} - {equipment.Value.Name}");
                    }

                    message = "Which item would you like to sell ?\n";
                    userInput = Data.Methods.General.AskForUserInputInt(message, 0, equipments.Count);
                    if (userInput != 0)
                    {
                        double sellPrice = Math.Ceiling(equipments[userInput].Price * 0.8);
                        message = $"You are about to sell {equipments[userInput].Name} for {sellPrice} Improv Coins.\nAre you sure ?";
                        isSure = Data.Methods.General.AskYesNo(message);
                        if (isSure)
                        {
                            team.SellEquipment(equipments[userInput]);
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
            List<Player> playerList = GameEditor.Methods.GetItems.GetDefaultPlayers(context);
            Dictionary<int, Player> playerDictionary = new Dictionary<int, Player>();

            if (playerList.Count > 0)
            {
                foreach (Player player in playerList)
                {
                    if (team.Players.Contains(player))
                    {
                        playerList.Remove(player);
                    }
                }

                if (playerList.Count > 0)
                {
                    do
                    {
                        Console.WriteLine("0 - Back to shop");
                        cpt = 1;
                        foreach (Player player in playerList)
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
                            }
                            playerDictionary.Add(cpt, player);
                            Console.WriteLine($"{cpt} - {player.Name} | {price} Improv Coins");
                        }

                        message = "Choose a player to see their stats";
                        userInput = Data.Methods.General.AskForUserInputInt(message, 0, playerList.Count);

                        if (userInput != 0)
                        {
                            Player virtualPlayer = playerDictionary[userInput];
                            virtualPlayer.DisplaySelf();

                            message = "Would you like to recruit them ?";
                            isSure = Data.Methods.General.AskYesNo(message);
                            if (isSure && team.Money >= price)
                            {
                                Player player = GameEditor.Methods.AddItems.CreatePlayer(virtualPlayer.Name, virtualPlayer.Level, virtualPlayer.Equipments, virtualPlayer.Stats, virtualPlayer.Inventory, virtualPlayer.Age, team, virtualPlayer.Type, context);
                                team.Money -= price;
                                Console.WriteLine($"Congrats ! You just recruited {player.Name} for {price} Improv Coins !");
                            }
                            else if (team.Money < price)
                            {
                                Console.WriteLine($"You don't have enough Improv Coins to recruit {virtualPlayer.Name}");
                            }
                        }

                    } while (userInput != 0);
                }
            }
        }

        public static void SellPlayerMenu(Team team, ConnectDB context)
        {
            string message;
            int userInput = -1;
            int cpt = 1;
            int price = 0;
            bool isSure = false;

            do
            {
                Dictionary<int, Player> playerDictionary = new Dictionary<int, Player>();

                Console.WriteLine("0 - Back to shop");
                foreach (Player player in team.Players)
                {
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
                    }

                    message = $"You are about to sell {player.Values.ElementAt(0).Name} for {price} Improv Coins\n" +
                        $"Are you sure ?";
                    isSure = Data.Methods.General.AskYesNo(message);
                    if(isSure)
                    {
                        player.Values.ElementAt(0).leaveTeam(team);
                        team.Money += price;
                        Console.WriteLine($"With regrets, {player.Values.ElementAt(0).Name} has left your team. You earned {price} Improv Coins.");
                    }
                }
            } while (userInput != 0);
        }

        public static void UpgradeTrainingRoomMenu(Team team, ConnectDB context)
        {
            string message;
            bool isSure = false;

            message = "Would you like to upgrade your Training room ?";
            isSure = Data.Methods.General.AskYesNo(message);
            int price = 5000 * team.TrainingRoom.Level;

            if (isSure && team.Money >= price)
            {
                team.TrainingRoom.LevelUp();
            }
            else if (team.Money < price)
            {
                Console.WriteLine("You don't have enough Improv Coins to upgrade your Training room");
            }
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
            int userInput = -1;
            int cpt = 1;
            Dictionary<int, string> performanceTypes = new Dictionary<int, string>();

            Console.WriteLine("0 - Back");
            foreach(string name in Enum.GetNames(typeof(PerformanceTypeEnum))){
                performanceTypes.Add(cpt, Data.Methods.General.AddSpaceBeforeUppercase(name));
                Console.WriteLine($"{cpt} - {name}");
                cpt++;
            }
            message = "Which performance would you like to do ?";
            userInput = Data.Methods.General.AskForUserInputInt(message, 0, Enum.GetNames(typeof(PerformanceTypeEnum)).Count());

            if (userInput != 0)
            {
                string name = Data.Methods.General.RemoveSpaces(performanceTypes[userInput]);
                if (Enum.GetNames(typeof(PerformanceTypeEnum)).Contains(name))
                {
                    // Next to code : Performance menus with "string name"
                }
            }
            else
            {
                Console.WriteLine("Hope to see you again soon !");
                TrainingRoomMenu(team, context);
            }
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
            string message;
            int userInput = -1;

            foreach (Player player in team.Players)
            {
                player.Stats.FirstOrDefault(powerStat => powerStat.Stat.Name == "Fatigue").Power = 100;
            }

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

        public static void Fight(Team team, Performance performance, Audience audience, ConnectDB context)
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
                        "2 - Quit the performance", 0, 2);

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
                                    "1 - Quit the performance", 0, 1);
                                switch (userInput)
                                {
                                    case 0:
                                        DoImprov(team, audience, audienceConverted, audienceNotConverted, audienceAngry);
                                        break;
                                    case 1:
                                        isPerformanceQuit = QuitPerformance(team, audience.Prize.Money);
                                        if (!isPerformanceQuit)
                                        {
                                            userInput = Data.Methods.General.AskForUserInputInt("What do you wish to do ?\n" +
                                                "0 - Do the next improv\n" +
                                                "1 - Quit the performance", 0, 1);
                                        }
                                        break;
                                }
                            } while (1 != 1);
                            break;
                        case 2:
                            QuitPerformance(team, audience.Prize.Money);
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
                "1 - No", 0, 1);
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
            foreach (Player player in team.Players)
            {
                message += $"\n{cpt} - {player.Name}";
                cpt++;
            }
            userInput = Data.Methods.General.AskForUserInputInt(message, 0, team.Players.Count - 1);
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
            foreach (Skill skill in player.Skills)
            {
                message += $"\n{cpt} - {skill.Name}";
                cpt++;
            }
            userInput = Data.Methods.General.AskForUserInputInt(message, 0, player.Skills.Count - 1);
            return player.Skills.ElementAt(userInput);
        }




















        public static void PerformBattle(Team team, Audience audience, Performance performance)
        {
            if (performance.Type == Data.Enums.PerformanceTypeEnum.Match)
            {
                Team playerTeam = team;
                Team aiTeam = team;
                MatchPerformance(playerTeam, aiTeam, audience, performance);
            }
            else
            {
                ClassicPerformance(team, audience, performance);
            }
        }

        public static void ClassicPerformance(Team team, Audience audience, Performance performance)
        {
            // Définir les variables de contrôle
            int audienceSatisfaction = 0;
            int maxTurns = performance.Duration;
            int currentTurn = 0;

            int action = -1;

            // Continue tant qu'il reste des tours à jouer
            while (currentTurn < maxTurns)
            {
                Console.WriteLine($"--- Tour {currentTurn + 1} ---");

                // Tour de chaque joueur dans l'équipe
                foreach (var player in team.Players)
                {
                    Console.WriteLine($"Tour du joueur : {player.Name}");

                    // Choisir entre un Equipment consommable ou un Skill
                    action = GetPlayerAction(player);  // Demander l'action du joueur
                    if (action == 0)
                    {
                        Equipment consumable = GetConsumableFromPlayer(player);  // Choisir un consommable
                        Console.WriteLine($"{player.Name} uses {consumable.Name}");
                        audienceSatisfaction += HandleEquipmentUsage(consumable);  // Ajouter un effet de l'équipement
                    }
                    else if (action == 1)
                    {
                        Skill skill = GetSkillFromPlayer(player);  // Choisir un skill
                        Console.WriteLine($"{player.Name} utilise le skill {skill.Name}");
                        audienceSatisfaction += HandleSkillUsage(skill);  // Ajouter un effet du skill
                    }
                }

                // Tour de l'Audience
                Console.WriteLine("Tour de l'Audience...");

                // Si l'équipe a utilisé un consumable, l'audience utilise le skill "nothing"
                if (action == 0)
                {
                    Console.WriteLine("L'audience réagit avec le skill 'nothing'.");
                }
                // Si l'équipe a utilisé un skill, l'audience utilise le skill "applause"
                else if (action == 1)
                {
                    Console.WriteLine("L'audience réagit avec le skill 'applause'.");
                    audienceSatisfaction += 10;  // Exemple d'effet positif de l'applaudissement
                }

                currentTurn++;  // Passer au tour suivant
            }

            // Calcul final de la performance
            if (audienceSatisfaction >= 80)
            {
                Console.WriteLine("L'équipe a gagné la performance !");
                team.Money += audience.Prize.Money;
                team.Experience += audience.Prize.Experience;
                team.Money += performance.Prize.Money;
                team.Experience += performance.Prize.Experience;
            }
            else
            {
                Console.WriteLine($"Your Team did not succed to satisfy {audience.Name}'s audience. You lost {audience.Prize.Money / 2} Improv Coins.");
                team.Money -= audience.Prize.Money / 2;
            }
        }

        public static void MatchPerformance(Team playerTeam, Team aiTeam, Audience audience, Performance performance)
        {
            // Code the match
        }

        // Méthode pour obtenir l'action d'un joueur (Equipment ou Skill)
        private static int GetPlayerAction(Player player)
        {
            // Demander à l'utilisateur s'il souhaite utiliser un equipment ou un skill
            string message = "Choisir une action : (1) Utiliser un Equipment consommable, (2) Utiliser un Skill";
            int userInput = Data.Methods.General.AskForUserInputInt(message, 0, 1);
            return userInput;
        }

        // Méthode pour obtenir un équipement consommable du joueur
        private static Equipment GetConsumableFromPlayer(Player player)
        {
            // Logique pour choisir un équipement consommable
            // Exemple de choix d'équipement : renvoyer le premier consumable de la liste
            return player.Equipments.FirstOrDefault(equip => equip.Type == Data.Enums.EquipmentTypeEnum.Consumable);
        }

        // Méthode pour obtenir un skill du joueur
        private static Skill GetSkillFromPlayer(Player player)
        {
            // Logique pour choisir un skill
            // Exemple de choix de skill : renvoyer le premier skill de la liste
            return player.Skills.FirstOrDefault();
        }

        // Gérer l'utilisation d'un équipement consommable
        private static int HandleEquipmentUsage(Equipment equipment)
        {
            // Exemple d'effet d'un équipement consommable sur l'audience
            return 5;  // Ajouter un bonus à la satisfaction de l'audience
        }

        // Gérer l'utilisation d'un skill
        private static int HandleSkillUsage(Skill skill)
        {
            // Exemple d'effet d'un skill sur l'audience
            return skill.Power;  // Ajouter un bonus en fonction de la puissance du skill
        }


























    }
}
