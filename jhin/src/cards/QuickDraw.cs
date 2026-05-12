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
using jhin.Actions;
using jhin.CardPools;
using jhin.Extensions;

namespace jhin.Cards;

/// <summary>
/// 快速拔枪 / Quick Draw — 0 cost, shoot attack.
/// 3 dmg + draw 1. Upgrade: 5 dmg + draw 1.
/// </summary>
[Pool(typeof(JhinCardPool))]
public class QuickDraw() : AbstractShootCard(
    cost: 0,
    rarity: CardRarity.Common,
    target: TargetType.AnyEnemy)
{
    protected override string PortraitResourcePath => "Card/JHIN-QUICK_DRAW.png".ImagePath();

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(3, ValueProp.Move)];

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
            await JhinCombatActionUtil.Draw(choiceContext, Owner, 1);
        }
        finally
        {
            EndFlourishContext();
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
    }
}
