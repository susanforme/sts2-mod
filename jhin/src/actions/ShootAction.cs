#nullable enable

using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Creatures;
using jhin.Magazine;
using jhin.Powers;

namespace jhin.Actions;

public enum ShootResult
{
    /// <summary>No bullet was consumed (magazine empty or state unavailable).</summary>
    Failed,
    /// <summary>Bullet consumed, but not the last one — no flourish.</summary>
    Normal,
    /// <summary>Last bullet consumed — flourish triggered.</summary>
    Flourish,
}

public static class ShootAction
{
    public static int GetMarkAmount(Creature? target)
    {
        return target?.GetPower<MarkPower>()?.Amount ?? 0;
    }

    public static void ConsumeMarks(Creature? target)
    {
        MarkPower? markPower = target?.GetPower<MarkPower>();
        if (markPower is null)
        {
            return;
        }

        target!.RemovePowerInternal(markPower);
    }

    /// <summary>
    /// Attempts to consume one bullet. Returns the result indicating whether
    /// the shot was normal, triggered a flourish, or failed.
    /// </summary>
    public static ShootResult Execute(Player? player)
    {
        JhinMagazineState? state = JhinMagazineStateRegistry.TryGet(player);
        if (state is null || !state.CanShoot())
        {
            return ShootResult.Failed;
        }

        bool isFlourish = state.TryConsumeBulletAndCheckFlourish();
        return isFlourish ? ShootResult.Flourish : ShootResult.Normal;
    }
}
