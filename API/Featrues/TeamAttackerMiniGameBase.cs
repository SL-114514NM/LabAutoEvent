using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabAutoEvent.API.Featrues
{
    /// <summary>
    /// 团队竞技小游戏示例基类
    /// </summary>
    public abstract class TeamAttackerMiniGameBase : MiniGameBase
    {
        /// <summary>
        /// 示例基类不进行加载，如果你要继承，需要重写属性为true
        /// </summary>
        public override bool WillLoad { get; set; } = false;
        public Dictionary<Player, int> PlayerCustomTeam = new Dictionary<Player, int>();
        public Dictionary<int, float> CustomTeamSoc = new Dictionary<int, float>();

        private CoroutineHandle cor;
        public override void OnEnabled()
        {
            Server.FriendlyFire = true;
            foreach (Player player in Player.List)
            {
                SpawnPlayerRandomPos(player);
            }
            LabApi.Events.Handlers.PlayerEvents.Death += OnPlayerDie;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Server.FriendlyFire = false;
            foreach (Player player in Player.List)
            {
                PlayerCustomTeam.Remove(player);
                CustomTeamSoc.Clear();
            }
            LabApi.Events.Handlers.PlayerEvents.Death -= OnPlayerDie;
            base.OnDisabled();
        }
        private void OnPlayerDie(PlayerDeathEventArgs ev)
        {
            if (ev.Player == null||ev.Attacker==null)
            {
                return;
            }
            if(PlayerCustomTeam.ContainsKey(ev.Attacker))
            {
                int teamid = PlayerCustomTeam[ev.Attacker];
                CustomTeamSoc[teamid] += 1;
                SpawnPlayerRandomPos(ev.Player);
            }
        }
        private void SpawnPlayerRandomPos(Player player)
        {
            Timing.CallDelayed(1, () =>
            {
                int posindex = AutoEventAPI._random.Next(this.PlayerSpawnPositions.Count);
                player.SetRole(AutoEventAPI.GetRandomRole());
                player.Position = PlayerSpawnPositions[posindex].transform.position + UnityEngine.Vector3.up;
                player.AddItem(ItemType.GunCom45);
            });
        }
        private IEnumerator<float> HandlerSoc()
        {
            while(IsRunning)
            {
                if (CustomTeamSoc.Values.Any(x => x >= 20))
                {
                    int teamid = CustomTeamSoc.Keys.FirstOrDefault(x => CustomTeamSoc[x] >= 20);
                    foreach(Player player in Player.List)
                    {
                        if(teamid==0)
                        {
                            player.SendHint("队伍1得到积分20获得胜利", 10);
                        }
                        else
                        {
                            player.SendHint("队伍2得到积分20获得胜利", 10);
                        }
                    }
                    OnEnd();
                    yield break;
                }
                if(!IsRunning)
                {
                    yield break;
                }
            }
        }
        private enum TeamId
        {
            Team1,
            Team2
        }
    }
}
