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
    public class ListCommand : ICommand
    {
        public string Command =>"list";

        public string[] Aliases => Array.Empty<string>();

        public string Description => "show list of minigames";

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
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("小游戏列表:\n==========================\n");
            foreach(MiniGameBase miniGame in MiniGameManager.MiniGames)
            {
                sb.AppendLine($"[{miniGame.GameId}][{miniGame.GameName}][作者{miniGame.GameAuthor}] - {miniGame.GameDescription}\n");
            }
            sb.AppendLine("==========================\n使用au start <ID> 运行游戏");
            response = sb.ToString();
            return true;
        }
    }
}
