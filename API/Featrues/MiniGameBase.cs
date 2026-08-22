using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using ProjectMER.Features;
using SecretLabNAudio.Core.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = LabApi.Features.Console.Logger;

namespace LabAutoEvent.API.Featrues
{
    public abstract class MiniGameBase : IMiniGameBase
    {
        public abstract int GameId { get; set; }
        public abstract int GameName { get; set; }
        public abstract string GameAuthor { get; set; }
        public abstract string GameDescription { get; set; }
        public abstract string GameVersion { get; set; }
        public abstract bool WillLoad { get; set; }
        public MapInfo MapInfo { get; set; }
        public MusicInfo MusicInfo { get; set; }
        public bool NeedLoadMap { get; set; }
        public bool IsRunning { get; set; }
        public static event Action<MiniGameBase> OnGameLoad;
        public static event Action<MiniGameBase> OnGameEnd;
        public List<GameObject> PlayerSpawnPositions = new List<GameObject>();
        public virtual void OnDisabled()
        {
            OnGameEnd.Invoke(this);
            PlayerSpawnPositions.Clear();
            MapInfo.MapObject.Destroy();
            MusicInfo.AudioPlayer.Destroy();
            IsRunning = true;
        }
        public virtual void OnEnabled()
        {
            OnGameLoad.Invoke(this);
            IsRunning = false;
            if(MapInfo!=null)
            {
                this.NeedLoadMap = true;
                if(MapInfo.MapObject==null)
                {
                    ObjectSpawner.SpawnSchematic(MapInfo.MapObject.Name, MapInfo.Position);
                }
                foreach(GameObject gb in MapInfo.MapObject.AttachedBlocks)
                {
                    if(gb.name=="SpawnPos")
                    {
                        PlayerSpawnPositions.Add(gb);
                    }
                }
            }
            if(MusicInfo!=null)
            {
                string musicpath = Path.Combine(CustomPaths.MusicFolder, $"{MusicInfo.MusicName}.ogg");
                if(File.Exists(musicpath))
                {
                    MusicInfo.AudioPlayer.UseFile(musicpath, true, 24);
                }
                else
                {
                    Logger.Warn($"未找到音频{MusicInfo.MusicName},自动销毁音频播放器");
                    MusicInfo.AudioPlayer.Destroy();
                }
            }
        }
        public virtual void OnEnd()
        {
            OnDisabled();
            foreach(Player player in Player.List)
            {
                player.SetRole(PlayerRoles.RoleTypeId.Tutorial);
                player.SetRole(PlayerRoles.RoleTypeId.ClassD, PlayerRoles.RoleChangeReason.RemoteAdmin, PlayerRoles.RoleSpawnFlags.All);
                player.SendBroadcast($"{GameName}结束",10);
            }    
        }
    }
}
