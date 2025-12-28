using System;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace ZeroLossCoke;

// ReSharper disable once ClassNeverInstantiated.Global
public class ZeroLossCokeSystem : ModSystem
{
    private Harmony? _harmony;

    internal static ZeroLossCokeConfig Config { get; private set; } = null!;
    internal static Logger Logger { get; private set; } = null!;

    public override bool ShouldLoad(EnumAppSide side)
        => side == EnumAppSide.Server;

    public override void StartServerSide(ICoreServerAPI api)
    {
        base.StartServerSide(api);

        Logger = new Logger(api, Mod.Info.ModID);
        Config = LoadAndNormalizeConfig(api);

        _harmony = new Harmony(Mod.Info.ModID);

        foreach (var type in typeof(ZeroLossCokeSystem).Assembly.GetTypes())
        {
            // Only attempt to patch types that actually have Harmony attributes 
            // AND are functional (not empty stubs from other framework builds)
            if (type.GetCustomAttributes(typeof(HarmonyPatch), false).Length <= 0) continue;
            try
            {
                _harmony.CreateClassProcessor(type).Patch();
            }
            catch (Exception ex)
            {
                if (Config.DebugLogging) Logger.Debug($"Skipping patch type {type.Name}: {ex.Message}");
            }
        }

        Logger.Event($"Loaded. YieldMultiplier={Config.YieldMultiplier:F2}");
    }

    private static ZeroLossCokeConfig LoadAndNormalizeConfig(ICoreServerAPI api)
    {
        var cfg = api.LoadModConfig<ZeroLossCokeConfig>("zerolosscoke.json")
                  ?? new ZeroLossCokeConfig();

        cfg.YieldMultiplier = Math.Max(0.1f, cfg.YieldMultiplier);

        if (cfg is { MinYield: > 0, MaxYield: > 0 } && cfg.MinYield > cfg.MaxYield)
        {
            (cfg.MinYield, cfg.MaxYield) = (cfg.MaxYield, cfg.MinYield);
            Logger.Warn("MinYield > MaxYield corrected");
        }

        api.StoreModConfig(cfg, "zerolosscoke.json");
        return cfg;
    }

    public override void Dispose()
    {
        _harmony?.UnpatchAll(Mod.Info.ModID);
    }
}