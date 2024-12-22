using Data.People;
using Data.Store;
using Data.Values;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection.Emit;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Data.Enums;
using System.Xml.Linq;

namespace GameEditor.Methods
{
    public class AddItems
    {
        public static void Audiences(ConnectDB context)
        {
            string name;
            string description;
            int level;
            int quantity;
            Prize prize;
            List<Skill> playerSkills = new List<Skill>();

            string message;
            int userInput = -1;

            if (context.skills.Count() > 0)
            {
                Console.WriteLine("An audience needs certain parameters. Please, fill in the followings :");
                bool alreadyExists = false;
                do
                {
                    name = Data.Methods.General.AskForUserInput("Name");
                    if (context.audiences.FirstOrDefault(audience => audience.Name == name) != null)
                    {
                        Console.WriteLine("An Audience with this name already exists. Please chosse another one");
                        alreadyExists = true;
                    }
                    else
                    {
                        alreadyExists = false;
                    }
                } while (alreadyExists);
                description = Data.Methods.General.AskForUserInput("Description");
                level = Data.Methods.General.AskForUserInputInt("Level (min value 1) : ", 1, int.MaxValue);
                quantity = Data.Methods.General.AskForUserInputInt("Number of people in the audience (min value 1) : ", 1, int.MaxValue);
                int prizeMoney = Data.Methods.General.AskForUserInputInt("Prize (Improv Coins, min value 1) : ", 1, int.MaxValue);
                int prizeExperience = Data.Methods.General.AskForUserInputInt("Experience (min value 1) : ", 1, int.MaxValue);
                prize = new Prize(prizeMoney, prizeExperience);

                string askMessage = "Which skill would you like to add to the audience skills ?";
                string atLeastMessage = "An audience needs to be able to cast at least one ability !";
                playerSkills = General.AskUserForSkills(askMessage, atLeastMessage, context);

                // Verify entries
                Audience audience = new Audience(name, description, level, quantity, prize, playerSkills);
                Console.WriteLine($"You are about to create a new audience with the following parameters :");
                audience.DisplaySelf();
                message = "Are you sure ?\n" +
                    "0 - No\n" +
                    "1 - Yes";
                userInput = Data.Methods.General.AskForUserInputInt(message, 0, 1);

                if (userInput == 1)
                { // Add audience to db
                    context.audiences.Add(audience);
                    context.SaveChanges();
                }
                else
                { // Back to menus
                    message = "Sorry you were not satisfied... What would you like to do now ?\n" +
                        "0 - Go back to Add items menu\n" +
                        "1 - Create a new Audience";
                    userInput = Data.Methods.General.AskForUserInputInt(message, 0, 1);
                    if (userInput == 0)
                    {
                        General.AddItemsMenu(context);
                    }
                    else
                    {
                        Audiences(context);
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no skills in the database yet. Please fill in at least one");
                Skills(context);
            }
        }

        public static void Teams(ConnectDB context)
        {
            bool hasStarterPlayer = false;
            foreach (Player player in context.players)
            {
                if (player.Type == PlayerTypeEnum.Starter)
                {
                    hasStarterPlayer = true;
                }
            }

            if (hasStarterPlayer && context.stats.Count() > 0)
            {
                string message;
                int userInput = -1;

                Console.WriteLine("A Team needs certain parameters. Please, fill in the followings :");
                // Create generic impro
                string name;
                bool alreadyExists = false;
                do
                {
                    name = Data.Methods.General.AskForUserInput("Name");
                    if (context.teams.FirstOrDefault(team => team.Name == name) != null)
                    {
                        Console.WriteLine("A Team with this name already exists. Please chosse another one");
                        alreadyExists = true;
                    }
                    else
                    {
                        alreadyExists = false;
                    }
                } while (alreadyExists);
                Impro impro = General.CreateImpro(name, "Team", context);

                // Get Team specifics
                string slogan;
                int money;
                List<Player> players = new List<Player>();
                slogan = Data.Methods.General.AskForUserInput("Moto");
                message = "Amount of Improv Coins to begin with : ";
                money = Data.Methods.General.AskForUserInputInt(message, 0, int.MaxValue);
                foreach (Player player in context.players)
                {
                    if (player.Type == PlayerTypeEnum.Starter)
                    {
                        players.Add(player);
                    }
                }

                message = "Who will play this team ?";
                int askType = Data.Methods.General.AskForUserInputEnum(message, "Team");
                TeamTypeEnum type = TeamTypeEnum.Player;
                switch (askType)
                {
                    case 0:
                        type = TeamTypeEnum.Player;
                        break;
                    case 1:
                        type = TeamTypeEnum.Computer;
                        break;
                }

                // Create Team
                TrainingRoom trainingRoom = CreateTrainingRoom(name, context);
                Team team = new Team(impro.Name, impro.Level, impro.Equipments, impro.Stats, impro.Inventory, slogan, money, trainingRoom, type);
                team.Players = players;

                // Verify entries
                Console.WriteLine("You are about to create a new Team. Are you sure ?");
                team.DisplaySelf();
                message = "Are you sure ?\n" +
                    "0 - No\n" +
                    "1 - Yes";
                userInput = Data.Methods.General.AskForUserInputInt(message, 0, 1);

                if (userInput == 1)
                {// Add Team to db
                    context.teams.Add(team);
                    context.SaveChanges();
                    Console.WriteLine("Well done ! You created a new Team !");
                }
                else
                { // Back to menus
                    message = "Sorry you were not satisfied... What would you like to do now ?\n" +
                        "0 - Go back to Add items menu\n" +
                        "1 - Create a new Team";
                    userInput = Data.Methods.General.AskForUserInputInt(message, 0, 1);
                    if (userInput == 0)
                    {
                        General.AddItemsMenu(context);
                    }
                    else
                    {
                        Teams(context);
                    }
                }
            }
            else
            {
                if (!hasStarterPlayer)
                {
                    Console.WriteLine("There are no starter Players in the database yet. Please fill in at least one.");
                    Players(context);
                }
                else
                {
                    Console.WriteLine("There are no Stats in the database yet. Please fill in at least one.");
                    Stats(context);
                }
            }
        }

        public static void Players(ConnectDB context)
        {
            if(context.skills.Count() > 0)
            {
                string message;
                int userInput = -1;

                Console.WriteLine("A Player needs certain parameters. Please, fill in the followings :");
                // Create generic Impro
                string name;
                bool alreadyExists = false;
                do
                {
                    name = Data.Methods.General.AskForUserInput("Name");
                    if (context.players.FirstOrDefault(player => player.Name == name) != null)
                    {
                        Console.WriteLine("A Player with this name already exists. Please chosse another one");
                        alreadyExists = true;
                    }
                    else
                    {
                        alreadyExists = false;
                    }
                } while (alreadyExists);
                Impro impro = General.CreateImpro(name, "Player", context);

                // Get Player specifics
                int age = Data.Methods.General.AskForUserInputInt("Age", 10, 80);

                string askMessage = "Which skill does the Player need to know ?";
                string atLeastMessage = "A Player needs at least one Skill to perform !";
                List<Skill> skills = General.AskUserForSkills(askMessage, atLeastMessage, context);

                Team team;
                message = $"Will {impro.Name} belong to a Team ?\n" +
                    $"0 - No\n" +
                    $"1 - Yes";
                userInput = Data.Methods.General.AskForUserInputInt(message, 0, 1);
                if (userInput == 1)
                {
                    Dictionary<int, Team> teams = new Dictionary<int, Team>();
                    teams = ListItems.ListTeams(context);
                    message = "Choose a Team";
                    userInput = Data.Methods.General.AskForUserInputInt(message, 0, teams.Count);
                    team = teams[userInput];
                }
                else
                {
                    team = context.teams.ElementAt(0);
                }

                int askType = Data.Methods.General.AskForUserInputEnum("Type", "Impro");
                PlayerTypeEnum type = PlayerTypeEnum.Starter;
                switch (askType)
                {
                    case 0:
                        type = PlayerTypeEnum.Starter;
                        break;
                    case 1:
                        type = PlayerTypeEnum.Common;
                        break;
                    case 2:
                        type = PlayerTypeEnum.Rare;
                        break;
                    case 3:
                        type = PlayerTypeEnum.Epic;
                        break;
                }

                // Create Player
                Player player = new Player(impro.Name, impro.Level, impro.Equipments, impro.Stats, impro.Inventory, age, team, type, false);
                player.Skills = skills;

                // Verify entries
                Console.WriteLine("You are about to create a Player with the following parmeters");
                player.DisplaySelf();
                message = "Are you sure ?\n" +
                    "0 - No\n" +
                    "1 - Yes";
                userInput = Data.Methods.General.AskForUserInputInt(message, 0, 1);
                if (userInput == 1)
                { // Add Player to db
                    context.players.Add(player);
                    Console.WriteLine("Well done ! You created a new Player");
                        context.SaveChanges();
                }
                else
                { // Back to menus
                    message = "Sorry you were not satisfied... What would you like to do now ?\n" +
                        "0 - Go back to Add items menu\n" +
                        "1 - Create a new Player";
                    userInput = Data.Methods.General.AskForUserInputInt(message, 0, 1);
                    if (userInput == 0)
                    {
                        General.AddItemsMenu(context);
                    }
                    else
                    {
                        Players(context);
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no Skill in the database yet. Please, fill in at least one.");
                Skills(context);
            }
        }

        public static void Equipments(ConnectDB context)
        {
            if (context.stats.Count() > 0) // At least one stat is needed
            { // Add the mandatory fields of an equipment
                string message;
                int userInput = -1;

                Console.WriteLine("An Equipment needs certain parameters. Please, fill in the followings :");
                string name;
                bool alreadyExists = false;
                do
                {
                    name = Data.Methods.General.AskForUserInput("Name");
                    if (context.equipments.FirstOrDefault(equipment => equipment.Name == name) != null)
                    {
                        Console.WriteLine("A Team with this name already exists. Please chosse another one");
                        alreadyExists = true;
                    }
                    else
                    {
                        alreadyExists = false;
                    }
                } while (alreadyExists);
                string description = Data.Methods.General.AskForUserInput("Description");
                int price = Data.Methods.General.AskForUserInputInt("Price", 0, int.MaxValue);
                int minLevel = Data.Methods.General.AskForUserInputInt("Level required to hold the equipment", 0, int.MaxValue);

                int askType = -1;
                EquipmentTypeEnum type = EquipmentTypeEnum.Consumable;
                askType = Data.Methods.General.AskForUserInputEnum("Which type is the Equipment", "Equipment");
                switch (askType)
                {
                    case 0:
                        type = EquipmentTypeEnum.Consumable;
                        break;
                    case 1:
                        type = EquipmentTypeEnum.Team;
                        break;
                    case 2:
                        type = EquipmentTypeEnum.Player;
                        break;
                }

                // Add the stats
                string askMessage = "Which stat would you like to add to the Equipment ?";
                string atLeastMessage = "An equipment needs at least one stat !";
                List<PowerStat> equipmentStats = Methods.General.AskUserForStats(askMessage, atLeastMessage, name, type.ToString(), context);

                // Verify the entries
                Equipment equipment = new Equipment(name, description, price, type, equipmentStats, minLevel, true);
                Console.WriteLine("------------------------");
                Console.WriteLine("You are about to create a new Equipment with the following parameters :");
                equipment.DisplaySelf();
                message = ("Are you sure ?\n" +
                    "0 - No\n" +
                    "1 - Yes");
                userInput = Data.Methods.General.AskForUserInputInt(message, 0, 1);

                if (userInput == 1)
                { // Add the equipment to db
                    context.equipments.Add(equipment);
                    context.SaveChanges();
                    Console.WriteLine("Well done ! You created a new Equipment !");
                }
                else
                { // Back to menus
                    message = "Sorry you were not satisfied... What would you like to do now ?" +
                        "0 - Go back to Add items menu\n" +
                        "1 - Create a new Equipment";
                    userInput = Data.Methods.General.AskForUserInputInt(message, 0, 1);
                    if (userInput == 0)
                    {
                        General.AddItemsMenu(context);
                    }
                    else
                    {
                        Equipments(context);
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no stats in the database yet. Please fill in at least one.");
                Stats(context);
            }
        }

        public static void Skills(ConnectDB context)
        {
            string message;
            int userInput = -1;
            bool isSure = false;
            // Add the mandatory fields of an equipment
            Console.WriteLine("A Skill needs certain parameters. Please, fill in the followings :");
            string name;
            bool alreadyExists = false;
            do
            {
                name = Data.Methods.General.AskForUserInput("Name");
                if (context.skills.FirstOrDefault(skill => skill.Name == name) != null)
                {
                    Console.WriteLine("A Skill with this name already exists. Please chosse another one");
                    alreadyExists = true;
                }
                else
                {
                    alreadyExists = false;
                }
            } while (alreadyExists);
            string description = Data.Methods.General.AskForUserInput("Description");
            int power = Data.Methods.General.AskForUserInputInt("Power (min value : 1)", 1, int.MaxValue);

            int askType = -1;
            SkillTypeEnum type = SkillTypeEnum.Audience;
            askType = Data.Methods.General.AskForUserInputEnum("Which type is the Skill ?", "Skill");
            switch (askType)
            {
                case 0:
                    type = SkillTypeEnum.Audience;
                    break;
                case 1:
                    type = SkillTypeEnum.Player;
                    break;
            }

            Dictionary<int, Stat> stats = new Dictionary<int, Stat>();
            List<CostStat> costList = new List<CostStat>();
            if (context.stats.Count() > 0)
            {
                int cost = -1;

                Console.WriteLine("Choose the Stat required to use the Skill (\"0 - That's enough\" if free) :");

                do
                {
                    Console.WriteLine("0 - That's enough");
                    stats = ListItems.ListStats(context, "Player");
                    userInput = Data.Methods.General.AskForUserInputInt("Choose a Stat  :", 0, stats.Count);
                    if (userInput != 0)
                    {
                        message = $"How many points of {stats[userInput].Name} is needed ?";
                        cost = Data.Methods.General.AskForUserInputInt(message, 1, int.MaxValue);

                        CostStat skillCost = new CostStat(stats[userInput], cost, name, type);
                        costList.Add(skillCost);
                    }
                } while (stats.Count != costList.Count() && userInput != 0);
            }
            else
            {
                Console.WriteLine("There are no stats in the database yet. It is not mandatory to create a Skill, but you may want to add some to require one to use your Skill");
                isSure = Data.Methods.General.AskYesNo("Would you like to create one ?");
                if (isSure) {
                    Stats(context);
                }
            }

            // Verify the entries
            Skill skill = new Skill(name, description, type, power, costList);
            Console.WriteLine("You are about to create a new Skill with the following parameters :");
            skill.DisplaySelf();
            if (costList.Count > 0)
            {
                Console.WriteLine("Stat(s) required :");
                foreach (CostStat costStat in costList)
                {
                    Console.WriteLine($"{costStat.Cost} points of {costStat.Stat.Name} ");
                }
            }

            message = ("Are you sure ?");
            isSure = Data.Methods.General.AskYesNo(message);

            // Add the skill to db
            if (isSure)
            {
                context.skills.Add(skill);
                context.SaveChanges();
                Console.WriteLine("Well done ! You created a new Skill !");
            }
            else
            {
                message = "Sorry you were not satisfied... What would you like to do now ?" +
                    "0 - Go back to Add items menu\n" +
                    "1 - Create a new Skill";
                userInput = Data.Methods.General.AskForUserInputInt(message, 0, 1);
                if (userInput == 0)
                {
                    General.AddItemsMenu(context);
                }
                else
                {
                    Skills(context);
                }
            }
        }

        public static void Stats(ConnectDB context)
        {
            string message;
            int userInput = -1;
            bool isSure = false;

            // Add the mandatory fields of a Stat
            Console.WriteLine("A Stat needs certain parameters. Please, fill in the followings :");
            string name;
            bool alreadyExists = false;
            do
            {
                name = Data.Methods.General.AskForUserInput("Name");
                if (context.stats.FirstOrDefault(stat => stat.Name == name) != null)
                {
                    Console.WriteLine("A Stat with this name already exists. Please chosse another one");
                    alreadyExists = true;
                }
                else
                {
                    alreadyExists = false;
                }
            } while (alreadyExists);
            string description = Data.Methods.General.AskForUserInput("Description");

            int askType = -1;
            StatTypeEnum type = StatTypeEnum.Team;
            askType = Data.Methods.General.AskForUserInputEnum("Which type is the Stat ?", "Stat");
            switch (askType)
            {
                case 0:
                    type = StatTypeEnum.Team;
                    break;
                case 1:
                    type = StatTypeEnum.Player;
                    break;
                case 2:
                    type = StatTypeEnum.TrainingRoom;
                    break;
            }

            Stat stat = new Stat(name, description, type);
            Console.WriteLine($"You are about to create a new stat with the following parameters :");
            stat.DisplaySelf();
                
            message = "Are you sure ?";
            isSure = Data.Methods.General.AskYesNo(message);

            if (isSure)
            {
                context.stats.Add(stat);
                context.SaveChanges();
                Console.WriteLine("Well done ! You created a new Stat !");
            }
            else
            {
                message = "Sorry you were not satisfied... What would you like to do now ?\n" +
                    "0 - Go back to Add items menu\n" +
                    "1 - Create a new Stat";
                userInput = Data.Methods.General.AskForUserInputInt(message, 0, 1);
                if (userInput == 0)
                {
                    General.AddItemsMenu(context);
                }
                else
                {
                    Stats(context);
                }
            }
        }

        public static void Performances(ConnectDB context)
        {
            string message;
            int userInput = -1;
            bool isSure = false;

            // Add the mandatory fields of a Stat
            Console.WriteLine("A Performance needs certain parameters. Please, fill in the followings :");
            string name;
            bool alreadyExists = false;
            do
            {
                name = Data.Methods.General.AskForUserInput("Name");
                if (context.performances.FirstOrDefault(performance => performance.Name == name) != null)
                {
                    Console.WriteLine("A Performance with this name already exists. Please chosse another one");
                    alreadyExists = true;
                }
                else
                {
                    alreadyExists = false;
                }
            } while (alreadyExists);
            string description = Data.Methods.General.AskForUserInput("Description");
            message = "Duration (number of turns) : ";
            int duration = Data.Methods.General.AskForUserInputInt(message, 1, int.MaxValue);
            message = "Number of players required : ";
            int nbPlayers = Data.Methods.General.AskForUserInputInt(message, 1, int.MaxValue);

            int askType = -1;
            PerformanceTypeEnum type = PerformanceTypeEnum.Catch;
            askType = Data.Methods.General.AskForUserInputEnum("Which type is the Performance ?", "Performance");
            switch (askType)
            {
                case 0:
                    type = PerformanceTypeEnum.Match;
                    break;
                case 1:
                    type = PerformanceTypeEnum.Catch;
                    break;
                case 2:
                    type = PerformanceTypeEnum.LongForm;
                    break;
                case 3:
                    type = PerformanceTypeEnum.Solo;
                    break;
            }

            message = "Amount of Improv Coins the player will get if they succeed : ";
            int money = Data.Methods.General.AskForUserInputInt(message, 1, int.MaxValue);
            message = "Amount of experience the player will get if they succeed : ";
            int experience = Data.Methods.General.AskForUserInputInt(message, 1, int.MaxValue);
            Prize prize = context.prizes.FirstOrDefault(prize => prize.Money == money && prize.Experience == experience);
            if (prize == null)
            {
                prize = new Prize(money, experience);
                context.prizes.Add(prize);
                context.SaveChanges();
                prize = context.prizes.FirstOrDefault(prize => prize.Money == money && prize.Experience == experience);
            }

            string askMessage = "Choose an equipment";
            List<Equipment> equipmentNeeded = General.AskUserForEquipments(askMessage, context);

            Performance performance = new Performance(name, description, duration, nbPlayers, type, prize, equipmentNeeded);
            Console.WriteLine($"You are about to create a new stat with the following parameters :");
            performance.DisplaySelf();

            message = "Are you sure ?";
            isSure = Data.Methods.General.AskYesNo(message);

            if (isSure)
            {
                context.performances.Add(performance);
                context.SaveChanges();
                Console.WriteLine("Well done ! You created a new Performance !");
            }
            else
            {
                message = "Sorry you were not satisfied... What would you like to do now ?\n" +
                    "0 - Go back to Add items menu\n" +
                    "1 - Create a new Stat";
                userInput = Data.Methods.General.AskForUserInputInt(message, 0, 1);
                if (userInput == 0)
                {
                    General.AddItemsMenu(context);
                }
                else
                {
                    Stats(context);
                }
            }
        }

        public static TrainingRoom CreateTrainingRoom(string name, ConnectDB context)
        {
            TrainingRoom trainingRoom;
            Shop shop = CreateShop(name, context);
            Console.WriteLine(shop.Name);
            trainingRoom = context.trainingRooms.ElementAt(0);
            trainingRoom = new TrainingRoom(trainingRoom.Name, trainingRoom.Description, 1, trainingRoom.Stats, shop);
            context.trainingRooms.Add(trainingRoom);
            context.SaveChanges();
            trainingRoom = context.trainingRooms.ElementAt(context.trainingRooms.Count() - 1);


            return trainingRoom;
        }

        public static Shop CreateShop(string name, ConnectDB context)
        {
            Shop shop;
            name = name + "'s shop";

            List<Equipment> equipmentList = new List<Equipment>();
            foreach (Equipment equipment in context.equipments)
            {
                equipmentList.Add(equipment);
            }
            shop = context.shops.ElementAt(0);
            shop = new Shop(name, shop.Description, equipmentList);
            context.shops.Add(shop);
            context.SaveChanges();
            shop = context.shops.FirstOrDefault(shop => shop.Name == name);

            return shop;
        }

        public static Inventory CreateInventory(ConnectDB context)
        {
            Inventory inventory;
            inventory = context.inventories.ElementAt(0);
            inventory = new Inventory(inventory.NbItemsMax);
            context.inventories.Add(inventory);
            context.SaveChanges();
            inventory = context.inventories.ElementAt(context.inventories.Count() - 1);

            return inventory;
        }

        public static Team CreateTeam(string name, Inventory inventory, string slogan, TrainingRoom trainingRoom, TeamTypeEnum type, ConnectDB context)
        {
            List<Equipment> equipments = new List<Equipment>();
            List<PowerStat> stats = new List<PowerStat>();
            Team team = new Team(name, 0, equipments, stats, inventory, slogan, 1500, trainingRoom, type);
            // Adding basic players to the team
            List<Player> dbPlayerList = General.GetStarterPlayers();
            List<Player> playerList = new List<Player>();
            foreach (Player dbPlayer in dbPlayerList)
            {
                context.players.Entry(dbPlayer).Collection(dbPlayer => dbPlayer.Equipments).Load();
                context.players.Entry(dbPlayer).Collection(dbPlayer => dbPlayer.Stats).Load();
                Inventory playerInventory = CreateInventory(context);
                List<PowerStat> playerStats = new List<PowerStat>();
                foreach (PowerStat stat in dbPlayer.Stats)
                {
                    PowerStat powerStat = new PowerStat(stat);
                    context.Add(powerStat);
                    context.SaveChanges();
                    playerStats.Add(context.powerStats.ElementAt(context.powerStats.Count() - 1));
                }
                Player player = CreatePlayer(dbPlayer.Name, dbPlayer.Level, dbPlayer.Equipments, playerStats, playerInventory, dbPlayer.Age, team, dbPlayer.Type, dbPlayer.Skills, context);
                playerList.Add(player);
            }
            team.Players = playerList;
            // Add the new team to the database
            context.teams.Add(team);
            context.SaveChanges();
            team = context.teams.FirstOrDefault(team => team.Name == name);

            return team;
        }

        public static Player CreatePlayer(string name, int level, List<Equipment> equipments, List<PowerStat> stats, Inventory inventory, int age, Team team, PlayerTypeEnum type, List<Skill> skills, ConnectDB context)
        {
            Player player = new Player(name, level, equipments, stats, inventory, age, team, type, false);
            // Add the new player to the database
            context.players.Add(player);
            context.SaveChanges();
            player = context.players.FirstOrDefault(player => player.Name == name);
            player.Skills = skills;
            context.Update(player);
            context.SaveChanges();

            return player;
        }
    }
}
