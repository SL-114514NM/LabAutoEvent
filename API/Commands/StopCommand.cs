using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using LabAutoEvent.API.Featrues;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabAutoEvent.API.Commands
{
    public class StopCommand : ICommand
    {
        public string Command => "stop";

        public string[] Aliases => Array.Empty<string>();

        public string Description => "Stop game";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            PlayerCommandSender playerCommandSender = sender as PlayerCommandSender;
            if (!Player.TryGet(playerCommandSender.SenderId, out Player player))
            {
                response = "null of player";
                return false;
            }
            if (!player.HasPermission("autoevent.*"))
            {
                response = "需要配置autoevent.*命令权限";
                return false;
            }
            MiniGameBase miniGameBase = MiniGameManager.MiniGames.First(x => x.IsRunning);
            if(miniGameBase == null)
            {
                response = "游戏没开始";
                return false;
            }
            miniGameBase.OnEnd();
            response = "OK";
            return true;
        }
    }
}
