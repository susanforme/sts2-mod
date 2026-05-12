#nullable enable

using System.Diagnostics.CodeAnalysis;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Nodes.Combat;
using jhin.CardPools;
using jhin.Extensions;
using jhin.Powers;
using jhin.Utils;

namespace jhin.Cards;

public abstract class AbstractJhinCard(int cost, CardType type, CardRarity rarity, TargetType target)
    : CustomCardModel(cost, type, rarity, target)
{
    protected virtual string PortraitResourcePath => Placeholders.Card;

    public override string CustomPortraitPath => PortraitResourcePath;
    public override string PortraitPath => PortraitResourcePath;
    public override string BetaPortraitPath => PortraitResourcePath;

    protected static bool TryGetTarget(CardPlay cardPlay, [NotNullWhen(true)] out Creature? target)
    {
        return JhinTargetValidation.TryGetCardTarget(cardPlay, out target);
    }

    protected static void SubscribePowerEvents(object? power)
    {
        JhinPowerEventSubscription.SubscribeIfSupported(power);
    }
}
