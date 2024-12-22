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

namespace Data.Methods
{
    public class LoadDatabase
    {
        public static void PersonalDebug(ConnectDB context)
        {
            Console.WriteLine($"---------------------------------------------------------");
            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.State != Microsoft.EntityFrameworkCore.EntityState.Unchanged)
                {
                    Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
                }
            }
            Console.WriteLine($"---------------------------------------------------------");
        }
        public static void checkIfDbIsLoaded(ConnectDB context)
        {
            if (!context.players.Any())
            {
                LoadDB(context);
                TestTeam(context);
                RestartProgram();
            }
        }

        private static void RestartProgram()
        {
            Console.WriteLine();
            Console.WriteLine("Application will now restart...");
            Console.WriteLine();

            // Get current exec path
            var exePath = Process.GetCurrentProcess().MainModule.FileName;

            // Start a new instance of the exec
            Process.Start(new ProcessStartInfo
            {
                FileName = exePath,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Normal
            });

            // Close current app
            Environment.Exit(0);
        }

        public static void LoadDB(ConnectDB context)
        {
            /************************
             * 
             *
             *        Stats
             * 
             * 
             ***********************/
            Console.WriteLine("Initializing stats");
            // Team stats
            Stat solidarity = new Stat("Solidarity", "Team cohesion. The higher, the better the collective improvisations are", StatTypeEnum.Team);
            Stat reputation = new Stat("Reputation", "Team reputation", StatTypeEnum.Team);
            Stat creativity = new Stat("Creativity", "Reflects the ability to create new ideas and new concepts", StatTypeEnum.Team);
            // Player stats
            Stat fatigue = new Stat("Fatigue", "Energy of the player, decreases after each performance", StatTypeEnum.Player);
            Stat confidence = new Stat("Confidence", "Confidence of the player to play in front of an audience", StatTypeEnum.Player);
            Stat charisma = new Stat("Charisma", "The ability to captivate the audience", StatTypeEnum.Player);
            Stat solidarityThreshold = new Stat("Solidarity threshold", "Threshold below which the player will leave the team", StatTypeEnum.Player);
            // Training room stats
            Stat capacity = new Stat("Capacity", "Maximum number of player the training room can contain", StatTypeEnum.TrainingRoom);
            Stat comfort = new Stat("Comfort", "Impacts the energy regeneration of the players", StatTypeEnum.TrainingRoom);
            Stat focus = new Stat("Focus", "Minimise the distractions so that the trainings are more efficient", StatTypeEnum.TrainingRoom);

            // Add stats to database
            context.stats.AddRange(solidarity, reputation, creativity);
            context.stats.AddRange(fatigue, confidence, charisma, solidarityThreshold);
            context.stats.AddRange(capacity, comfort, focus);
            context.SaveChanges();
            Console.WriteLine("Stats successfully initialized");

            // Fetch stats from database to use them again later
            solidarity = context.stats.FirstOrDefault(stat => stat.Name == "Solidarity");
            reputation = context.stats.FirstOrDefault(stat => stat.Name == "Reputation");
            creativity = context.stats.FirstOrDefault(stat => stat.Name == "Creativity");
            fatigue = context.stats.FirstOrDefault(stat => stat.Name == "Fatigue");
            confidence = context.stats.FirstOrDefault(stat => stat.Name == "Confidence");
            charisma = context.stats.FirstOrDefault(stat => stat.Name == "Charisma");
            solidarityThreshold = context.stats.FirstOrDefault(stat => stat.Name == "Solidarity threshold");
            capacity = context.stats.FirstOrDefault(stat => stat.Name == "Capacity");
            comfort = context.stats.FirstOrDefault(stat => stat.Name == "Comfort");
            focus = context.stats.FirstOrDefault(stat => stat.Name == "Focus");

            /************************
             * 
             *
             *        PowerStats
             * 
             * 
             ***********************/
            Console.WriteLine("Initializing PowerStats");
            // Team PowerStats
            PowerStat team1Solidarity = new PowerStat(solidarity, "TeamStat", "Team", 70);
            PowerStat team1Reputation = new PowerStat(reputation, "TeamStat", "Team", 1);
            PowerStat team1Creativity = new PowerStat(creativity, "TeamStat", "Team", 65);

            PowerStat team2Solidarity = new PowerStat(solidarity, "TeamStat", "Team", 10);
            PowerStat team2Reputation = new PowerStat(reputation, "TeamStat", "Team", 50);
            PowerStat team2Creativity = new PowerStat(creativity, "TeamStat", "Team", 95);
            // Player PowerStat
            PowerStat player1Fatigue = new PowerStat(fatigue, "PlayerStat", "Player", 25);
            PowerStat player1Confidence = new PowerStat(confidence, "PlayerStat", "Player", 50);
            PowerStat player1Charisma = new PowerStat(charisma, "PlayerStat", "Player", 50);
            PowerStat player1SolidarityThreshold = new PowerStat(solidarityThreshold, "PlayerStat", "Player", 50);

            PowerStat player2Fatigue = new PowerStat(fatigue, "PlayerStat", "Player", 50);
            PowerStat player2Confidence = new PowerStat(confidence, "PlayerStat", "Player", 50);
            PowerStat player2Charisma = new PowerStat(charisma, "PlayerStat", "Player", 50);
            PowerStat player2SolidarityThreshold = new PowerStat(solidarityThreshold, "PlayerStat", "Player", 50);

            PowerStat player3Fatigue = new PowerStat(fatigue, "PlayerStat", "Player", 50);
            PowerStat player3Confidence = new PowerStat(confidence, "PlayerStat", "Player", 50);
            PowerStat player3Charisma = new PowerStat(charisma, "PlayerStat", "Player", 50);
            PowerStat player3SolidarityThreshold = new PowerStat(solidarityThreshold, "PlayerStat", "Player", 50);

            PowerStat player4Fatigue = new PowerStat(fatigue, "PlayerStat", "Player", 50);
            PowerStat player4Confidence = new PowerStat(confidence, "PlayerStat", "Player", 50);
            PowerStat player4Charisma = new PowerStat(charisma, "PlayerStat", "Player", 50);
            PowerStat player4SolidarityThreshold = new PowerStat(solidarityThreshold, "PlayerStat", "Player", 50);

            PowerStat player5Fatigue = new PowerStat(fatigue, "PlayerStat", "Player", 50);
            PowerStat player5Confidence = new PowerStat(confidence, "PlayerStat", "Player", 50);
            PowerStat player5Charisma = new PowerStat(charisma, "PlayerStat", "Player", 50);
            PowerStat player5SolidarityThreshold = new PowerStat(solidarityThreshold, "PlayerStat", "Player", 50);

            PowerStat player6Fatigue = new PowerStat(fatigue, "PlayerStat", "Player", 50);
            PowerStat player6Confidence = new PowerStat(confidence, "PlayerStat", "Player", 50);
            PowerStat player6Charisma = new PowerStat(charisma, "PlayerStat", "Player", 50);
            PowerStat player6SolidarityThreshold = new PowerStat(solidarityThreshold, "PlayerStat", "Player", 50);

            PowerStat player7Fatigue = new PowerStat(fatigue, "PlayerStat", "Player", 50);
            PowerStat player7Confidence = new PowerStat(confidence, "PlayerStat", "Player", 50);
            PowerStat player7Charisma = new PowerStat(charisma, "PlayerStat", "Player", 50);
            PowerStat player7SolidarityThreshold = new PowerStat(solidarityThreshold, "PlayerStat", "Player", 50);

            PowerStat player8Fatigue = new PowerStat(fatigue, "PlayerStat", "Player", 50);
            PowerStat player8Confidence = new PowerStat(confidence, "PlayerStat", "Player", 50);
            PowerStat player8Charisma = new PowerStat(charisma, "PlayerStat", "Player", 50);
            PowerStat player8SolidarityThreshold = new PowerStat(solidarityThreshold, "PlayerStat", "Player", 50);

            PowerStat player9Fatigue = new PowerStat(fatigue, "PlayerStat", "Player", 50);
            PowerStat player9Confidence = new PowerStat(confidence, "PlayerStat", "Player", 50);
            PowerStat player9Charisma = new PowerStat(charisma, "PlayerStat", "Player", 50);
            PowerStat player9SolidarityThreshold = new PowerStat(solidarityThreshold, "PlayerStat", "Player", 50);

            PowerStat player10Fatigue = new PowerStat(fatigue, "PlayerStat", "Player", 50);
            PowerStat player10Confidence = new PowerStat(confidence, "PlayerStat", "Player", 50);
            PowerStat player10Charisma = new PowerStat(charisma, "PlayerStat", "Player", 50);
            PowerStat player10SolidarityThreshold = new PowerStat(solidarityThreshold, "PlayerStat", "Player", 50);

            // TrainingRoom PowerStat
            PowerStat trainingRoom1Capacity = new PowerStat(capacity, "TrainingRoom", "TrainingRoom", 5);
            PowerStat trainingRoom1Comfort = new PowerStat(comfort, "TrainingRoom", "TrainingRoom", 10);
            PowerStat trainingRoom1Focus = new PowerStat(focus, "TrainingRoom", "TrainingRoom", 18);

            PowerStat trainingRoom2Capacity = new PowerStat(capacity, "TrainingRoom", "TrainingRoom", 20);
            PowerStat trainingRoom2Comfort = new PowerStat(comfort, "TrainingRoom", "TrainingRoom", 55);
            PowerStat trainingRoom2Focus = new PowerStat(focus, "TrainingRoom", "TrainingRoom", 72);

            // Equipment PowerStat
            PowerStat teamEquipmentTimer = new PowerStat(solidarity, "Timer", "Equipment", 2);
            PowerStat teamEquipmentWhistle = new PowerStat(creativity, "Whistle", "Equipment", 1);
            PowerStat teamEquipmentKazoo = new PowerStat(creativity, "Kazoo", "Equipment", 3);
            PowerStat teamEquipmentCatchRing = new PowerStat(solidarity, "Catch Ring", "Equipment", 3);
            PowerStat teamEquipmentSkatingRink = new PowerStat(reputation, "Skating Rink", "Equipment", 5);
            PowerStat teamEquipmentVoteCard = new PowerStat(reputation, "Vote Card", "Equipment", 1);
            PowerStat teamEquipmentPuck = new PowerStat(reputation, "Puck", "Equipment", 1);
            PowerStat teamEquipmentControlRoom = new PowerStat(creativity, "Control Room", "Equipment", 10);
            PowerStat teamEquipmentBracelet = new PowerStat(solidarity, "Bracelet", "Equipment", 10);

            PowerStat playerEquipmentVest = new PowerStat(fatigue, "Vest", "Player", 3);
            PowerStat playerEquipmentShoes = new PowerStat(fatigue, "Shoes", "Player", 2);
            PowerStat playerEquipmentTracksuit = new PowerStat(fatigue, "Tracksuit", "Player", 5);
            PowerStat playerEquipmentGlasses = new PowerStat(charisma, "Glasses", "Player", 3);
            PowerStat playerEquipmentRedNose = new PowerStat(charisma, "Red nose", "Player", 2);
            PowerStat playerEquipmentCape = new PowerStat(creativity, "Wig", "Player", 6);
            PowerStat playerEquipmentWig = new PowerStat(creativity, "Wig", "Player", 3);
            PowerStat playerEquipmentHat = new PowerStat(creativity, "Hat", "Player", 1);
            PowerStat playerEquipmentWaterBottle = new PowerStat(fatigue, "Water bottle", "Player", 1);

            PowerStat consumableEquipmentChips = new PowerStat(fatigue, "Chips", "Consumable", 1);
            PowerStat consumableEquipmentFruit = new PowerStat(fatigue, "Fruit", "Consumable", 3);
            PowerStat consumableEquipmentSandwich = new PowerStat(fatigue, "Sandwich", "Consumable", 6);
            PowerStat consumableEquipmentWelsh = new PowerStat(fatigue, "Welsh", "Consumable", 10);
            PowerStat consumableEquipmentWater = new PowerStat(fatigue, "Water", "Consumable", 1);
            PowerStat consumableEquipmentSirup = new PowerStat(fatigue, "Sirup", "Consumable", 1);
            PowerStat consumableEquipmentSoda = new PowerStat(fatigue, "Soda", "Consumable", 3);
            PowerStat consumableEquipmentBeer = new PowerStat(confidence, "Beer", "Consumable", 5);

            // Add powerStats to database
            context.powerStats.AddRange(team1Solidarity, team1Creativity, team1Reputation);
            context.powerStats.AddRange(team2Solidarity, team2Creativity, team2Reputation);

            context.powerStats.AddRange(player1Fatigue, player1Confidence, player1Charisma, player1SolidarityThreshold);
            context.powerStats.AddRange(player2Fatigue, player2Confidence, player2Charisma, player2SolidarityThreshold);
            context.powerStats.AddRange(player3Fatigue, player3Confidence, player3Charisma, player3SolidarityThreshold);
            context.powerStats.AddRange(player4Fatigue, player4Confidence, player4Charisma, player4SolidarityThreshold);
            context.powerStats.AddRange(player5Fatigue, player5Confidence, player5Charisma, player5SolidarityThreshold);
            context.powerStats.AddRange(player6Fatigue, player6Confidence, player6Charisma, player6SolidarityThreshold);
            context.powerStats.AddRange(player7Fatigue, player7Confidence, player7Charisma, player7SolidarityThreshold);
            context.powerStats.AddRange(player8Fatigue, player8Confidence, player8Charisma, player8SolidarityThreshold);
            context.powerStats.AddRange(player9Fatigue, player9Confidence, player9Charisma, player9SolidarityThreshold);
            context.powerStats.AddRange(player10Fatigue, player10Confidence, player10Charisma, player10SolidarityThreshold);

            context.powerStats.AddRange(trainingRoom1Capacity, trainingRoom1Comfort, trainingRoom1Focus);
            context.powerStats.AddRange(trainingRoom2Capacity, trainingRoom2Comfort, trainingRoom2Focus);

            context.powerStats.AddRange(teamEquipmentTimer, teamEquipmentWhistle, teamEquipmentKazoo, teamEquipmentCatchRing, teamEquipmentSkatingRink, teamEquipmentVoteCard, teamEquipmentPuck, teamEquipmentControlRoom, teamEquipmentBracelet);
            context.powerStats.AddRange(playerEquipmentVest, playerEquipmentShoes, playerEquipmentTracksuit, playerEquipmentGlasses, playerEquipmentRedNose, playerEquipmentCape, playerEquipmentWig, playerEquipmentHat, playerEquipmentWaterBottle);
            context.powerStats.AddRange(consumableEquipmentChips, consumableEquipmentFruit, consumableEquipmentSandwich, consumableEquipmentWelsh, consumableEquipmentWater, consumableEquipmentSirup, consumableEquipmentSoda, consumableEquipmentBeer);
            context.SaveChanges();
            Console.WriteLine("PowerStats successfully initialized");

            // Fetch powerStats from database to use them again later
            team1Solidarity = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 1);
            team1Creativity = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 2);
            team1Reputation = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 3);
            team2Solidarity = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 4);
            team2Creativity = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 5);
            team2Reputation = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 6);

            List<PowerStat> team1PowerStatList = new List<PowerStat> { team1Solidarity, team1Creativity, team1Reputation };
            List<PowerStat> team2PowerStatList = new List<PowerStat> { team2Solidarity, team2Creativity, team2Reputation };

            player1Fatigue = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 7);
            player1Confidence = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 8);
            player1Charisma = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 9);
            player1SolidarityThreshold = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 10);
            player2Fatigue = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 11);
            player2Confidence = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 12);
            player2Charisma = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 13);
            player2SolidarityThreshold = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 14);
            player3Fatigue = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 15);
            player3Confidence = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 16);
            player3Charisma = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 17);
            player3SolidarityThreshold = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 18);
            player4Fatigue = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 19);
            player4Confidence = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 20);
            player4Charisma = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 21);
            player4SolidarityThreshold = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 22);
            player5Fatigue = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 23);
            player5Confidence = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 24);
            player5Charisma = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 25);
            player5SolidarityThreshold = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 26);
            player6Fatigue = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 27);
            player6Confidence = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 28);
            player6Charisma = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 29);
            player6SolidarityThreshold = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 30);
            player7Fatigue = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 31);
            player7Confidence = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 32);
            player7Charisma = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 33);
            player7SolidarityThreshold = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 34);
            player8Fatigue = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 35);
            player8Confidence = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 36);
            player8Charisma = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 37);
            player8SolidarityThreshold = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 38);
            player9Fatigue = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 39);
            player9Confidence = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 40);
            player9Charisma = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 41);
            player9SolidarityThreshold = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 42);
            player10Fatigue = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 43);
            player10Confidence = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 44);
            player10Charisma = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 45);
            player10SolidarityThreshold = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 46);

            List<PowerStat> player1PowerStatList = new List<PowerStat> { player1Fatigue, player1Confidence, player1Charisma, player1SolidarityThreshold };
            List<PowerStat> player2PowerStatList = new List<PowerStat> { player2Fatigue, player2Confidence, player2Charisma, player2SolidarityThreshold };
            List<PowerStat> player3PowerStatList = new List<PowerStat> { player3Fatigue, player3Confidence, player3Charisma, player3SolidarityThreshold };
            List<PowerStat> player4PowerStatList = new List<PowerStat> { player4Fatigue, player4Confidence, player4Charisma, player4SolidarityThreshold };
            List<PowerStat> player5PowerStatList = new List<PowerStat> { player5Fatigue, player5Confidence, player5Charisma, player5SolidarityThreshold };
            List<PowerStat> player6PowerStatList = new List<PowerStat> { player6Fatigue, player6Confidence, player6Charisma, player6SolidarityThreshold };
            List<PowerStat> player7PowerStatList = new List<PowerStat> { player7Fatigue, player7Confidence, player7Charisma, player7SolidarityThreshold };
            List<PowerStat> player8PowerStatList = new List<PowerStat> { player8Fatigue, player8Confidence, player8Charisma, player8SolidarityThreshold };
            List<PowerStat> player9PowerStatList = new List<PowerStat> { player9Fatigue, player9Confidence, player9Charisma, player9SolidarityThreshold };
            List<PowerStat> player10PowerStatList = new List<PowerStat> { player10Fatigue, player10Confidence, player10Charisma, player10SolidarityThreshold };

            trainingRoom1Capacity = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 47);
            trainingRoom1Comfort = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 48);
            trainingRoom1Focus = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 49);
            trainingRoom2Capacity = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 50);
            trainingRoom2Comfort = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 51);
            trainingRoom2Focus = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 52);

            List<PowerStat> trainingRoom1PowerStatList = new List<PowerStat> { trainingRoom1Capacity, trainingRoom1Comfort, trainingRoom1Focus };
            List<PowerStat> trainingRoom2PowerStatList = new List<PowerStat> { trainingRoom2Capacity, trainingRoom2Comfort, trainingRoom2Focus };

            teamEquipmentTimer = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 53);
            teamEquipmentWhistle = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 54);
            teamEquipmentKazoo = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 55);
            teamEquipmentCatchRing = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 56);
            teamEquipmentSkatingRink = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 57);
            teamEquipmentVoteCard = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 58);
            teamEquipmentPuck = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 59);
            teamEquipmentControlRoom = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 60);
            teamEquipmentBracelet = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 61);

            playerEquipmentVest = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 62);
            playerEquipmentShoes = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 63);
            playerEquipmentTracksuit = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 64);
            playerEquipmentGlasses = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 65);
            playerEquipmentRedNose = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 66);
            playerEquipmentCape = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 67);
            playerEquipmentWig = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 68);
            playerEquipmentHat = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 69);
            playerEquipmentWaterBottle = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 70);

            consumableEquipmentChips = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 71);
            consumableEquipmentFruit = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 72);
            consumableEquipmentSandwich = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 73);
            consumableEquipmentWelsh = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 74);
            consumableEquipmentWater = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 75);
            consumableEquipmentSirup = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 76);
            consumableEquipmentSoda = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 77);
            consumableEquipmentBeer = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 78);

            List<PowerStat> teamEquipmentPowerStatList = new List<PowerStat> { teamEquipmentTimer, teamEquipmentWhistle, teamEquipmentKazoo, teamEquipmentCatchRing, teamEquipmentSkatingRink, teamEquipmentVoteCard, teamEquipmentPuck, teamEquipmentControlRoom, teamEquipmentBracelet };
            List<PowerStat> playerEquipmentPowerStatList = new List<PowerStat> { playerEquipmentVest, playerEquipmentShoes, playerEquipmentTracksuit, playerEquipmentGlasses, playerEquipmentRedNose, playerEquipmentCape, playerEquipmentWig, playerEquipmentHat, playerEquipmentWaterBottle };
            List<PowerStat> consumableEquipmentPowerStatList = new List<PowerStat> { consumableEquipmentChips, consumableEquipmentFruit, consumableEquipmentSandwich, consumableEquipmentWelsh, consumableEquipmentWater, consumableEquipmentSirup, consumableEquipmentSoda, consumableEquipmentBeer };

            /************************
             * 
             *
             *        CostStats
             * 
             * 
             ***********************/
            Console.WriteLine("Initializing CostStats");
            // Audience costStats
            CostStat applauseCost = new CostStat(fatigue, 0, "Applause", SkillTypeEnum.Audience);
            CostStat booCost = new CostStat(fatigue, 0, "Boo", SkillTypeEnum.Audience);
            CostStat nothingCost = new CostStat(fatigue, 0, "Nothing", SkillTypeEnum.Audience);
            CostStat voteCost = new CostStat(fatigue, 0, "Vote", SkillTypeEnum.Audience);

            // Player costStats
            // Punchline
            CostStat punchlineCostFatigue = new CostStat(fatigue, 1, "Punchline", SkillTypeEnum.Player);
            CostStat punchlineCostCharisma = new CostStat(charisma, 3, "Punchline", SkillTypeEnum.Player);
            CostStat punchlineCostConfidence = new CostStat(confidence, 5, "Punchline", SkillTypeEnum.Player);

            // Plot twist
            CostStat plotTwistCostFatigue = new CostStat(fatigue, 1, "Plot twist", SkillTypeEnum.Player);
            CostStat plotTwistCostCharisma = new CostStat(charisma, 3, "Plot twist", SkillTypeEnum.Player);
            CostStat plotTwistCostConfidence = new CostStat(confidence, 5, "Plot twist", SkillTypeEnum.Player);

            // Mime
            CostStat mimeCostFatigue = new CostStat(fatigue, 5, "PlotTwist", SkillTypeEnum.Player);
            CostStat mimeCostCharisma = new CostStat(charisma, 5, "PlotTwist", SkillTypeEnum.Player);
            CostStat mimeCostConfidence = new CostStat(confidence, 5, "PlotTwist", SkillTypeEnum.Player);

            // Monologue
            CostStat monologueCostFatigue = new CostStat(fatigue, 1, "PlotTwist", SkillTypeEnum.Player);
            CostStat monologueCostCharisma = new CostStat(charisma, 7, "PlotTwist", SkillTypeEnum.Player);
            CostStat monologueCostConfidence = new CostStat(confidence, 5, "PlotTwist", SkillTypeEnum.Player);

            // Add costStats to database
            context.costStats.AddRange(applauseCost, booCost, nothingCost, voteCost);
            context.costStats.AddRange(punchlineCostFatigue, punchlineCostCharisma, punchlineCostConfidence);
            context.costStats.AddRange(plotTwistCostFatigue, plotTwistCostCharisma, plotTwistCostConfidence);
            context.costStats.AddRange(mimeCostFatigue, mimeCostCharisma, mimeCostConfidence);
            context.costStats.AddRange(monologueCostFatigue, monologueCostCharisma, monologueCostConfidence);
            context.SaveChanges();
            Console.WriteLine("CostStats successfully initialized");

            // Fetch costStats from database to use them again later
            applauseCost = context.costStats.FirstOrDefault(costStat => costStat.Id == 1);
            booCost = context.costStats.FirstOrDefault(costStat => costStat.Id == 2);
            nothingCost = context.costStats.FirstOrDefault(costStat => costStat.Id == 3);
            voteCost = context.costStats.FirstOrDefault(costStat => costStat.Id == 4);
            punchlineCostFatigue = context.costStats.FirstOrDefault(costStat => costStat.Id == 5);
            punchlineCostCharisma = context.costStats.FirstOrDefault(costStat => costStat.Id == 6);
            punchlineCostConfidence = context.costStats.FirstOrDefault(costStat => costStat.Id == 7);

            List<CostStat> punchlineCostStatList = new List<CostStat> { punchlineCostFatigue, punchlineCostCharisma, punchlineCostConfidence };
            List<CostStat> applauseCostStatList = new List<CostStat>();

            /************************
             * 
             *
             *        Skills
             * 
             * 
             ***********************/
            Console.WriteLine("Initializing skills");
            // Player skills
            Skill punchline = new Skill("Punchline", "A joke that triggers laughter", SkillTypeEnum.Player, 10, punchlineCostStatList);
            Skill plotTwist = new Skill("Plot twist", "A joke that triggers laughter", SkillTypeEnum.Player, 10, punchlineCostStatList);
            Skill mime = new Skill("Mime", "A joke that triggers laughter", SkillTypeEnum.Player, 10, punchlineCostStatList);
            Skill monologue = new Skill("Monologue", "A pause int he ", SkillTypeEnum.Player, 10, punchlineCostStatList);

            // Audience skills
            Skill applause = new Skill("Applause", "Crowd applauding that causes confidence boost", SkillTypeEnum.Audience, 10, applauseCostStatList);
            Skill boo = new Skill("Boo", "Crowd applauding that causes confidence boost", SkillTypeEnum.Audience, 5, applauseCostStatList);
            Skill nothing = new Skill("Nothing", "Crowd applauding that causes confidence boost", SkillTypeEnum.Audience, 0, applauseCostStatList);
            Skill vote = new Skill("Vote", "Crowd applauding that causes confidence boost", SkillTypeEnum.Audience, 1, applauseCostStatList);

            // Add skills to database
            context.skills.AddRange(punchline, plotTwist, mime, monologue);
            context.skills.AddRange(applause, boo, nothing, vote);
            context.SaveChanges();
            Console.WriteLine("Skills successfully initialized");

            // Fetch skills from database to use them again later
            punchline = context.skills.FirstOrDefault(skill => skill.Name == "Punchline");
            plotTwist = context.skills.FirstOrDefault(skill => skill.Name == "Plot twist");
            mime = context.skills.FirstOrDefault(skill => skill.Name == "Mime");
            monologue = context.skills.FirstOrDefault(skill => skill.Name == "Monologue");
            applause = context.skills.FirstOrDefault(skill => skill.Name == "Applause");
            boo = context.skills.FirstOrDefault(skill => skill.Name == "Boo");
            nothing = context.skills.FirstOrDefault(skill => skill.Name == "Nothing");
            vote = context.skills.FirstOrDefault(skill => skill.Name == "Vote");

            List<Skill> player1SkillList = new List<Skill> { punchline, plotTwist, mime, monologue };
            List<Skill> player2SkillList = new List<Skill> { punchline, plotTwist, mime, monologue };
            List<Skill> player3SkillList = new List<Skill> { punchline, plotTwist, mime, monologue };
            List<Skill> player4SkillList = new List<Skill> { punchline, plotTwist, mime, monologue };
            List<Skill> player5SkillList = new List<Skill> { punchline, plotTwist, mime, monologue };
            List<Skill> player6SkillList = new List<Skill> { punchline, plotTwist, mime, monologue };
            List<Skill> player7SkillList = new List<Skill> { punchline, plotTwist, mime, monologue };
            List<Skill> player8SkillList = new List<Skill> { punchline, plotTwist, mime, monologue };
            List<Skill> player9SkillList = new List<Skill> { punchline, plotTwist, mime, monologue };
            List<Skill> player10SkillList = new List<Skill> { punchline, plotTwist, mime, monologue };

            List<Skill> audienceSkillList = new List<Skill> { applause, boo, nothing, vote };

            /************************
             * 
             *
             *        Equipments
             * 
             * 
             ***********************/
            Console.WriteLine("Initializing equipments");
            // Team equipments
            Equipment timer = new Equipment("Timer", "A tool to manage time during performances", 100, EquipmentTypeEnum.Team, teamEquipmentPowerStatList, 1, true);
            Equipment whistle = new Equipment("Whistle", "Used to coordinate players during matches", 150, EquipmentTypeEnum.Team, teamEquipmentPowerStatList, 2, true);
            Equipment kazoo = new Equipment("Kazoo", "A fun instrument to entertain the audience", 50, EquipmentTypeEnum.Team, teamEquipmentPowerStatList, 1, true);
            Equipment catchRing = new Equipment("Catch Ring", "Essential for improvised wrestling matches", 300, EquipmentTypeEnum.Team, teamEquipmentPowerStatList, 3, true);
            Equipment skatingRink = new Equipment("Skating Rink", "Provides a unique stage for performances. Required to perform a match (x1)", 500, EquipmentTypeEnum.Team, teamEquipmentPowerStatList, 4, true);
            Equipment voteCard = new Equipment("Vote Card", "The audience uses them to vote in a match. Required to performa a match (x6)", 20, EquipmentTypeEnum.Team, teamEquipmentPowerStatList, 1, true);
            Equipment puck = new Equipment("Puck", "Used in match to determine which team is playing first in a compared improvisation. Required to perform a match (x1)", 80, EquipmentTypeEnum.Team, teamEquipmentPowerStatList, 2, true);
            Equipment controlRoom = new Equipment("Control Room", "Enhances technical performance", 1000, EquipmentTypeEnum.Team, teamEquipmentPowerStatList, 5, true);
            Equipment bracelet = new Equipment("Bracelet", "Symbol of team solidarity", 60, EquipmentTypeEnum.Team, teamEquipmentPowerStatList, 1, true);

            // Player equipments
            Equipment vest = new Equipment("Vest", "A simple yet effective uniform", 40, EquipmentTypeEnum.Player, playerEquipmentPowerStatList, 1, true);
            Equipment shoes = new Equipment("Shoes", "Designed for comfort and mobility", 120, EquipmentTypeEnum.Player, playerEquipmentPowerStatList, 2, true);
            Equipment tracksuit = new Equipment("Tracksuit", "Provides flexibility for dynamic movements", 150, EquipmentTypeEnum.Player, playerEquipmentPowerStatList, 2, true);
            Equipment glasses = new Equipment("Glasses", "Comical oversized glasses for humor", 30, EquipmentTypeEnum.Player, playerEquipmentPowerStatList, 1, true);
            Equipment redNose = new Equipment("Red Nose", "A classic clown accessory", 25, EquipmentTypeEnum.Player, playerEquipmentPowerStatList, 1, true);
            Equipment cape = new Equipment("Cape", "Adds flair to character performances", 200, EquipmentTypeEnum.Player, playerEquipmentPowerStatList, 3, true);
            Equipment wig = new Equipment("Wig", "Transforms appearance for diverse roles", 70, EquipmentTypeEnum.Player, playerEquipmentPowerStatList, 2, true);
            Equipment hat = new Equipment("Hat", "A stylish addition for versatile characters", 100, EquipmentTypeEnum.Player, playerEquipmentPowerStatList, 2, true);
            Equipment waterBottle = new Equipment("Water Bottle", "Keeps players hydrated during performances", 10, EquipmentTypeEnum.Player, playerEquipmentPowerStatList, 1, true);

            // Consumable
            Equipment chips = new Equipment("Chips", "Restores some energy during a performance", 15, EquipmentTypeEnum.Consumable, consumableEquipmentPowerStatList, 1, true);
            Equipment fruit = new Equipment("Fruit", "A healthy option for a quick energy boost", 20, EquipmentTypeEnum.Consumable, consumableEquipmentPowerStatList, 1, true);
            Equipment sandwich = new Equipment("Sandwich", "A filling snack to restore stamina", 25, EquipmentTypeEnum.Consumable, consumableEquipmentPowerStatList, 1, true);
            Equipment welsh = new Equipment("Welsh", "A hearty meal for maximum energy recovery", 50, EquipmentTypeEnum.Consumable, consumableEquipmentPowerStatList, 3, true);
            Equipment water = new Equipment("Water", "Essential for hydration", 5, EquipmentTypeEnum.Consumable, consumableEquipmentPowerStatList, 1, true);
            Equipment sirup = new Equipment("Syrup", "A sweet drink for a quick morale boost", 10, EquipmentTypeEnum.Consumable, consumableEquipmentPowerStatList, 1, true);
            Equipment soda = new Equipment("Soda", "A bubbly refreshment to restore confidence", 15, EquipmentTypeEnum.Consumable, consumableEquipmentPowerStatList, 1, true);
            Equipment beer = new Equipment("Beer", "For relaxed, light-hearted performances", 25, EquipmentTypeEnum.Consumable, consumableEquipmentPowerStatList, 2, true);


            // Add to database
            context.equipments.AddRange(timer, whistle, kazoo, catchRing, skatingRink, voteCard, puck, controlRoom, bracelet);
            context.equipments.AddRange(vest, shoes, tracksuit, glasses, redNose, cape, wig, hat, waterBottle);
            // context.equipments.AddRange(chips, fruit, sandwich, welsh, water, sirup, soda, beer);
            context.equipments.Add(chips);
            context.equipments.Add(fruit);
            context.equipments.Add(sandwich);
            context.equipments.Add(welsh);
            context.equipments.Add(water);
            context.equipments.Add(sirup);
            context.equipments.Add(soda);
            context.equipments.Add(beer);
            context.SaveChanges();
            Console.WriteLine("Equipments successfully initialized");

            // Fetch stats from database to use them again later
            timer = context.equipments.FirstOrDefault(equipment => equipment.Id == 1);
            whistle = context.equipments.FirstOrDefault(equipment => equipment.Id == 2);
            kazoo = context.equipments.FirstOrDefault(equipment => equipment.Id == 3);
            catchRing = context.equipments.FirstOrDefault(equipment => equipment.Id == 4);
            skatingRink = context.equipments.FirstOrDefault(equipment => equipment.Id == 5);
            voteCard = context.equipments.FirstOrDefault(equipment => equipment.Id == 6);
            puck = context.equipments.FirstOrDefault(equipment => equipment.Id == 7);
            controlRoom = context.equipments.FirstOrDefault(equipment => equipment.Id == 8);
            bracelet = context.equipments.FirstOrDefault(equipment => equipment.Id == 9);

            vest = context.equipments.FirstOrDefault(equipment => equipment.Id == 10);
            shoes = context.equipments.FirstOrDefault(equipment => equipment.Id == 11);
            tracksuit = context.equipments.FirstOrDefault(equipment => equipment.Id == 12);
            glasses = context.equipments.FirstOrDefault(equipment => equipment.Id == 13);
            redNose = context.equipments.FirstOrDefault(equipment => equipment.Id == 14);
            cape = context.equipments.FirstOrDefault(equipment => equipment.Id == 15);
            wig = context.equipments.FirstOrDefault(equipment => equipment.Id == 16);
            hat = context.equipments.FirstOrDefault(equipment => equipment.Id == 17);
            waterBottle = context.equipments.FirstOrDefault(equipment => equipment.Id == 18);

            chips = context.equipments.FirstOrDefault(equipment => equipment.Id == 19);
            fruit = context.equipments.FirstOrDefault(equipment => equipment.Id == 20);
            sandwich = context.equipments.FirstOrDefault(equipment => equipment.Id == 21);
            welsh = context.equipments.FirstOrDefault(equipment => equipment.Id == 22);
            water = context.equipments.FirstOrDefault(equipment => equipment.Id == 23);
            sirup = context.equipments.FirstOrDefault(equipment => equipment.Id == 24);
            soda = context.equipments.FirstOrDefault(equipment => equipment.Id == 25);
            beer = context.equipments.FirstOrDefault(equipment => equipment.Id == 26);

            List<Equipment> equipmentList = new List<Equipment> { timer, whistle, kazoo, catchRing, skatingRink, voteCard, puck, controlRoom, bracelet, vest, shoes, tracksuit, glasses, redNose, cape, wig, hat, waterBottle, chips, fruit, sandwich, welsh, water, sirup, soda, beer };
            List<Equipment> team1EquipmentList = new List<Equipment> { timer, whistle, catchRing, voteCard };
            List<Equipment> team2EquipmentList = new List<Equipment> { timer, whistle, kazoo, catchRing, skatingRink, voteCard, puck, controlRoom, bracelet };
            List<Equipment> player1EquipmentList = new List<Equipment> { vest, shoes, waterBottle };
            List<Equipment> player2EquipmentList = new List<Equipment> { vest, shoes, waterBottle };
            List<Equipment> player3EquipmentList = new List<Equipment> { vest, shoes, waterBottle };
            List<Equipment> player4EquipmentList = new List<Equipment> { vest, shoes, waterBottle };
            List<Equipment> player5EquipmentList = new List<Equipment> { vest, shoes, waterBottle };
            List<Equipment> player6EquipmentList = new List<Equipment> { vest, shoes, waterBottle };
            List<Equipment> player7EquipmentList = new List<Equipment> { vest, shoes, waterBottle };
            List<Equipment> player8EquipmentList = new List<Equipment> { vest, shoes, waterBottle };
            List<Equipment> player9EquipmentList = new List<Equipment> { vest, shoes, waterBottle };
            List<Equipment> player10EquipmentList = new List<Equipment> { vest, shoes, waterBottle };

            /************************
             * 
             *
             *        Prizes
             * 
             * 
             ***********************/
            Console.WriteLine("Initializing prizes");
            Prize match1Prize = new Prize(500, 350);
            Prize match2Prize = new Prize(500, 350);
            Prize match3Prize = new Prize(500, 350);
            Prize catch1Prize = new Prize(200, 150);
            Prize catch2Prize = new Prize(200, 150);
            Prize catch3Prize = new Prize(200, 150);
            Prize longForm1Prize = new Prize(1000, 500);
            Prize longForm2Prize = new Prize(1000, 500);
            Prize longForm3Prize = new Prize(1000, 500);
            Prize solo1Prize = new Prize(750, 100);
            Prize solo2Prize = new Prize(750, 100);
            Prize solo3Prize = new Prize(750, 100);

            Prize audience1Prize = new Prize(50, 25);
            Prize audience2Prize = new Prize(100, 50);
            Prize audience3Prize = new Prize(300, 150);
            Prize audience4Prize = new Prize(150, 75);
            Prize audience5Prize = new Prize(1000, 500);

            // Add prizes to database
            context.prizes.AddRange(match1Prize, match2Prize, match3Prize);
            context.prizes.AddRange(catch1Prize, catch2Prize, catch3Prize);
            context.prizes.AddRange(longForm1Prize, longForm2Prize, longForm3Prize);
            context.prizes.AddRange(solo1Prize, solo2Prize, solo3Prize);
            context.prizes.AddRange(audience1Prize, audience2Prize, audience3Prize, audience4Prize, audience5Prize);
            context.SaveChanges();
            Console.WriteLine("Prizes successfully initialized");

            // Fetch stats from database to use them again later
            match1Prize = context.prizes.FirstOrDefault(prize => prize.Id == 1);
            match2Prize = context.prizes.FirstOrDefault(prize => prize.Id == 2);
            match3Prize = context.prizes.FirstOrDefault(prize => prize.Id == 3);
            catch1Prize = context.prizes.FirstOrDefault(prize => prize.Id == 4);
            catch2Prize = context.prizes.FirstOrDefault(prize => prize.Id == 5);
            catch3Prize = context.prizes.FirstOrDefault(prize => prize.Id == 6);
            longForm1Prize = context.prizes.FirstOrDefault(prize => prize.Id == 7);
            longForm2Prize = context.prizes.FirstOrDefault(prize => prize.Id == 8);
            longForm3Prize = context.prizes.FirstOrDefault(prize => prize.Id == 9);
            solo1Prize = context.prizes.FirstOrDefault(prize => prize.Id == 10);
            solo2Prize = context.prizes.FirstOrDefault(prize => prize.Id == 11);
            solo3Prize = context.prizes.FirstOrDefault(prize => prize.Id == 12);
            audience1Prize = context.prizes.FirstOrDefault(prize => prize.Id == 13);
            audience2Prize = context.prizes.FirstOrDefault(prize => prize.Id == 14);
            audience3Prize = context.prizes.FirstOrDefault(prize => prize.Id == 15);
            audience4Prize = context.prizes.FirstOrDefault(prize => prize.Id == 16);
            audience5Prize = context.prizes.FirstOrDefault(prize => prize.Id == 17);


            /************************
             * 
             *
             *        Shops
             * 
             * 
             ***********************/
            Console.WriteLine("Initializing shops");
            // Playable team
            Shop team1Shop = new Shop("Improv Shop", "A shop for all improvisation needs", equipmentList);

            // Computer team
            Shop team2Shop = new Shop("Computer Shop", "A shop for all improvisation needs", equipmentList);

            // Add shops to database
            context.shops.AddRange(team1Shop, team2Shop);
            context.SaveChanges();
            Console.WriteLine("Shops successfully initialized");

            // Fetch stats from database to use them again later
            team1Shop = context.shops.FirstOrDefault(shop => shop.Id == 1);
            team2Shop = context.shops.FirstOrDefault(shop => shop.Id == 2);

            /************************
             * 
             *
             *        Inventories
             * 
             * 
             ***********************/
            Console.WriteLine("Initializing Inventories");
            // Team inventories
            Inventory team1Inventory = new Inventory(4);
            Inventory team2Inventory = new Inventory(4);
            team1Inventory.Equipments = equipmentList;
            team2Inventory.Equipments = equipmentList;

            // Player inventories
            Inventory player1Inventory = new Inventory(5);
            Inventory player2Inventory = new Inventory(5);
            Inventory player3Inventory = new Inventory(5);
            Inventory player4Inventory = new Inventory(5);
            Inventory player5Inventory = new Inventory(5);
            Inventory player6Inventory = new Inventory(5);
            Inventory player7Inventory = new Inventory(5);
            Inventory player8Inventory = new Inventory(5);
            Inventory player9Inventory = new Inventory(5);
            Inventory player10Inventory = new Inventory(5);
            player1Inventory.Equipments = equipmentList;
            player2Inventory.Equipments = equipmentList;
            player3Inventory.Equipments = equipmentList;
            player4Inventory.Equipments = equipmentList;
            player5Inventory.Equipments = equipmentList;
            player6Inventory.Equipments = equipmentList;
            player7Inventory.Equipments = equipmentList;
            player8Inventory.Equipments = equipmentList;
            player9Inventory.Equipments = equipmentList;
            player10Inventory.Equipments = equipmentList;
            context.inventories.AddRange(team1Inventory, team2Inventory);
            context.inventories.AddRange(player1Inventory, player2Inventory, player3Inventory, player4Inventory, player5Inventory, player6Inventory, player7Inventory, player8Inventory, player9Inventory, player10Inventory);
            context.SaveChanges();
            Console.WriteLine("Inventories successfully initialized");

            // Fetch inventories from database to use them again later
            team1Inventory = context.inventories.FirstOrDefault(inventory => inventory.Id == 1);
            team2Inventory = context.inventories.FirstOrDefault(inventory => inventory.Id == 2);
            player1Inventory = context.inventories.FirstOrDefault(inventory => inventory.Id == 3);
            player2Inventory = context.inventories.FirstOrDefault(inventory => inventory.Id == 4);
            player3Inventory = context.inventories.FirstOrDefault(inventory => inventory.Id == 5);
            player4Inventory = context.inventories.FirstOrDefault(inventory => inventory.Id == 6);
            player5Inventory = context.inventories.FirstOrDefault(inventory => inventory.Id == 7);
            player6Inventory = context.inventories.FirstOrDefault(inventory => inventory.Id == 8);
            player7Inventory = context.inventories.FirstOrDefault(inventory => inventory.Id == 9);
            player8Inventory = context.inventories.FirstOrDefault(inventory => inventory.Id == 10);
            player9Inventory = context.inventories.FirstOrDefault(inventory => inventory.Id == 11);
            player10Inventory = context.inventories.FirstOrDefault(inventory => inventory.Id == 12);


            /************************
             * 
             *
             *        TrainingRooms
             * 
             * 
             ***********************/
            Console.WriteLine("Initializing trainingRooms");
            TrainingRoom trainingRoom1 = new TrainingRoom("Basic Training Room", "A place to train your team", 1, trainingRoom1PowerStatList, team1Shop);
            TrainingRoom trainingRoom2 = new TrainingRoom("Basic Training Room", "A place to train your team", 1, trainingRoom2PowerStatList, team2Shop);

            // Add training room to database
            context.trainingRooms.AddRange(trainingRoom1, trainingRoom2);
            context.SaveChanges();
            Console.WriteLine("TrainingRooms successfully initialized");

            // Fetch stats from database to use them again later
            trainingRoom1 = context.trainingRooms.FirstOrDefault(trainingRoom => trainingRoom.Id == 1);
            trainingRoom2 = context.trainingRooms.FirstOrDefault(trainingRoom => trainingRoom.Id == 2);

            /************************
             * 
             *
             *        Teams
             * 
             * 
             ***********************/
            Console.WriteLine("Initializing teams");
            // Default team
            List<Player> playerList = new List<Player>();
            Team defaultTeam = new Team("Default", 1, team1EquipmentList, team1PowerStatList, team1Inventory, "Let's have fun together !", 1500, trainingRoom1, TeamTypeEnum.Player);

            // Computer team
            Team olimp = new Team("OLIMP", 1, equipmentList, team2PowerStatList, team2Inventory, "Improvise and conquer !", 1500, trainingRoom2, TeamTypeEnum.Computer);
            context.teams.AddRange(defaultTeam, olimp);
            context.SaveChanges();
            defaultTeam = context.teams.FirstOrDefault(team => team.Name == "Default");
            olimp = context.teams.FirstOrDefault(team => team.Name == "OLIMP");
            Console.WriteLine("Teams successfully initialized");

            /************************
             * 
             *
             *        Players
             * 
             * 
             ***********************/
            Console.WriteLine("Initializing players");
            // Starter players
            Player playerXavier = new Player("Xavier", 1, player1EquipmentList, player1PowerStatList, player1Inventory, 20, null, PlayerTypeEnum.Starter, true);
            Player playerIsmael = new Player("Ismael", 1, player2EquipmentList, player2PowerStatList, player2Inventory, 20, null, PlayerTypeEnum.Starter, true);
            Player playerGuillaume = new Player("Guillaume", 1, player3EquipmentList, player3PowerStatList, player3Inventory, 20, null, PlayerTypeEnum.Starter, true);
            Player playerAlix = new Player("Alix", 1, player4EquipmentList, player4PowerStatList, player4Inventory, 20, null, PlayerTypeEnum.Starter, true);
            Player playerRomain = new Player("Romain", 1, player5EquipmentList, player5PowerStatList, player5Inventory, 20, null, PlayerTypeEnum.Starter, true);

            // Common players

            // Rare players

            // Epic players
            // Player playerEmilie = new Player("Emilie", 1, player6EquipmentList, player6PowerStatList/*fatigue 18/50 confidence 50/100 charisma 87/100 solidarityThreshold 30/50*/, player6Inventory, 28, null, PlayerTypeEnum.Starter, true);

            // Computer players
            Player playerMonia = new Player("Monia", 1, player6EquipmentList, player6PowerStatList, player6Inventory, 28, olimp, PlayerTypeEnum.Common, false);
            Player playerRenald = new Player("Renald", 1, player7EquipmentList, player7PowerStatList, player7Inventory, 20, olimp, PlayerTypeEnum.Common, false);
            Player playerJeanPhi = new Player("Jean-Phi", 1, player8EquipmentList, player8PowerStatList, player8Inventory, 20, olimp, PlayerTypeEnum.Common, false);
            Player playerMarie = new Player("Marie", 1, player9EquipmentList, player9PowerStatList, player9Inventory, 20, olimp, PlayerTypeEnum.Rare, false);
            Player playerFred = new Player("Fred", 1, player10EquipmentList, player10PowerStatList, player10Inventory, 20, olimp, PlayerTypeEnum.Epic, false);

            // Add players to database
            context.players.AddRange(playerXavier, playerIsmael, playerGuillaume, playerAlix, playerRomain);
            context.players.AddRange(playerMonia, playerRenald, playerJeanPhi, playerMarie, playerFred);
            context.SaveChanges();

            // Fetch players from database to use them again later
            playerXavier = context.players.FirstOrDefault(player => player.Name == "Xavier");
            playerIsmael = context.players.FirstOrDefault(player => player.Name == "Ismael");
            playerGuillaume = context.players.FirstOrDefault(player => player.Name == "Guillaume");
            playerAlix = context.players.FirstOrDefault(player => player.Name == "Alix");
            playerRomain = context.players.FirstOrDefault(player => player.Name == "Romain");
            // playerEmilie = context.players.FirstOrDefault(player => player.Name == "Emilie");
            playerMonia = context.players.FirstOrDefault(player => player.Name == "Monia");
            playerRenald = context.players.FirstOrDefault(player => player.Name == "Renald");
            playerJeanPhi = context.players.FirstOrDefault(player => player.Name == "Jean-Phi");
            playerMarie = context.players.FirstOrDefault(player => player.Name == "Marie");
            playerFred = context.players.FirstOrDefault(player => player.Name == "Fred");

            // Add skills to players
            playerXavier.Skills = player1SkillList;
            playerIsmael.Skills = player2SkillList;
            playerGuillaume.Skills = player3SkillList;
            playerAlix.Skills = player4SkillList;
            playerRomain.Skills = player5SkillList;
            // playerEmilie.Skills = player6SkillList;
            playerMonia.Skills = player6SkillList;
            playerRenald.Skills = player7SkillList;
            playerJeanPhi.Skills = player8SkillList;
            playerMarie.Skills = player9SkillList;
            playerFred.Skills = player10SkillList;
            context.players.Update(playerXavier);
            context.players.Update(playerIsmael);
            context.players.Update(playerGuillaume);
            context.players.Update(playerAlix);
            context.players.Update(playerRomain);
            // context.players.UpdateRange(playerEmilie);
            context.players.UpdateRange(playerMonia, playerRenald, playerJeanPhi, playerMarie, playerFred);
            context.SaveChanges();

            // Add players to default team
            foreach (Player player in context.players)
            {
                if (player.Team == null)
                {
                    player.Team = defaultTeam;
                    defaultTeam.Players.Add(player);
                }
            }
            context.teams.Update(defaultTeam);
            context.SaveChanges();
            Console.WriteLine("Teams successfully initialized");

            /************************
             * 
             *
             *        Audiences
             * 
             * 
             ***********************/
            Console.WriteLine("Initializing audiences");
            Audience smallTownAudience = new Audience("Small town", "A small but enthusiastic audience from a rural area", 1, 20, audience1Prize, audienceSkillList);
            Audience urbanYouthAudience = new Audience("Urban youth", "A young, dynamic crowd from the city", 2, 50, audience2Prize, audienceSkillList);
            Audience festivalAudience = new Audience("Festival", "A large audience at an improvisation festival", 3, 100, audience3Prize, audienceSkillList);
            Audience comedyClubAudience = new Audience("Comedy club", "A sophisticated audience expecting witty humor", 2, 40, audience4Prize, audienceSkillList);
            Audience nationalFinalsAudience = new Audience("National finals", "The largest and most critical audience at the national finals", 5, 200, audience5Prize, audienceSkillList);

            // Add audiences to database
            context.audiences.AddRange(smallTownAudience, urbanYouthAudience, festivalAudience, comedyClubAudience, nationalFinalsAudience);
            context.SaveChanges();

            // Fetch players from database to use them again later
            smallTownAudience = context.audiences.FirstOrDefault(audience => audience.Name == "Small town");
            urbanYouthAudience = context.audiences.FirstOrDefault(audience => audience.Name == "Urban youth");
            festivalAudience = context.audiences.FirstOrDefault(audience => audience.Name == "Festival");
            comedyClubAudience = context.audiences.FirstOrDefault(audience => audience.Name == "Comedy club");
            nationalFinalsAudience = context.audiences.FirstOrDefault(audience => audience.Name == "National finals");

            Console.WriteLine("Audiences successfully initialized");

            /************************
             * 
             *
             *        Performances
             * 
             * 
             ***********************/
            Console.WriteLine("Initializing performances");
            // Match
            List<Equipment> matchEquipmentsNeededList = new List<Equipment> { vest };
            Performance match1Performance = new Performance("Local Match", "A friendly match in a small town hall.", 5, 6, PerformanceTypeEnum.Match, match1Prize, matchEquipmentsNeededList);
            Performance match2Performance = new Performance("Regional Match", "Compete against other regional teams.", 8, 6, PerformanceTypeEnum.Match, match2Prize, matchEquipmentsNeededList);
            Performance match3Performance = new Performance("Championship Match", "The ultimate showdown in front of a massive audience.", 10, 6, PerformanceTypeEnum.Match, match3Prize, matchEquipmentsNeededList);
            // Catch
            List<Equipment> catchEquipmentsNeededList = new List<Equipment> { vest };
            Performance friendlyCatch = new Performance("Friendly Catch", "A lighthearted performance to entertain the audience", 4, 4, PerformanceTypeEnum.Catch, catch1Prize, catchEquipmentsNeededList);
            Performance cityCatch = new Performance("City Catch", "A competition to prove who’s the best in town", 6, 4, PerformanceTypeEnum.Catch, catch2Prize, catchEquipmentsNeededList);
            Performance galaCatch = new Performance("Gala Catch", "A glamorous event for the most skilled improvisers", 8, 4, PerformanceTypeEnum.Catch, catch3Prize, catchEquipmentsNeededList);
            // Long form
            List<Equipment> longFormEquipmentsNeededList = new List<Equipment> { vest };
            Performance storyTellingNight = new Performance("Storytelling Night", "Craft a memorable story for an engaged audience.", 6, 10, PerformanceTypeEnum.LongForm, longForm1Prize, longFormEquipmentsNeededList);
            Performance improvMarathon = new Performance("Improv Marathon", "A long performance that tests endurance and creativity.", 10, 10, PerformanceTypeEnum.LongForm, longForm2Prize, longFormEquipmentsNeededList);
            Performance theaterFestival = new Performance("Theater Festival", "A highly anticipated event at a renowned festival.", 8, 8, PerformanceTypeEnum.LongForm, longForm3Prize, longFormEquipmentsNeededList);
            // Solo
            List<Equipment> soloEquipmentsNeededList = new List<Equipment> { vest };
            Performance standUpComedy = new Performance("Stand-Up Comedy", "A single performer captivates the audience with humor", 3, 1, PerformanceTypeEnum.Solo, solo1Prize, soloEquipmentsNeededList);
            Performance monologueShow = new Performance("Monologue Show", "Deliver an emotional and gripping solo performance", 4, 1, PerformanceTypeEnum.Solo, solo2Prize, soloEquipmentsNeededList);
            Performance grandMasterShow = new Performance("Grand Master Show", "A legendary solo performance by the best improviser in the league", 5, 1, PerformanceTypeEnum.Solo, solo3Prize, soloEquipmentsNeededList);

            // Add performances to database
            context.performances.AddRange(match1Performance, match2Performance, match3Performance);
            context.performances.AddRange(friendlyCatch, cityCatch, galaCatch);
            context.performances.AddRange(storyTellingNight, improvMarathon, theaterFestival);
            context.performances.AddRange(standUpComedy, monologueShow, grandMasterShow);
            context.SaveChanges();
            Console.WriteLine("Performances successfully initialized");

            Console.WriteLine("Database successfully initialized");
        }

        public static void TestTeam(ConnectDB context)
        {
            PowerStat trainingRoom2Capacity = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 50);
            PowerStat trainingRoom2Comfort = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 51);
            PowerStat trainingRoom2Focus = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 52);
            List<PowerStat> trainingRoom2PowerStatList = new List<PowerStat> { trainingRoom2Capacity, trainingRoom2Comfort, trainingRoom2Focus };

            PowerStat teamEquipmentTimer = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 53);
            PowerStat teamEquipmentWhistle = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 54);
            PowerStat teamEquipmentKazoo = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 55);
            PowerStat teamEquipmentCatchRing = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 56);
            PowerStat teamEquipmentSkatingRink = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 57);
            PowerStat teamEquipmentVoteCard = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 58);
            PowerStat teamEquipmentPuck = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 59);
            PowerStat teamEquipmentControlRoom = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 60);
            PowerStat teamEquipmentBracelet = context.powerStats.FirstOrDefault(powerStat => powerStat.Id == 61);

            List<PowerStat> teamEquipmentPowerStatList = new List<PowerStat> { teamEquipmentTimer, teamEquipmentWhistle, teamEquipmentKazoo, teamEquipmentCatchRing, teamEquipmentSkatingRink, teamEquipmentVoteCard, teamEquipmentPuck, teamEquipmentControlRoom, teamEquipmentBracelet };

            List<Equipment> equipmentList = new List<Equipment>();
            foreach (Equipment equipment in context.equipments)
            {
                equipmentList.Add(equipment);
            }
            Shop team3Shop = new Shop("Test Shop", "A shop for all improvisation needs", equipmentList);
            context.shops.AddRange(team3Shop);
            context.SaveChanges();
            team3Shop = context.shops.FirstOrDefault(shop => shop.Id == 3);

            Inventory team3Inventory = new Inventory(99999999);
            team3Inventory.Equipments = equipmentList;
            context.inventories.Add(team3Inventory);
            context.SaveChanges();
            team3Inventory = context.inventories.FirstOrDefault(inventory => inventory.Id == 13);

            TrainingRoom trainingRoom3 = new TrainingRoom("Basic Training Room", "A place to train your team", 1, trainingRoom2PowerStatList, team3Shop);
            context.trainingRooms.AddRange(trainingRoom3);
            context.SaveChanges();
            trainingRoom3 = context.trainingRooms.FirstOrDefault(trainingRoom => trainingRoom.Id == 3);

            // Test team
            Team testTeam = new Team("Test team", 99999, equipmentList, teamEquipmentPowerStatList, team3Inventory, "Test desc", 99999, trainingRoom3, TeamTypeEnum.Player);
            context.teams.AddRange(testTeam);
            context.SaveChanges();
            testTeam = context.teams.FirstOrDefault(team => team.Name == "Test team");

            foreach (Player player in context.players)
            {
                if (player.Type == PlayerTypeEnum.Starter)
                {
                    testTeam.Players.Add(player);
                }
            }
            context.teams.Update(testTeam);
            context.SaveChanges();

        }
    }
}
