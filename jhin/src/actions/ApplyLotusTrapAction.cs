#nullable enable

using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using jhin.Powers;

namespace jhin.Actions;

public static class ApplyLotusTrapAction
{
    public static bool Execute(Creature? target, int amount)
    {
        if (target is null || amount <= 0 || !target.IsAlive || !target.CanReceivePowers)
        {
            return false;
        }

        LotusTrapPower? existingPower = target.GetPower<LotusTrapPower>();
        if (existingPower is not null)
        {
            existingPower.AddStacks(amount);
            return true;
        }

        LotusTrapPower lotusTrapPower = (LotusTrapPower)ModelDb.Power<LotusTrapPower>().ToMutable();
        lotusTrapPower.ApplyInternal(target, amount, silent: false);
        return true;
    }

    public static int ExecuteAllEnemies(Player? player, int amount)
    {
        if (player?.Creature?.CombatState is null || amount <= 0)
        {
            return 0;
        }

        int appliedCount = 0;
        foreach (Creature enemy in player.Creature.CombatState.HittableEnemies.Where(enemy => enemy.IsAlive))
        {
            if (Execute(enemy, amount))
            {
                appliedCount++;
            }
        }

        return appliedCount;
    }
}
