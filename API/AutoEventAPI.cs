using LabApi.Features.Wrappers;
using MEC;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabAutoEvent.API
{
    public static class AutoEventAPI
    {
        public static List<Player> _secplayers = new List<Player>();
        public static System.Random _random = new System.Random();
        public static Player GetRandomPlayer(List<Player> playerList)
        {
            List<Player> newlist = playerList.Where(x => !_secplayers.Contains(x)).ToList();
            int newplayerindex = _random.Next(0, newlist.Count);
            Player player = newlist[newplayerindex];
            _secplayers.Add(player);
            return player;
        }
        public static List<Player> GetRandomPlayers(List<Player> players, float count)
        {
            List<Player> needplayers = new List<Player>();
            for(int i=0;i<count;i++)
            {
                Player player = GetRandomPlayer(players);
                if(player != null)
                {
                    needplayers.Add(player);
                }
            }
            return needplayers;
        }
        public static RoleTypeId GetRandomRole()
        {
            int i = _random.Next(Enum.GetValues(typeof(RoleTypeId)).Length);
            return (RoleTypeId)Enum.GetValues(typeof(RoleTypeId)).GetValue(i);
        }
    }
}
