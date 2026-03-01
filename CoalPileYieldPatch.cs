// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedType.Global

using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Vintagestory.GameContent;

namespace ZeroLossCoke;

[HarmonyPatch]
public static class CoalPileYieldPatch
{
    static IEnumerable<MethodBase> TargetMethods()
    {
        // VS 1.21+
        var externalTick = AccessTools.Method(
            typeof(BlockEntityCoalPile),
            "OnBurningTickServer",
            [typeof(float)]
        );

        if (externalTick != null)
            yield return externalTick;

        // VS 1.20 fallback
        var oldTick = AccessTools.Method(
            typeof(BlockEntityCoalPile),
            "onBurningTickServer",
            [typeof(float)]
        );

        if (oldTick != null)
            yield return oldTick;
    }

    [HarmonyPrefix]
    private static void Prefix(BlockEntityCoalPile __instance, out bool __state)
    {
        // Check if the pile already contains coke BEFORE the tick
        // This prevents the doubling of existing coke piles used as fuel (e.g., in a steel furnace)
        var stack = __instance.inventory[0]?.Itemstack;
        __state = stack?.Item?.Code?.Path != "coke";
    }

    [HarmonyPostfix]
    private static void Postfix(BlockEntityCoalPile __instance, bool __state)
    {
        // Only apply logic if it was NOT coke before the tick (indicates it was still coal)
        // AND the logic itself will check if it IS coke now (indicates conversion happened)
        if (__state)
        {
            CoalPileYieldLogic.Apply(__instance);
        }
    }
}