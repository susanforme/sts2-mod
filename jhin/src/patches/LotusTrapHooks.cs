#nullable enable

using HarmonyLib;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using jhin.Powers;

namespace jhin.Patches;

[HarmonyPatch(typeof(MonsterModel), nameof(MonsterModel.PerformMove))]
public static class LotusTrapMonsterPerformMovePatch
{
    public static void Prefix(MonsterModel __instance, out bool __state)
    {
        __state = __instance.Creature?.IsEnemy == true
            && __instance.Creature.IsAlive
            && __instance.IntendsToAttack;
    }

    public static void Postfix(MonsterModel __instance, bool __state, ref Task __result)
    {
        if (!__state)
        {
            return;
        }

        __result = TriggerLotusTrapAfterMove(__instance, __result);
    }

    private static async Task TriggerLotusTrapAfterMove(MonsterModel monster, Task originalMoveTask)
    {
        await originalMoveTask;

        LotusTrapPower? lotusTrapPower = monster.Creature?.GetPower<LotusTrapPower>();
        if (lotusTrapPower is null)
        {
            return;
        }

        await lotusTrapPower.TriggerAfterOwnerAttack(new BlockingPlayerChoiceContext());
    }
}
