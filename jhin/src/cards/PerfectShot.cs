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

[Pool(typeof(JhinCardPool))]
public class PerfectShot() : AbstractShootCard(
    cost: 1,
    rarity: CardRarity.Common,
    target: TargetType.AnyEnemy)
{
    protected override string PortraitResourcePath => "Card/JHIN-PERFECT_SHOT.png".ImagePath();

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(9, ValueProp.Move)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(JhinKeywords.Bullet),
        HoverTipFactory.FromKeyword(JhinKeywords.Flourish),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!TryShootTarget(choiceContext, cardPlay, out Creature? target))
        {
            return;
        }

        bool wasFlourish = IsFlourishShot;

        try
        {
            await PerformShootAttack(choiceContext, target);
        }
        finally
        {
            EndFlourishContext();
        }

        if (wasFlourish)
        {
            await JhinCombatActionUtil.GainEnergy(Owner, 1);
            if (IsUpgraded)
            {
                await JhinCombatActionUtil.Draw(choiceContext, Owner, 1);
            }
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
    }
}
