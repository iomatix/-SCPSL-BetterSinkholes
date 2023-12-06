using CustomPlayerEffects;
using Exiled.API.Features.Hazards;
using Exiled.Events.EventArgs.Player;
using Hazards;
using System.Linq;
using UnityEngine;

namespace BetterSinkholes
{
    public class EventHandlers
    {
        public readonly BetterSinkholes plugin;
        public EventHandlers(BetterSinkholes plugin) => this.plugin = plugin;
        public void OnWaitingPlayer()
        {
            foreach (Hazard hazard in Hazard.List)
            {
                if (hazard.Is(out SinkholeHazard sinkholeHazard))
                    sinkholeHazard.MaxDistance *= plugin.Config.SlowDistance;
            }
        }
        public void OnStayingOnEnvironmentalHazard(StayingOnEnvironmentalHazardEventArgs ev) 
        {
            if (ev.Hazard is not SinkholeHazard sinkholeHazard || ev.Player.ReferenceHub.playerEffectsController.GetEffect<PocketCorroding>().IsEnabled)
                return;
            if (Vector3.Distance(sinkholeHazard.Position, ev.Player.Position) <= sinkholeHazard.MaxDistance * plugin.Config.TeleportDistance)
            {
                if (ev.Player.EnableEffect<PocketCorroding>())
                    Exiled.Events.Handlers.Player.OnEnteringPocketDimension(new(ev.Player, null));

                if (plugin.Translation.TeleportMessage is not null)
                    ev.Player.Broadcast(plugin.Translation.TeleportMessage);
            }
        }
    }
}
