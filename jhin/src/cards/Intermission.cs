using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;
using jhin.CardPools;
using jhin.Powers;

namespace jhin.Cards;

/// <summary>
/// 幕间休息 / Intermission — 1 cost, common power.
/// First non-shoot attack each turn: +3 dmg. Upgrade: +5 dmg.
/// </summary>
[Pool(typeof(JhinCardPool))]
public class Intermission() : AbstractJhinCard(
    cost: 1,
    type: CardType.Power,
    rarity: CardRarity.Common,
    target: TargetType.Self)
{
    protected override IEnumerable<MegaCrit.Sts2.Core.Localization.DynamicVars.DynamicVar> CanonicalVars => [];

    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        IntermissionPower power = (IntermissionPower)ModelDb.Power<IntermissionPower>().ToMutable();
        power.ApplyInternal(Owner.Creature, IsUpgraded ? 2 : 1, silent: false);
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
    }
}
