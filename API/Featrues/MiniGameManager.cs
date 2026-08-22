using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LabAutoEvent.API.Featrues
{
    public class MiniGameManager
    {
        public static List<MiniGameBase> MiniGames = new List<MiniGameBase>();

        public static void AddTypeToList(Type type)
        {
            if(!type.BaseType.IsAssignableFrom(typeof(MiniGameBase)))
            {
                return;
            }
            MiniGameBase miniGame = Activator.CreateInstance(type) as MiniGameBase;
            if(MiniGames.Any(x=> x.GameId == miniGame.GameId))
            {
                Logger.Warn("已记录ID为"+miniGame.GameId+"的小游戏，无法重复记录");
                return;
            }
            MiniGames.Add(miniGame);
        }
        public static void RemoveTypeToList(Type type)
        {
            if (!type.BaseType.IsAssignableFrom(typeof(MiniGameBase)))
            {
                return;
            }
            MiniGameBase miniGame = Activator.CreateInstance(type) as MiniGameBase;
            MiniGames.Remove(miniGame);
        }
        public static List<Type> FindMiniGamesFromPath(string Path)
        {
            List<Type> types = Assembly.LoadFile(Path).GetTypes().Where(x => x.BaseType.IsAssignableFrom(typeof(MiniGameBase))).ToList();
            return types;
        }
        public static void StartGame(int GameId, out string result)
        {
            if(!MiniGames.Any(x => x.GameId==GameId))
            {
                result = $"未找到ID为{GameId}的游戏";
                return;
            }
            MiniGameBase miniGameBase = MiniGames.First(x => x.GameId == GameId);
            if(AnyGameIsRunning())
            {
                result = $"有游戏正在运行，无法运行另一个[正在运行ID{MiniGames.First(x => x.IsRunning == true).GameId}]";
                return;
            }
            miniGameBase.OnEnabled();
            result = $"ID为{GameId}的游戏启动成功";
            return;
        }
        public static void StopOrEndGame(int GameId,string resean,out string result)
        {
            if (!AnyGameIsRunning())
            {
                result = "没有游戏正在运行";
                return;
            }
            MiniGameBase runninggame = MiniGames.First(x => x.IsRunning == true);
            runninggame.OnEnd();
            foreach(Player player in Player.List)
            {
                player.SendHint($"游戏被迫结束，原因{resean}");
            }
            result = "OK";
            return;
        }
        public static bool AnyGameIsRunning()
        {
            return MiniGames.Any(x => x.IsRunning == true);
        }
        public static void OnPluginEnabled()
        {
            List<Type> types = FindMiniGamesFromPath(Path.Combine(LabApi.Loader.Features.Paths.PathManager.Plugins.ToString(), Server.Port.ToString()));
            foreach (Type type in types)
            {
                AddTypeToList(type);
            }
        }
        public static void OnPluginUnLoad()
        {
            MiniGames.Clear();
        }
    }
}
