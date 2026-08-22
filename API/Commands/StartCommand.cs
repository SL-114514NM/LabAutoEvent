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
    public class StartCommand : ICommand
    {
        public string Command => "start";

        public string[] Aliases => Array.Empty<string>();

        public string Description => "Start MiniGame";

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
            string gameid = arguments.At(0);
            if (string.IsNullOrEmpty(gameid))
            {
                response = "游戏id为空,使用au list查看所有小游戏";
                return false;
            }
            int gameindex = int.Parse(gameid);
            MiniGameManager.StartGame(gameindex, out string result);
            response = result;
            return true;
        }
    }
}
