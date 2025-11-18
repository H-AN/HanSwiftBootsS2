using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Helpers;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace HanSwiftBootsS2;

public class HanSwiftBootsS2Service
{
    private readonly ISwiftlyCore _core;
    private readonly ILogger<HanSwiftBootsS2Service> _logger;
    private readonly IOptionsMonitor<HanSwiftBootsS2CFG> _SwiftBootsCFG;
    private readonly HanSwiftBootsS2Globals _globals;
    public HanSwiftBootsS2Service(ISwiftlyCore core,
        ILogger<HanSwiftBootsS2Service> logger,
        IOptionsMonitor<HanSwiftBootsS2CFG> Config,
        HanSwiftBootsS2Globals globals)
    {
        _core = core;
        _logger = logger;
        _SwiftBootsCFG = Config;
        _globals = globals;
    }
    public void HookEvent()
    {
        _core.GameEvent.HookPre<EventPlayerJump>(OnPlayerJump);
        _core.Event.OnTick += Event_OnTick;

    }

    public HookResult OnPlayerJump(EventPlayerJump @event)
    {
        var player = @event.UserIdPlayer;
        if (player == null || !player.IsValid)
            return HookResult.Continue;

        var pawn = @event.UserIdPawn;
        if (pawn == null || !pawn.IsValid)
            return HookResult.Continue;

        var cfg = _SwiftBootsCFG.CurrentValue; 
        int nowTick = _core.Engine.GlobalVars.TickCount;

        int durationTicks = 8;     //tick 
        float jumpBoostHeight = 500; 

        string steamId = player.SteamID.ToString();

        bool applied = false;

        if (cfg.Players.TryGetValue(steamId, out var sidCfg) && sidCfg.Enabled)
        {
            bool allowed = sidCfg.Team switch
            {
                0 => true,          // 全部队伍
                1 => pawn.TeamNum == 3, // CT
                2 => pawn.TeamNum == 2, // T
                _ => false
            };

            if (allowed)
            {
                jumpBoostHeight = sidCfg.JumpBoostHeight;
                durationTicks = sidCfg.DurationTicks;
                applied = true; // 已应用
            }
        }

        if (!applied)
        {
            TeamEntry? teamCfg = pawn.TeamNum switch
            {
                2 => cfg.Teams.T,
                3 => cfg.Teams.CT,
                _ => null
            };

            if (teamCfg != null && teamCfg.Enabled)
            {
                jumpBoostHeight = teamCfg.JumpBoostHeight;
                durationTicks = teamCfg.DurationTicks;
                applied = true;
            }
        }

        if (!applied)
            return HookResult.Continue;

        _globals.JumpBoostState[player] = nowTick + durationTicks;
        _globals.JumpBoostHeight[player] = jumpBoostHeight;

        return HookResult.Continue;
    }




    private void Event_OnTick()
    {
        int nowTick = _core.Engine.GlobalVars.TickCount;

        foreach (var kv in new Dictionary<IPlayer, int>(_globals.JumpBoostState))
        {
            var player = kv.Key;
            int endTick = kv.Value;

            if (player == null || !player.IsValid)
            {
                _globals.JumpBoostState.Remove(player);
                _globals.JumpBoostHeight.Remove(player);
                continue;
            }

            var pawn = player.PlayerPawn;
            if (pawn == null || !pawn.IsValid)
            {
                _globals.JumpBoostState.Remove(player);
                _globals.JumpBoostHeight.Remove(player);
                continue;
            }

            if (_core.Engine.GlobalVars.TickCount <= endTick)
            {
                if (_globals.JumpBoostHeight.TryGetValue(player, out var height))
                {
                    pawn.AbsVelocity.Z = height;
                }
            }
            else
            {
                _globals.JumpBoostState.Remove(player);
                _globals.JumpBoostHeight.Remove(player);
            }
        }
    }

}

