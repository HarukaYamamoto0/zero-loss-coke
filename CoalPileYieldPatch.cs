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

    [HarmonyPostfix]
    private static void Postfix(BlockEntityCoalPile __instance)
    {
        CoalPileYieldLogic.Apply(__instance);
    }
}