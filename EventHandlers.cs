using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Hazards;
using Exiled.Events.EventArgs.Player;
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
                {
                    sinkholeHazard.MaxDistance *= plugin.Config.SlowDistance;
                    sinkholeHazard.MaxHeightDistance *= plugin.Config.SlowDistance;
                }
            }
        }
        public void OnStayingOnEnvironmentalHazard(StayingOnEnvironmentalHazardEventArgs ev) 
        {
            if (ev.Hazard is not SinkholeHazard sinkholeHazard || ev.Player.IsEffectActive<PocketCorroding>()) return;

            float distanceToHazardCenter = Vector3.Distance(sinkholeHazard.Position, ev.Player.Position);

            // Slowdown player more if closer to the center
            float slowdownFactor = 1.0f - (distanceToHazardCenter / sinkholeHazard.MaxDistance);
            slowdownFactor = Mathf.Clamp(slowdownFactor, 0.1f, 0.95f);
            // Convert the slowdown factor to a byte value for intensity
            byte intensity = (byte)(slowdownFactor * plugin.Config.SlowIntensity);

            ev.Player.EnableEffect<Slowness>(intensity: intensity, duration: 0.25f, addDurationIfActive: false);

            if (distanceToHazardCenter <= sinkholeHazard.MaxDistance * plugin.Config.TeleportDistance)
            {
                if (ev.Player.EnableEffect<PocketCorroding>())
                    Exiled.Events.Handlers.Player.OnEnteringPocketDimension(new(ev.Player, null));

                if (plugin.Translation.TeleportMessage is not null)
                    ev.Player.Broadcast(plugin.Translation.TeleportMessage);
            }
        }
    }
}
