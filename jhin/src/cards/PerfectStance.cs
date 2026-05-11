using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using jhin.Actions;
using jhin.CardPools;

namespace jhin.Cards;

/// <summary>
/// 完美姿态 / Perfect Stance — 1 cost, skill.
/// 6 Block. If 4 bullets, +6 Block. Upgrade: 8 Block, +8 Block.
/// </summary>
[Pool(typeof(JhinCardPool))]
public class PerfectStance() : AbstractJhinCard(
    cost: 1,
    type: CardType.Skill,
    rarity: CardRarity.Uncommon,
    target: TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(6, ValueProp.Move)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(JhinKeywords.Bullet),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this, cardPlay);

        if (JhinCombatActionUtil.HasBulletCount(Owner, 4))
        {
            int bonusBlock = IsUpgraded ? 8 : 6;
            await CreatureCmd.GainBlock(Owner.Creature, bonusBlock, ValueProp.Move, cardPlay, false);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(2m);
    }
}
