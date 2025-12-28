using System;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace ZeroLossCoke;

internal static class CoalPileYieldLogic
{
    public static void Apply(BlockEntityCoalPile be)
    {
        if (be.Api?.Side != EnumAppSide.Server) return;

        var stack = be.inventory[0]?.Itemstack;
        if (stack?.Item?.Code?.Path != "coke") return;

        var original = stack.StackSize;
        var cfg = ZeroLossCokeSystem.Config;

        if (Math.Abs(cfg.YieldMultiplier - 1f) < 0.01f) return;

        var value = (int)Math.Clamp(
            original * cfg.YieldMultiplier,
            1,
            int.MaxValue
        );

        if (cfg.MinYield > 0) value = Math.Max(value, cfg.MinYield);
        if (cfg.MaxYield > 0) value = Math.Min(value, cfg.MaxYield);

        if (value == original) return;

        stack.StackSize = value;
        be.MarkDirty(true);

        if (cfg.DebugLogging)
        {
            ZeroLossCokeSystem.Logger.Debug(
                $"Adjusted at {be.Pos}: {original} → {value}"
            );
        }
    }
}