using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;
using jhin.CardPools;
using jhin.Powers;

namespace jhin.Cards;

/// <summary>
/// 从容不迫 / Composed — 1 cost, common power.
/// First flourish each combat: draw 2. Upgrade: Innate.
/// </summary>
[Pool(typeof(JhinCardPool))]
public class Composed() : AbstractJhinCard(
    cost: 1,
    type: CardType.Power,
    rarity: CardRarity.Common,
    target: TargetType.Self)
{
    protected override IEnumerable<MegaCrit.Sts2.Core.Localization.DynamicVars.DynamicVar> CanonicalVars => [];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(JhinKeywords.Flourish),
    ];

    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ComposedPower power = (ComposedPower)ModelDb.Power<ComposedPower>().ToMutable();
        power.ApplyInternal(Owner.Creature, 1, silent: false);
        power.SubscribeEvents();
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
    }
}
