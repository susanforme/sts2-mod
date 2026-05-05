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
/// 演员本能 / Actor's Instinct — 1 cost, common power.
/// If no flourish this turn, next turn +1 Strength. Upgrade: also +1 Dexterity.
/// </summary>
[Pool(typeof(JhinCardPool))]
public class ActorsInstinct() : AbstractJhinCard(
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
        ActorsInstinctPower power = (ActorsInstinctPower)ModelDb.Power<ActorsInstinctPower>().ToMutable();
        power.ApplyInternal(Owner.Creature, IsUpgraded ? 2 : 1, silent: false);
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
    }
}
