using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using jhin.CardPools;
using jhin.Extensions;

namespace jhin.Cards;

/// <summary>
/// 侧身开火 / Sidestep Fire — 1 cost, shoot attack.
/// 6 dmg + 4 Block. Upgrade: 8 dmg + 5 Block.
/// </summary>
[Pool(typeof(JhinCardPool))]
public class SidestepFire() : AbstractShootCard(
    cost: 1,
    rarity: CardRarity.Common,
    target: TargetType.AnyEnemy)
{
    protected override string PortraitResourcePath => "Card/JHIN-SIDESTEP_FIRE.png".ImagePath();

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(6, ValueProp.Move),
        new BlockVar(4, ValueProp.Move),
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(JhinKeywords.Bullet),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!TryShootTarget(choiceContext, cardPlay, out Creature? target))
        {
            return;
        }

        try
        {
            await PerformShootAttack(choiceContext, target);
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block.IntValue, ValueProp.Move, cardPlay, false);
        }
        finally
        {
            EndFlourishContext();
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
        DynamicVars.Block.UpgradeValueBy(1m);
    }
}
