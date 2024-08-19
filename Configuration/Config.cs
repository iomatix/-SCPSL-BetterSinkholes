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

        [Description("Max number of sinkholes")]
        public int MaxSinkholes { get; private set; } = 5;

        [Description("Extend spawn amount of sinkholes by this value if the SCP-106 is alive")]
        public int MoreSinkholesSCP106 { get; private set; } = 5;

        [Description("Probability the sinkhole will spawn in the random room. ")]
        public int SinkholeChance { get; private set; } = 8;

        [Description("All sinkholes will disappear and then reappear in the random locations. Set it to 0 to turn off the rotation.")]
        public float RotateSinkholesTime { get; private set; } = 360.0f;

        [Description("Allow the sinkhole spawn in LCZ ")]
        public bool AllowLCZ { get; private set; } = true;

        [Description("Allow the sinkhole spawn in HCZ ")]
        public bool AllowHCZ { get; private set; } = true;

        [Description("Allow the sinkhole spawn in Entrance ")]
        public bool AllowEntrance { get; private set; } = true;



        [Description("Debug BetterSinkholes")]
        public bool Debug { get; set; }


    }
}
