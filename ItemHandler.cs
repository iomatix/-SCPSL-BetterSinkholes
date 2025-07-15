/* TODO
namespace BetterSinkholes
{
    using Hazards;
    using LabApi.Events.Arguments.PlayerEvents;
    using LabApi.Events.Arguments.ServerEvents;
    using LabApi.Events.Handlers;
    using LabApi.Features.Wrappers;
    using MEC;
    using Mirror;
    using PlayerRoles.PlayableScps.Scp106;
    using RelativePositioning;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class ItemSinkholeHandler
    {
        private static readonly Dictionary<Pickup, CoroutineHandle> monitoredPickups = new();
        private static readonly object lockObject = new();

        public static void Initialize()
        {
            // Subscribe to pickup events  
            ServerEvents.PickupCreated += OnPickupCreated;
            ServerEvents.PickupDestroyed += OnPickupDestroyed;
            PlayerEvents.DroppedItem += OnPlayerDroppedItem;
        }

        public static void Cleanup()
        {
            ServerEvents.PickupCreated -= OnPickupCreated;
            ServerEvents.PickupDestroyed -= OnPickupDestroyed;
            PlayerEvents.DroppedItem -= OnPlayerDroppedItem;

            lock (lockObject)
            {
                foreach (var coroutine in monitoredPickups.Values)
                {
                    if (coroutine.IsRunning)
                        Timing.KillCoroutines(coroutine);
                }
                monitoredPickups.Clear();
            }
        }

        private static void OnPickupCreated(PickupCreatedEventArgs ev)
        {
            StartMonitoringPickup(ev.Pickup);
        }

        private static void OnPlayerDroppedItem(PlayerDroppedItemEventArgs ev)
        {
            StartMonitoringPickup(ev.Pickup);
        }

        private static void OnPickupDestroyed(LabApi.Events.Arguments.ServerEvents.PickupDestroyedEventArgs ev)
        {
            StopMonitoringPickup(ev.Pickup);
        }

        private static void StartMonitoringPickup(Pickup pickup)
        {
            if (pickup == null || pickup.IsDestroyed) return;

            lock (lockObject)
            {
                if (monitoredPickups.ContainsKey(pickup)) return;

                var coroutine = Timing.RunCoroutine(MonitorPickupPosition(pickup), Segment.Update);
                monitoredPickups[pickup] = coroutine;
            }
        }

        private static void StopMonitoringPickup(Pickup pickup)
        {
            if (pickup == null) return;

            lock (lockObject)
            {
                if (monitoredPickups.TryGetValue(pickup, out var coroutine))
                {
                    if (coroutine.IsRunning)
                        Timing.KillCoroutines(coroutine);
                    monitoredPickups.Remove(pickup);
                }
            }
        }

        private static IEnumerator<float> MonitorPickupPosition(Pickup pickup)
        {
            while (pickup != null && !pickup.IsDestroyed)
            {
                yield return Timing.WaitForSeconds(0.5f); // Check every 500ms  

                try
                {
                    if (pickup.Room == null) continue;

                    // Check if pickup is in a room with sinkhole hazards  
                    var sinkholeHazards = GetSinkholeHazardsInRoom(pickup.Room);

                    foreach (var hazard in sinkholeHazards)
                    {
                        float distanceToHazardCenter = Vector3.Distance(pickup.Position, hazard.Position);

                        // Use your existing logic for distance checking  
                        if (distanceToHazardCenter <= hazard.MaxDistance * Plugin.Config.TeleportDistance)
                        {
                            TeleportItemToPocketDimension(pickup);
                            yield break; // Stop monitoring this pickup  
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Library_ExiledAPI.LogError("MonitorPickupPosition", $"Error monitoring pickup: {ex.Message}");
                    yield break;
                }
            }

            // Cleanup when pickup is destroyed or monitoring stops  
            lock (lockObject)
            {
                monitoredPickups.Remove(pickup);
            }
        }

        private static void TeleportItemToPocketDimension(Pickup pickup)
        {
            try
            {
                // Create a PocketItem to handle the teleportation  
                var pocketItemManager = UnityEngine.Object.FindObjectOfType<Scp106PocketItemManager>();
                if (pocketItemManager == null)
                {
                    Library_ExiledAPI.LogError("TeleportItemToPocketDimension", "Could not find Scp106PocketItemManager");
                    return;
                }

                // Create pocket item entry  
                var pocketItem = new Scp106PocketItemManager.PocketItem
                {
                    Item = pickup.Base,
                    TriggerTime = NetworkTime.time + YourPlugin.Config.ItemPocketLifetime, // Configure this  
                    Remove = YourPlugin.Config.DestroyItemsInPocket, // Configure whether to destroy or drop back  
                    DropPosition = new RelativePosition(YourPlugin.Config.ItemDropPosition) // Configure drop position  
                };

                // Add to pocket dimension  
                pocketItemManager.PocketItems.Add(pocketItem);

                // Remove from world (the pickup will be managed by pocket dimension now)  
                pickup.Destroy();

                Library_ExiledAPI.LogDebug("TeleportItemToPocketDimension",
                    $"Teleported item {pickup.Type} to pocket dimension");
            }
            catch (System.Exception ex)
            {
                Library_ExiledAPI.LogError("TeleportItemToPocketDimension",
                    $"Failed to teleport item to pocket dimension: {ex.Message}");
            }
        }

        private static IEnumerable<EnvironmentalHazard> GetSinkholeHazardsInRoom(Room room)
        {
            // You'll need to implement this based on your hazard detection logic  
            // This should return all sinkhole hazards in the given room  
            // Example implementation:  
            return room.GameObject.GetComponentsInChildren<EnvironmentalHazard>()
                .Where(h => h.HazardType == HazardType.Sinkhole); // Adjust based on your hazard type  
        }
    }
}
*/