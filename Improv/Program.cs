using Data;
using Data.Methods;
using Improv.Methods;

namespace Improv
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ConnectDB context = new ConnectDB())
            {
                LoadDatabase.checkIfDbIsLoaded(context);
                Game.StartGame(context);
            }
        }
    }
}
