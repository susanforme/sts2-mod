#nullable enable

using BaseLib.Abstracts;
using BaseLib.Patches.Localization;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using jhin.Utils;

namespace jhin.Powers;

public class LotusTrapPower : CustomPowerModel, IAddDumbVariablesToPowerDescription
{
    private static readonly Dictionary<Creature, int> PendingWeakByCreature = [];

    public override string CustomPackedIconPath => "res://JhinMod/Images/captive _audience.png";
    public override string CustomBigIconPath => "res://JhinMod/Images/captive _audience.png";
    public override string CustomBigBetaIconPath => "res://JhinMod/Images/captive _audience.png";

    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override int DisplayAmount => Amount;

    public static int DamagePerStack => ConstantUtil.LotusTrapDamagePerStack;
    public static int WeakPerStack => ConstantUtil.LotusTrapWeakPerStack;
    public static int DeathExplosionDamage => ConstantUtil.LotusTrapDeathExplosionDamage;

    public void AddDumbVariablesToPowerDescription(LocString description)
    {
        description.Add("lotusTrapDamagePerStack", DamagePerStack);
        description.Add("lotusTrapWeakPerStack", WeakPerStack);
        description.Add("lotusTrapDeathExplosionDamage", DeathExplosionDamage);
    }

    public void AddStacks(int amount)
    {
        SetAmount(Amount + amount, silent: false);
    }

    public void TriggerAfterOwnerAttack()
    {
        Creature? target = Owner;
        if (target is null || Amount <= 0 || !target.IsAlive)
        {
            return;
        }

        int stacks = Amount;
        Flash();
        DamageResult damageResult = target.LoseHpInternal(stacks * DamagePerStack, ValueProp.Move);

        if (damageResult.WasTargetKilled)
        {
            TriggerDeathExplosion(target);
            return;
        }

        if (target.IsAlive && target.CanReceivePowers)
        {
            QueuePendingWeak(target, stacks * WeakPerStack);
        }

        target.RemovePowerInternal(this);
    }

    public static void ApplyPendingWeak(CombatSide endedSide)
    {
        if (PendingWeakByCreature.Count == 0)
        {
            return;
        }

        foreach ((Creature target, int amount) in PendingWeakByCreature.ToArray())
        {
            PendingWeakByCreature.Remove(target);
            if (target.Side != endedSide || target.CombatState is null || !target.CombatState.ContainsCreature(target) || !target.IsAlive || !target.CanReceivePowers)
            {
                continue;
            }

            ApplyOrStackWeak(target, amount);
        }
    }

    public static void ClearPendingWeak()
    {
        PendingWeakByCreature.Clear();
    }

    private static void QueuePendingWeak(Creature target, int amount)
    {
        if (!PendingWeakByCreature.TryAdd(target, amount))
        {
            PendingWeakByCreature[target] += amount;
        }
    }

    private static void ApplyOrStackWeak(Creature target, int amount)
    {
        WeakPower? existingPower = target.GetPower<WeakPower>();
        if (existingPower is not null)
        {
            existingPower.SetAmount(existingPower.Amount + amount, silent: false);
            return;
        }

        WeakPower weakPower = (WeakPower)ModelDb.Power<WeakPower>().ToMutable();
        weakPower.ApplyInternal(target, amount, silent: false);
    }

    private static void TriggerDeathExplosion(Creature killedTarget)
    {
        if (killedTarget.CombatState is null)
        {
            return;
        }

        foreach (Creature enemy in killedTarget.CombatState.HittableEnemies.Where(enemy => enemy.IsAlive))
        {
            enemy.LoseHpInternal(DeathExplosionDamage, ValueProp.Move);
        }
    }
}
