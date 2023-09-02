using Exiled.API.Interfaces;
using System.ComponentModel;

namespace BetterSinkholes.Configuration
{
    public class Translation : ITranslation
    {
        [Description("Broadcasted to the player upon falling into a sinkhole. Default: nothing")]
        public Exiled.API.Features.Broadcast TeleportMessage { get; set; } = new("You have been Teleported", 10, false);
    }
}
