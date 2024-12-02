using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Methods
{
    public class General
    {
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
    }
}
