using Data;
using Data.People;
using Data.Store;
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
                shop = context.shops.FirstOrDefault(shop => shop.Id == team.TrainingRoom.Shop.Id);
            }

            return shop;
        }

        public static List<Player> GetDefaultPlayers()
        {
            List<Player> playerList = new List<Player>();

            using (ConnectDB context = new ConnectDB())
            {
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
            }

            return playerList;
        }
    }
}
