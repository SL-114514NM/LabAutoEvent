using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabAutoEvent.API.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class AutoEventCommand : ParentCommand
    {
        public AutoEventCommand() => LoadGeneratedCommands();
        public override string Command => "au";

        public override string[] Aliases => new string[] { "ev"};

        public override string Description => "Command Of MiniGame";

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            PlayerCommandSender playerCommandSender = sender as PlayerCommandSender;
            if(!Player.TryGet(playerCommandSender.SenderId, out Player player))
            {
                response = "null of player";
                return false;
            }
            if(!player.HasPermission("autoevent.*"))
            {
                response = "需要配置autoevent.*命令权限";
                return false;
            }
            response = "au list - 查看所有已注册小游戏\nau start - 开始小游戏\nau stop - 停止小游戏运行";
            return true;
        }

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new ListCommand());
            RegisterCommand(new StartCommand());
            RegisterCommand(new StopCommand());
        }

    }
}
