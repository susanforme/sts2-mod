#nullable enable

using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using jhin.Actions;
using jhin.CardPools;
using jhin.Magazine;

namespace jhin.Cards;

[Pool(typeof(JhinCardPool))]
public class SecondGunshot() : AbstractShootCard(
    cost: 1,
    rarity: CardRarity.Uncommon,
    target: TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move)];

    protected override bool IsPlayable
    {
        get
        {
            if (!base.IsPlayable)
            {
                return false;
            }

            JhinMagazineState? state = JhinMagazineStateRegistry.TryGet(Owner);
            return state is not null && state.Bullets == 3;
        }
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(JhinKeywords.Bullet),
        HoverTipFactory.FromKeyword(JhinKeywords.Mark),
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

        bool hadMark = ShootAction.GetMarkAmount(cardPlay.Target) > 0;

        await PerformShootAttack(choiceContext, cardPlay.Target);

        if (cardPlay.Target.IsAlive && hadMark)
        {
            await DealRawBonusDamage(choiceContext, cardPlay.Target, IsUpgraded ? 7 : 4);
        }

        EndFlourishContext();
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
    }
}
