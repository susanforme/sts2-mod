using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.Combat;
using jhin.Actions;
using jhin.CardPools;

namespace jhin.Cards;

/// <summary>
/// 数拍 / Count Beats — 0 cost, skill. Exhaust.
/// Draw 1. If bullets = 1, draw 1 more. Upgrade: Draw 2 instead.
/// (Simplified from Scry since Scry is not available in STS2.)
/// </summary>
[Pool(typeof(JhinCardPool))]
public class CountBeats() : AbstractJhinCard(
    cost: 0,
    type: CardType.Skill,
    rarity: CardRarity.Common,
    target: TargetType.Self)
{
    protected override IEnumerable<MegaCrit.Sts2.Core.Localization.DynamicVars.DynamicVar> CanonicalVars => [];

    protected override IEnumerable<MegaCrit.Sts2.Core.HoverTips.IHoverTip> ExtraHoverTips =>
    [
        MegaCrit.Sts2.Core.HoverTips.HoverTipFactory.FromKeyword(JhinKeywords.Bullet),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int drawAmount = IsUpgraded ? 2 : 1;
        await JhinCombatActionUtil.Draw(choiceContext, Owner, drawAmount);

        if (JhinCombatActionUtil.HasBulletCount(Owner, 1))
        {
            await JhinCombatActionUtil.Draw(choiceContext, Owner, 1);
        }
    }

    protected override PileType GetResultPileType() => PileType.Exhaust;

    protected override void OnUpgrade()
    {
    }
}
