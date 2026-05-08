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
using jhin.CurtainCall;
using jhin.Magazine;
using jhin.Utils;

namespace jhin.Cards;

[Pool(typeof(JhinCardPool))]
public class FourthAct() : AbstractShootCard(
    cost: 2,
    rarity: CardRarity.Rare,
    target: TargetType.AnyEnemy)
{
    protected override bool IsPlayable
    {
        get
        {
            if (!base.IsPlayable)
            {
                return false;
            }

            JhinMagazineState? state = JhinMagazineStateRegistry.TryGet(Owner);
            return state is not null && state.Bullets == 1;
        }
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(16, ValueProp.Move)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(JhinKeywords.Bullet),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!TryShoot(choiceContext))
        {
            return;
        }

        if (cardPlay.Target is null)
        {
            EndFlourishContext();
            return;
        }

        ShootDamageCalculationResult damageResult = CalculateShootDamage(cardPlay.Target, IsFlourishShot);
        int scaledDamage = CurtainCallDamageUtil.CalculateDiscreteMissingHpDamage(damageResult.TotalDamage, cardPlay.Target);
        await PerformResolvedShootAttack(choiceContext, cardPlay.Target, scaledDamage, ShouldConsumeMarksAfterAttack());

        EndFlourishContext();
    }

    protected override PileType GetResultPileType() => PileType.Exhaust;

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(8m);
    }
}
