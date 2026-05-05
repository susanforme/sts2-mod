#nullable enable

using HarmonyLib;
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

    public static void Postfix(MonsterModel __instance, bool __state)
    {
        if (!__state)
        {
            return;
        }

        LotusTrapPower? lotusTrapPower = __instance.Creature?.GetPower<LotusTrapPower>();
        lotusTrapPower?.TriggerAfterOwnerAttack();
    }
}
