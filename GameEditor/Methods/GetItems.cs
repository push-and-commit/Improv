using Data;
using Data.People;
using Data.Store;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.Methods
{
    public class GetItems
    {
        public static Shop Shop(Team team)
        {
            Shop shop;

            using (ConnectDB context = new ConnectDB())
            {
                Team vteam = context.teams
                    .Include(qTeam => qTeam.TrainingRoom) // Load Training room
                    .ThenInclude(trainingRoom => trainingRoom.Shop) // Then load Shop
                    .FirstOrDefault(qTeam => qTeam.Id == team.Id); // Query the right team

                // Get shop from team
                Console.WriteLine(vteam.Name);
                Console.WriteLine(vteam.TrainingRoom.Name);
                Console.WriteLine(vteam.TrainingRoom.Shop.Name);
                shop = team?.TrainingRoom?.Shop; // ? returns null before trying to reach the next object. Thanks ChatGPT, never would have found without it !

            }

            return shop;
        }

        public static List<Player> GetDefaultPlayers(ConnectDB context)
        {
            List<Player> playerList = new List<Player>();

            if (context.players.Count() > 0)
            {
                foreach (Player player in context.players)
                {
                    if (player.IsDefault == true)
                    {
                        playerList.Add(player);
                    }
                }
            }

            return playerList;
        }

        public static Team PlayerByName(string name)
        {
            using (ConnectDB context = new ConnectDB())
            {
                return context.teams.FirstOrDefault(team => team.Name == name);
            }
        }
    }
}
