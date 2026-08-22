using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretLabNAudio;
using SecretLabNAudio.Core;
using UnityEngine;

namespace LabAutoEvent.API.Featrues
{
    public class MusicInfo
    {
        public MusicInfo() { }
        public MusicInfo(AudioPlayer audioPlayer, string MusicName)
        {
            this.AudioPlayer = audioPlayer;
            this.MusicName = MusicName;
        }
        public MusicInfo(string musicname)
        {
            this.MusicName = musicname;
            this.AudioPlayer = AudioPlayer.CreateGlobal(new Vector3(1, 1, 1));
        }
        public string MusicName { get; set; }
        public AudioPlayer AudioPlayer { get; set; }
    }
}
