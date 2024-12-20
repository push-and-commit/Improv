using Data;
using Data.Store;
using Data.People;
using GameEditor;
using Improv.Methods;

namespace Improv
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ConnectDB context = new ConnectDB())
            {
                Game.StartGame(context);
            }
        }
    }
}
