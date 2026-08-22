using HarmonyLib;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using LabAutoEvent.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabAutoEvent
{
    public class AutoEvent : Plugin<Config>
    {
        public override string Name => "AutoEvent.LabAPI.Version";

        public override string Description => "A Mini Game Manager Of SCP:SL";

        public override string Author => "HUI";

        public override Version RequiredApiVersion => new Version(LabApi.Features.LabApiProperties.CompiledVersion);
        public override LoadPriority Priority => LoadPriority.High;
        public AutoEvent Instance { get; private set; }
        public Harmony XHarmony { get; private set; }
        public override void Enable()
        {
            Instance = this;
            CustomPaths.InitAll();
            XHarmony = new Harmony("sl.autoevent.com");
            XHarmony.PatchAll();
            CustomPaths.InitAll();
            LabApi.Events.Handlers.PlayerEvents.Death += OnPlayerDie;
        }
        public override void Disable()
        {
            Instance = null;
            XHarmony.UnpatchAll();
            LabApi.Events.Handlers.PlayerEvents.Death -= OnPlayerDie;
        }
        public void OnPlayerDie(PlayerDeathEventArgs ev)
        {
            if(ev.Player == null||ev.Attacker==null)
            {
                return;
            }
            if(AutoEventAPI._secplayers.Contains(ev.Player))
            {
                AutoEventAPI._secplayers.Remove(ev.Player);
            }
        }
    }
}
