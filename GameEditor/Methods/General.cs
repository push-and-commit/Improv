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
            int userInput = Data.Methods.General.AskForUserInputInt(message, 0, 3);

            return userInput;
        }

        public static void ListItemsMenu()
        {
            string message = "Which items would you like to list ?\n" +
                "0 - Audiences\n" +
                "1 - Teams\n" +
                "2 - Players\n" +
                "3 - Equipments\n" +
                "4 - Skills\n" +
                "5 - Stats\n" +
                "6 - Performances\n" +
                "7 - Go back to the main menu";
            int userInput = Data.Methods.General.AskForUserInputInt(message, 0, 7);

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

        public static void AddItemsMenu()
        {
            string message = "Which item would you like to add ?\n" +
                "0 - Audience\n" +
                "1 - Team\n" +
                "2 - Player\n" +
                "3 - Equipment\n" +
                "4 - Skill\n" +
                "5 - Stat\n" +
                "6 - Performance\n" +
                "7 - Go back to the main menu";
            int userInput = Data.Methods.General.AskForUserInputInt(message, 0, 7);

            switch (userInput)
            {
                case 0:
                    AddItems.Audiences();
                    AddItemsMenu();
                    break;
                case 1:
                    AddItems.Teams();
                    AddItemsMenu();
                    break;
                case 2:
                    AddItems.Players();
                    AddItemsMenu();
                    break;
                case 3:
                    AddItems.Equipments();
                    AddItemsMenu();
                    break;
                case 4:
                    AddItems.Skills();
                    AddItemsMenu();
                    break;
                case 5:
                    AddItems.Stats();
                    AddItemsMenu();
                    break;
                case 6:
                    AddItems.Performances();
                    AddItemsMenu();
                    break;
                case 7:
                    Game();
                    break;
            }
        }

        public static void RemoveItemsMenu()
        {
            string message = "Which item would you like to remove ?\n" +
                "0 - Audience\n" +
                "1 - Team\n" +
                "2 - Player\n" +
                "3 - Equipment\n" +
                "4 - Skill\n" +
                "5 - Stat\n" +
                "6 - Performance\n" +
                "7 - Go back to the main menu";
            int userInput = Data.Methods.General.AskForUserInputInt(message, 0, 7);

            switch (userInput)
            {
                case 0:
                    RemoveItems.Audiences();
                    RemoveItemsMenu();
                    break;
                case 1:
                    RemoveItems.Teams();
                    RemoveItemsMenu();
                    break;
                case 2:
                    RemoveItems.Players();
                    RemoveItemsMenu();
                    break;
                case 3:
                    RemoveItems.Equipments();
                    RemoveItemsMenu();
                    break;
                case 4:
                    RemoveItems.Skills();
                    RemoveItemsMenu();
                    break;
                case 5:
                    RemoveItems.Stats();
                    RemoveItemsMenu();
                    break;
                case 6:
                    RemoveItems.Performances();
                    RemoveItemsMenu();
                    break;
                case 7:
                    Game();
                    break;
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
                    Console.Write($"It costs {equipment.Price} Improv Coins and gives ");
                    foreach (PowerStat stat in equipment.Stats)
                    {
                        Console.Write($" {stat.Power} points of {stat.Stat.Name}");
                    }
                }
            }
        }

        public static Team CreateNewGame(string name, string slogan)
        {
            // New training room creation
            TrainingRoom trainingRoom = AddItems.CreateTrainingRoom();

            // New Inventory creation
            Inventory inventory = AddItems.CreateInventory();

            // New store creation
            Store store = AddItems.CreateStore(name, trainingRoom);

            // New team creation
            Team team = AddItems.CreateTeam(name, inventory, slogan, trainingRoom, TeamTypeEnum.Player);

            return team;
        }

        public static List<Player> GetStarterPlayers()
        {
            List<Player> playerList = new List<Player>();
            using(ConnectDB context = new ConnectDB())
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

        public static Dictionary<int, Team> GetPlayerGames()
        {
            Dictionary<int, Team> games = new Dictionary<int, Team>();

            using (ConnectDB context = new ConnectDB())
            { // Add all games in the database to the dictionary
                int cpt = 0;
                foreach (Team team in context.teams)
                {
                    if (team.Type == TeamTypeEnum.Player)
                    {
                        games.Add(cpt, team);
                        cpt++;
                    }
                }
            }

            return games;
        }

        public static Team LoadGame(string name)
        {
            Team userTeam = null;
            using (ConnectDB context = new ConnectDB())
            { // Connect to database
                foreach (Team team in context.teams)
                {
                    if (team.Name == name)
                    {
                        userTeam = team;
                    }
                }
            }
            return userTeam;
        }

        public static Impro CreateImpro(ConnectDB context, string name, string type)
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
                stats = ListItems.ListStats(context);
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


        public static void LoadDB()
        {
            using (ConnectDB context = new ConnectDB())
            {
                // Team statistics
                Stat nbPlayers = new Stat("Number of players", "Number of players the training room can hold", StatTypeEnum.TrainingRoom);
                Stat solidarity = new Stat("Solidarity", "Team's solidarity", StatTypeEnum.Team);
                Stat exhaustion = new Stat("Fatigue", "Player's fatigue", StatTypeEnum.Player);
                context.stats.Add(nbPlayers);
                context.stats.Add(solidarity);
                context.stats.Add(exhaustion);
                context.SaveChanges();

                nbPlayers = context.stats.FirstOrDefault(stat => stat.Name == "Number of players");
                solidarity = context.stats.FirstOrDefault(stat => stat.Name == "Solidarity");
                exhaustion = context.stats.FirstOrDefault(stat => stat.Name == "Fatigue");

                List<Stat> statList = new List<Stat> { nbPlayers, solidarity, exhaustion };

                // PowerStats
                List<PowerStat> powerStatList = new List<PowerStat>();

                // Prizes
                Prize prize = new Prize(150, 150);
                context.prizes.Add(prize);
                context.SaveChanges();

                prize = context.prizes.FirstOrDefault(prize => prize.Money == 150 && prize.Experience == 150);

                // Skill costs
                CostStat costExhaustion = new CostStat(exhaustion, 1, exhaustion.Name, SkillTypeEnum.Player);
                context.costStats.Add(costExhaustion);
                context.SaveChanges();

                costExhaustion = context.costStats.FirstOrDefault(costStat => costStat.Stat.Name == "Fatigue");

                List<CostStat> cost = new List<CostStat> { costExhaustion };

                // Skills
                Skill punchline = new Skill("Punchline", "A joke that triggers laughter", SkillTypeEnum.Player, 10, cost);
                Skill applause = new Skill("Applause", "The audience applauds, increasing satisfaction", SkillTypeEnum.Audience, 10, cost);
                context.skills.Add(punchline);
                context.skills.Add(applause);
                context.SaveChanges();

                punchline = context.skills.FirstOrDefault(skill => skill.Name == "Punchline");
                applause = context.skills.FirstOrDefault(skill => skill.Name == "Applause");

                List<Skill> playerSkillList = new List<Skill> { punchline };
                List<Skill> audienceSkillList = new List<Skill> { applause };

                // Training room
                PowerStat powerNbPlayers = new PowerStat(nbPlayers, "Training Room", "TrainingRoom", 15);
                context.powerStats.Add(powerNbPlayers);
                context.SaveChanges();
                powerNbPlayers = context.powerStats.FirstOrDefault(powerStat => powerStat.ObjectName == "Training Room" && powerStat.ObjectType == "TrainingRoom");

                List<PowerStat> trainingRoomPowerStatList = new List<PowerStat> { powerNbPlayers };

                TrainingRoom trainingRoom = new TrainingRoom("Training Room", "Your first training room", 1, trainingRoomPowerStatList);
                context.trainingRooms.Add(trainingRoom);
                context.SaveChanges();

                trainingRoom = context.trainingRooms.FirstOrDefault(trainingRoom => trainingRoom.Name == "Training Room");

                // Equipments
                PowerStat powerEquipment = new PowerStat(exhaustion, "Timer", "Equipment", 15);
                context.powerStats.Add(powerEquipment);
                context.SaveChanges();
                powerEquipment = context.powerStats.FirstOrDefault(powerStat => powerStat.ObjectName == "Training Room" && powerStat.ObjectType == "TrainingRoom");

                List<PowerStat> equipmentPowerStatList = new List<PowerStat> { powerEquipment };
                Equipment timer = new Equipment("Timer", "Timer to manage performances", 1, EquipmentTypeEnum.Team, equipmentPowerStatList, 1);
                Equipment shirt = new Equipment("Vest", "Player's uniform", 0, EquipmentTypeEnum.Player, equipmentPowerStatList, 1);
                Equipment fruit = new Equipment("Fruit", "Healthy snack to maintain energy", 1, EquipmentTypeEnum.Consumable, equipmentPowerStatList, 1);
                context.equipments.Add(timer);
                context.equipments.Add(shirt);
                context.equipments.Add(fruit);
                context.SaveChanges();

                timer = context.equipments.FirstOrDefault(equipment => equipment.Name == "Timer");
                shirt = context.equipments.FirstOrDefault(equipment => equipment.Name == "Vest");
                fruit = context.equipments.FirstOrDefault(equipment => equipment.Name == "Fruit");

                List<Equipment> equipmentList = new List<Equipment> { timer, shirt, fruit };

                // Store
                Store store = new Store("Store", "Your first store", equipmentList, trainingRoom);
                context.stores.Add(store);
                context.SaveChanges();

                store = context.stores.FirstOrDefault(store => store.Name == "Store");

                // Inventory
                Inventory inventory = new Inventory(1);
                context.inventories.Add(inventory);
                context.SaveChanges();

                inventory = context.inventories.FirstOrDefault(inventory => inventory.NbItemsMax == 1);

                // Team
                PowerStat powerSolidarity = new PowerStat(solidarity, "Improv", "Team", 25);
                context.powerStats.Add(powerSolidarity);
                context.SaveChanges();
                powerSolidarity = context.powerStats.FirstOrDefault(powerStat => powerStat.ObjectName == "Improv" && powerStat.ObjectType == "Team");

                List<PowerStat> teamPowerStatList = new List<PowerStat> { powerSolidarity };

                Team team = new Team("Improv", 1, equipmentList, teamPowerStatList, inventory, "Improvisation forever", 1500, trainingRoom, TeamTypeEnum.Player);
                context.teams.Add(team);
                context.SaveChanges();

                team = context.teams.FirstOrDefault(team => team.Name == "Improv");

                // Player
                PowerStat powerExhaustion = new PowerStat(exhaustion, "Oliver", "Player", 15);
                context.powerStats.Add(powerExhaustion);
                context.SaveChanges();
                powerExhaustion = context.powerStats.FirstOrDefault(powerStat => powerStat.ObjectName == "Oliver" && powerStat.ObjectType == "Player");

                List<PowerStat> playerPowerStatList = new List<PowerStat> { powerExhaustion };

                Player player = new Player("Oliver", 1, equipmentList, playerPowerStatList, inventory, 20, team, PlayerTypeEnum.Starter);
                context.players.Add(player);
                context.SaveChanges();

                player = context.players.FirstOrDefault(player => player.Name == "Oliver");

                // Audience
                Audience audience = new Audience("Paris", "The largest audience in France", 1, 10, prize, audienceSkillList);
                context.audiences.Add(audience);
                context.SaveChanges();

                audience = context.audiences.FirstOrDefault(audience => audience.Name == "Paris");

                // Performance
                Performance performance = new Performance("Performance", "A basic performance", 10, 4, PerformanceTypeEnum.Catch, prize, equipmentList);
                context.performances.Add(performance);
                context.SaveChanges();

                performance = context.performances.FirstOrDefault(performance => performance.Name == "Performance");

                Console.WriteLine("Database successfully initialized.");
            }
        }

    }
}
