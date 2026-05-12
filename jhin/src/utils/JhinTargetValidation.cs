#nullable enable

using System.Diagnostics.CodeAnalysis;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace jhin.Utils;

public static class JhinTargetValidation
{
    public static bool TryGetCardTarget(CardPlay cardPlay, [NotNullWhen(true)] out Creature? target)
    {
        target = cardPlay.Target;
        return target is not null;
    }

    public static bool TryGetLivingCardTarget(CardPlay cardPlay, [NotNullWhen(true)] out Creature? target)
    {
        return TryGetCardTarget(cardPlay, out target) && target.IsAlive;
    }
}
