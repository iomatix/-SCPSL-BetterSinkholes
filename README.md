# BetterSinkholes2 - Sinkhole Environmental Hazard Rework

BetterSinkholes is a plugin that makes **sinkhole environmental hazards** (found in Light Containment Zone - IX Intersections) more realistic and  similar to SCP: Containment Breach. With the use of this plugin, players who walk into sinkholes fall into the pocket dimension *and may never return*.

## Requirements
- This plugin uses [EXILED](https://github.com/galaxy119/EXILED/).
- Make sure the config option in `config_gameplay.txt` called `sinkhole_spawn_chance` is set to higher than 0.

Note: **BetterSinkholes is only guaranteed to work with Exiled 8.0.0(+) and SCP:SL 13(+)!**

## Releases and Installation
You can find the latest release [here](https://github.com/rby-blackruby/BetterSinkholes2/releases).
Once downloaded, place the BetterSinkholes2.dll file into the /EXILED/Plugins folder and restart your server.

## Configs
| Config option | Value type | Default value | Description |
| --- | --- | --- | --- |
| `IsEnabled` | bool | true | Enables the BetterSinkholes2 plugin. Set it to false to disable it. |
| `SlowDistance` | float | 1f | Distance a the sinkhole where it starts slowing. Don't set it higher than 1.15! |
| `TeleportDistance` | float | 0.6f | Distance from a sinkhole where it teleports you to the pocket dimension. Set it to higher than 0!|

## Translations
| Config option | Value type | Default value | Description |
| --- | --- | --- | --- |
| `TeleportMessage` | Broadcast | '' | Simple Exiled Broadcast. Can use Unity's RichText. |

## Thank you!

Thank you for being interested in this plugin and I wish you a great time using it! If you have any ideas/problems feel free to contact me on discord: `_yamato._`
(Thanks for `blackruby#2562` for the original Plugin