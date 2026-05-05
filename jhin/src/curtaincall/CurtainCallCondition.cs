#nullable enable

using MegaCrit.Sts2.Core.Entities.Players;
using jhin.Magazine;

namespace jhin.CurtainCall;

[Flags]
public enum CurtainCallCondition
{
    None = 0,
    EmptyMagazine = 1,
    FlourishedThisTurn = 2,
}

public static class CurtainCallConditionChecker
{
    public static bool CanUse(Player? player, CurtainCallCondition condition)
    {
        if (condition == CurtainCallCondition.None)
        {
            return true;
        }

        JhinMagazineState? state = JhinMagazineStateRegistry.TryGet(player);
        if (state is null)
        {
            return player is null;
        }

        bool hasAnyRequiredCondition = false;

        if (condition.HasFlag(CurtainCallCondition.EmptyMagazine) && state.Bullets == 0)
        {
            hasAnyRequiredCondition = true;
        }

        if (condition.HasFlag(CurtainCallCondition.FlourishedThisTurn) && state.FlourishCountThisTurn > 0)
        {
            hasAnyRequiredCondition = true;
        }

        return hasAnyRequiredCondition;
    }
}
