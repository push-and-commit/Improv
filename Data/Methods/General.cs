using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Values;
using Data.Store;
using Data.People;
using Data.Enums;


namespace Data.Methods
{
    public class General
    {
        public static int AskForUserInputInt(string message, int min, int max)
        {
            int userInput;
            bool isValid = false;

            Console.WriteLine(message);
            do
            {
                Console.Write("My choice : ");
                isValid = int.TryParse(Console.ReadLine(), out userInput);
                if (!isValid)
                {
                    Console.WriteLine("Invalid entry, please try again");
                }
                else if(userInput < min || userInput > max)
                {
                    Console.WriteLine($"The number given is not allowed (range {min.ToString()} - {max.ToString()}), please try again");
                    isValid = false;
                }
            } while (!isValid);

            return userInput;
        }

        public static string AskForUserInput(string message)
        {
            string result;
            bool isValid = false;

            do
            {
                Console.Write(message + " : ");
                result = Console.ReadLine();
                if (result.Count() > 0)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Your entry can not be empty");
                }
            } while (!isValid);

            return result;
        }

        public static int AskForUserInputEnum(string message, string type)
        {
            int userInput = -1;

            switch (type)
            {
                case "Equipment":
                    Console.WriteLine($"0 - {EquipmentTypeEnum.Team}");
                    Console.WriteLine($"1 - {EquipmentTypeEnum.Player}");
                    Console.WriteLine($"2 - {EquipmentTypeEnum.Consumable}");
                    userInput = AskForUserInputInt(message, 0, 2);
                    break;
                case "Player":
                    Console.WriteLine($"0 - {PlayerTypeEnum.Starter}");
                    Console.WriteLine($"1 - {PlayerTypeEnum.Common}");
                    Console.WriteLine($"2 - {PlayerTypeEnum.Rare}");
                    Console.WriteLine($"3 - {PlayerTypeEnum.Epic}");
                    userInput = AskForUserInputInt(message, 0, 3);
                    break;
                case "Team":
                    Console.WriteLine($"0 - {TeamTypeEnum.Player}");
                    Console.WriteLine($"1 - {TeamTypeEnum.Robot}");
                    userInput = AskForUserInputInt(message, 0, 1);
                    break;
                case "Performance":
                    Console.WriteLine($"0 - {PerformanceTypeEnum.Match}");
                    Console.WriteLine($"1 - {PerformanceTypeEnum.Catch}");
                    Console.WriteLine($"2 - {PerformanceTypeEnum.StandUp}");
                    Console.WriteLine($"3 - {PerformanceTypeEnum.LongForm}");
                    userInput = AskForUserInputInt(message, 0, 3);
                    break;
                case "Skill":
                    Console.WriteLine($"0 - {SkillTypeEnum.Audience}");
                    Console.WriteLine($"1 - {SkillTypeEnum.Player}");
                    userInput = AskForUserInputInt(message, 0, 1);
                    break;
                case "Stat":
                    Console.WriteLine($"0 - {StatTypeEnum.Team}");
                    Console.WriteLine($"1 - {StatTypeEnum.Player}");
                    Console.WriteLine($"2 - {StatTypeEnum.TrainingRoom}");
                    userInput = AskForUserInputInt(message, 0, 2);
                    break;
            }

            return userInput;
        }
    }
}
