using Exiled.API.Interfaces;
using System.ComponentModel;

namespace BetterSinkholes.Configuration
{
    public class Translation : ITranslation
    {
        [Description("Broadcasted to the player upon falling into a sinkhole.")]
        public Exiled.API.Features.Broadcast TeleportMessage { get; set; } = new Exiled.API.Features.Broadcast("You have been warped through the Sinkhole", 5, false);
    }
}
