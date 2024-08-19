using Exiled.API.Interfaces;
using System.ComponentModel;
using Exiled.API.Features;

namespace BetterSinkholes.Configuration
{
    public class Config : IConfig
    {
        [Description("Enable/disable BetterSinkholes")]
        public bool IsEnabled { get; set; } = true;

        [Description("Distance from the center of a sinkhole where player gets warped")]
        public float TeleportDistance { get; private set; } = 0.25f;

        [Description("Distance from the center of a sinkhole where player starts getting slowed")]
        public float SlowDistance { get; private set; } = 0.99f;

        [Description("Sinkhole slowness intensity. Higher value slows down movement based on the distance from the center of the sinkhole.")]
        public int SlowIntensity { get; private set; } = 100;


        [Description("Debug BetterSinkholes")]
        public bool Debug { get; set; }


    }
}
