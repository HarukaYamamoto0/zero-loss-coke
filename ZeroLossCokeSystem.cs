using System;
using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace ZeroLossCoke;

// ref: https://github.com/anegostudios/vssurvivalmod/blob/84c80e85f36e31d3b6454021da0fbbba4cba71a3/BlockEntity/BECoalPile.cs

[HarmonyPatch]
// ReSharper disable once UnusedType.Global
public class ZeroLossCokeSystem : ModSystem
{
    private Harmony? _harmony;
    private static ZeroLossCokeConfig _config = new();
    private static Logger _logger = null!;

    public override bool ShouldLoad(EnumAppSide side) => side.IsServer();

    public override void StartServerSide(ICoreServerAPI api)
    {
        base.StartServerSide(api);
        _logger = new Logger(api, Mod.Info.ModID);

        // Load and save configuration
        _config = api.LoadModConfig<ZeroLossCokeConfig>("zerolosscoke.json") ?? new ZeroLossCokeConfig();
        api.StoreModConfig(_config, "zerolosscoke.json");

        _config.YieldMultiplier = Math.Max(0.1f, _config.YieldMultiplier); // minimum 10% multiplier
        if (_config is { MinYield: > 0, MaxYield: > 0 } && _config.MinYield > _config.MaxYield)
        {
            (_config.MinYield, _config.MaxYield) = (_config.MaxYield, _config.MinYield);
            _logger.Warn("MinYield > MaxYield corrected in configuration");
        }

        // Apply Harmony patches
        _harmony = new Harmony(Mod.Info.ModID);
        _harmony.PatchAll(typeof(ZeroLossCokeSystem).Assembly);

        _logger.Event($"Mod loaded. Yield Multiplier: {_config.YieldMultiplier:F2}");
    }

    public override void Dispose()
    {
        _harmony?.UnpatchAll(Mod.Info.ModID);
    }

    /// <summary>
    /// Adjusts the yield of coke during the burning process on the server side based on the configured yield multiplier.
    /// Ensures that the final yield is within the specified minimum and maximum limits configured in <see cref="ZeroLossCokeConfig"/>.
    /// </summary>
    /// <param name="__instance">The instance of the <see cref="BlockEntityCoalPile"/> that is being processed.</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(BlockEntityCoalPile), "OnBurningTickServer")]
    // ReSharper disable once UnusedMember.Local
    private static void AdjustCokeYieldPostfix(
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        BlockEntityCoalPile __instance)
    {
        // Quick safety checks
        if (__instance.Api?.Side != EnumAppSide.Server) return;
        if (__instance.inventory[0]?.Itemstack == null) return;

        var currentStack = __instance.inventory[0].Itemstack;

        // Only act if the resulting item is Coke
        if (currentStack.Collectible?.Code?.Path != "coke") return;

        // Apply the configured multiplier
        var baseMultiplier = _config.YieldMultiplier;
        if (Math.Abs(baseMultiplier - 1.0f) < 0.01f) return; // Nothing to do if multiplier is 1

        var originalAmount = currentStack.StackSize;
        // Safe calculation, avoids overflow
        var newAmount = (int)Math.Max(1, Math.Min(originalAmount * baseMultiplier, int.MaxValue));

        // Apply configured limits
        if (_config.MinYield > 0) newAmount = Math.Max(newAmount, _config.MinYield);
        if (_config.MaxYield > 0) newAmount = Math.Min(newAmount, _config.MaxYield);

        // Apply the change and notify the game
        if (newAmount == originalAmount) return;
        currentStack.StackSize = newAmount;
        __instance.MarkDirty(true); // Important!! mark the block for saving

        if (_config.DebugLogging)
        {
            _logger.Debug(
                $"Adjusted at {__instance.Pos}: {originalAmount} -> {newAmount}"
            );
        }
    }
}