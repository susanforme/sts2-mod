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
/// 控场艺术 / Stage Control — 1 cost, common power.
/// When applying Weak, also apply 1 Mark. Upgrade: 2 Mark.
/// </summary>
[Pool(typeof(JhinCardPool))]
public class StageControl() : AbstractJhinCard(
    cost: 1,
    type: CardType.Power,
    rarity: CardRarity.Common,
    target: TargetType.Self)
{
    protected override IEnumerable<MegaCrit.Sts2.Core.Localization.DynamicVars.DynamicVar> CanonicalVars => [];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(JhinKeywords.Mark),
    ];

    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        StageControlPower power = (StageControlPower)ModelDb.Power<StageControlPower>().ToMutable();
        power.ApplyInternal(Owner.Creature, IsUpgraded ? 2 : 1, silent: false);
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
    }
}
