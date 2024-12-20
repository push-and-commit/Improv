using Data;
using Data.People;
using Data.Store;

namespace GameEditor
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (ConnectDB context = new ConnectDB())
            {
                Methods.General.LoadDB(context);
                Methods.General.Game(context);
            }
        }
    }
}
