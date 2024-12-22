using Data;
using Data.Methods;
using GameEditor.Methods;

namespace GameEditor
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (ConnectDB context = new ConnectDB())
            {
                LoadDatabase.checkIfDbIsLoaded(context);
                Methods.General.Game(context);
            }
        }
    }
}
