using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using jhin.CardPools;

namespace jhin.Cards;

[Pool(typeof(JhinCardPool))]
public class DeadlyShot() : AbstractShootCard(
    cost: 1,
    rarity: CardRarity.Uncommon,
    target: TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(8, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!TryShootTarget(choiceContext, cardPlay, out Creature? target))
        {
            return;
        }

        try
        {
            await PerformShootAttack(choiceContext, target);

            if (target.IsAlive && target.GetPower<MegaCrit.Sts2.Core.Models.Powers.WeakPower>() is null)
            {
                await DealRawBonusDamage(choiceContext, target, IsUpgraded ? 8 : 6);
            }
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
