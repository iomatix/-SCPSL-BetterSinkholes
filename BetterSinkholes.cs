﻿using BetterSinkholes.Configuration;
using Exiled.API.Features;
using System;

namespace BetterSinkholes
{
    public class BetterSinkholes : Plugin<Config, Translation>
    {

        public override string Author => "Yamato & Gamers-Workshop & iomatix";
        public override string Name => "BetterSinkholes";
        public override string Prefix => "BetterSinkholes";
        public override Version Version { get; } = new(6, 0, 0);
        public override Version RequiredExiledVersion { get; } = new(9, 0, 0);

        public static EventHandlers eventHandlers;

        public override void OnEnabled()
        {
            base.OnEnabled();
            eventHandlers = new(this);
            Exiled.Events.Handlers.Server.WaitingForPlayers += eventHandlers.OnWaitingPlayer;
            Exiled.Events.Handlers.Player.StayingOnEnvironmentalHazard += eventHandlers.OnStayingOnEnvironmentalHazard;

        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            Exiled.Events.Handlers.Server.WaitingForPlayers -= eventHandlers.OnWaitingPlayer;
            Exiled.Events.Handlers.Player.StayingOnEnvironmentalHazard -= eventHandlers.OnStayingOnEnvironmentalHazard;
        }
    }
}
